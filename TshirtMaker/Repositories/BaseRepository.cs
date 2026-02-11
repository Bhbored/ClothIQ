using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TshirtMaker.DTOs;
using TshirtMaker.Repositories.Interfaces;
using TshirtMaker.Services.Supabase;

namespace TshirtMaker.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntityDto, new()
    {
        private readonly HttpClient _httpClient;
        protected readonly string _tableName;
        private readonly string _apiKey;
        private readonly ISupabaseAccessTokenProvider _tokenProvider;
        private readonly JsonSerializerOptions _jsonOptions;

        public BaseRepository(
            HttpClient httpClient,
            string tableName,
            string apiKey,
            ISupabaseAccessTokenProvider tokenProvider)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _tableName = string.IsNullOrWhiteSpace(tableName) ? throw new ArgumentNullException(nameof(tableName)) : tableName;
            _apiKey = string.IsNullOrWhiteSpace(apiKey) ? throw new ArgumentNullException(nameof(apiKey)) : apiKey;
            _tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));

            if (!_httpClient.DefaultRequestHeaders.Accept.Any(h => h.MediaType == "application/json"))
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }
        #region  Base Methods
        private async Task<HttpRequestMessage> CreateRequestAsync(HttpMethod method, string requestUri, HttpContent? content = null, bool preferReturnRepresentation = false)
        {
            var req = new HttpRequestMessage(method, requestUri);
            if (content != null) req.Content = content;

            var token = await _tokenProvider.GetAccessTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            if (!req.Headers.Contains("apikey"))
                req.Headers.Add("apikey", _apiKey);

            if (preferReturnRepresentation && req.Content != null)
                req.Headers.Add("Prefer", "return=representation");

            return req;
        }

        private string BuildTableUrl(string query = "")
        {
            if (_tableName.Any(c => char.IsWhiteSpace(c) || c == '/' || c == '\\'))
                throw new InvalidOperationException("Invalid table name configured for repository.");

            var path = $"/rest/v1/{_tableName}";
            if (!string.IsNullOrEmpty(query))
                path += query.StartsWith("?") ? query : "?" + query;
            return path;
        }

        protected async Task<List<TResult>> ExecuteGetListAsync<TResult>(string path)
        {
            using var req = await CreateRequestAsync(HttpMethod.Get, path);
            using var res = await _httpClient.SendAsync(req, HttpCompletionOption.ResponseHeadersRead);
            res.EnsureSuccessStatusCode();
            using var stream = await res.Content.ReadAsStreamAsync();
            var items = await JsonSerializer.DeserializeAsync<List<TResult>>(stream, _jsonOptions);
            return items ?? new List<TResult>();
        }

        protected async Task<TResult?> ExecuteGetSingleAsync<TResult>(string path)
        {
            var list = await ExecuteGetListAsync<TResult>(path);
            return list.FirstOrDefault();
        }

        protected async Task<List<TResult>> ExecutePostAsync<TResult, TPayload>(string path, TPayload payload, bool returnRepresentation = true)
        {
            var json = JsonSerializer.Serialize(payload, _jsonOptions);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            using var req = await CreateRequestAsync(HttpMethod.Post, path, content, preferReturnRepresentation: returnRepresentation);
            using var res = await _httpClient.SendAsync(req, HttpCompletionOption.ResponseHeadersRead);
            res.EnsureSuccessStatusCode();
            using var stream = await res.Content.ReadAsStreamAsync();
            var items = await JsonSerializer.DeserializeAsync<List<TResult>>(stream, _jsonOptions);
            return items ?? new List<TResult>();
        }

        protected async Task<List<TResult>> ExecutePatchAsync<TResult, TPayload>(string path, TPayload payload, bool returnRepresentation = true)
        {
            var json = JsonSerializer.Serialize(payload, _jsonOptions);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            using var req = await CreateRequestAsync(HttpMethod.Patch, path, content, preferReturnRepresentation: returnRepresentation);
            using var res = await _httpClient.SendAsync(req, HttpCompletionOption.ResponseHeadersRead);
            res.EnsureSuccessStatusCode();
            using var stream = await res.Content.ReadAsStreamAsync();
            var items = await JsonSerializer.DeserializeAsync<List<TResult>>(stream, _jsonOptions);
            return items ?? new List<TResult>();
        }

        protected async Task<bool> ExecuteDeleteAsync(string path)
        {
            using var req = await CreateRequestAsync(HttpMethod.Delete, path);
            using var res = await _httpClient.SendAsync(req, HttpCompletionOption.ResponseHeadersRead);
            return res.IsSuccessStatusCode;
        }
        #endregion


        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            var uri = BuildTableUrl($"?id=eq.{Uri.EscapeDataString(id.ToString())}&select=*");
            return await ExecuteGetSingleAsync<T>(uri);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            pageNumber = Math.Max(1, pageNumber);
            pageSize = Math.Clamp(pageSize, 1, 1000);
            var offset = (pageNumber - 1) * pageSize;
            var uri = BuildTableUrl($"?select=*&order=created_at.desc&offset={offset}&limit={pageSize}");
            return await ExecuteGetListAsync<T>(uri);
        }

        public virtual async Task<T?> CreateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Id == Guid.Empty) entity.Id = Guid.NewGuid();
            entity.UpdatedAt = DateTime.UtcNow;

            var uri = BuildTableUrl();
            var created = await ExecutePostAsync<T, T>(uri, entity, returnRepresentation: true);
            return created.FirstOrDefault() ?? entity;
        }

        public virtual async Task<T?> UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.UpdatedAt = DateTime.UtcNow;

            var uri = BuildTableUrl($"?id=eq.{Uri.EscapeDataString(entity.Id.ToString())}");
            var updated = await ExecutePatchAsync<T, T>(uri, entity, returnRepresentation: true);
            return updated.FirstOrDefault() ?? entity;
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var uri = BuildTableUrl($"?id=eq.{Uri.EscapeDataString(id.ToString())}");
            return await ExecuteDeleteAsync(uri);
        }
    }
}