using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TshirtMaker.Services.Supabase
{
    public class SupabaseStorageService
    {
        private readonly global::Supabase.Client _supabase;
        private readonly string  bucketName = "Design images";

        public SupabaseStorageService(global::Supabase.Client supabase)
        {
            _supabase = supabase;
        }

        public async Task<string> UploadToUserTempFolderAsync(byte[] imageBytes, string fileName, Guid userId, CancellationToken cancellationToken = default)
        {
            var filePath = $"{userId}/temp/{fileName}";

            var storage = _supabase.Storage.From(bucketName);
            var result = await storage.Upload(imageBytes, filePath, new()
            {
                CacheControl = "3600",
                ContentType = "image/png"
            });

            var signedUrl = await storage.CreateSignedUrl(filePath, 60 * 24 * 365);
            return signedUrl;
        }

        public async Task<string> UploadToUserFinalFolderAsync(byte[] imageBytes, string fileName, Guid userId, CancellationToken cancellationToken = default)
        {
            var filePath = $"{userId}/final/{fileName}";

            var storage = _supabase.Storage.From(bucketName);
            var result = await storage.Upload(imageBytes, filePath, new()
            {
                CacheControl = "3600",
                ContentType = "image/png"
            });

            var signedUrl = await storage.CreateSignedUrl(filePath, 60 * 24 * 365);
            return signedUrl;
        }

        public async Task<List<string>> GetUserTempImagesAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var folderPath = $"{userId}/temp";

            var storage = _supabase.Storage.From(bucketName);
            var result = await storage.List(folderPath);

            var urls = new List<string>();
            if (result != null)
            {
                foreach (var file in result)
                {
                    if (file?.Name != null)
                    {
                        var fullPath = $"{folderPath}/{file.Name}";
                        var signedUrl = await storage.CreateSignedUrl(fullPath, 60 * 24 * 365);
                        urls.Add(signedUrl);
                    }
                }
            }

            return urls;
        }

        public async Task<List<string>> GetUserFinalImagesAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var folderPath = $"{userId}/final";

            var storage = _supabase.Storage.From(bucketName);
            var result = await storage.List(folderPath);

            var urls = new List<string>();
            if (result != null)
            {
                foreach (var file in result)
                {
                    if (file?.Name != null)
                    {
                        var fullPath = $"{folderPath}/{file.Name}";
                        var signedUrl = await storage.CreateSignedUrl(fullPath, 60 * 24 * 365);
                        urls.Add(signedUrl);
                    }
                }
            }

            return urls;
        }

        public async Task<int> GetNextTempImageIndexAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var folderPath = $"{userId}/temp";

            var storage = _supabase.Storage.From(bucketName);
            var result = await storage.List(folderPath);

            if (result == null || !result.Any())
                return 1;

            var maxIndex = result
                .Where(f => f?.Name != null && f.Name.StartsWith("temp_") && f.Name.EndsWith(".png"))
                .Select(f => f.Name!)
                .Select(name => name.Replace("temp_", "").Replace(".png", ""))
                .Where(s => int.TryParse(s, out _))
                .Select(int.Parse)
                .DefaultIfEmpty(0)
                .Max();

            return maxIndex + 1;
        }

        public async Task<int> GetNextFinalImageIndexAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var folderPath = $"{userId}/final";

            var storage = _supabase.Storage.From(bucketName);
            var result = await storage.List(folderPath);

            if (result == null || !result.Any())
                return 1;

            var maxIndex = result
                .Where(f => f?.Name != null && f.Name.StartsWith("final_") && f.Name.EndsWith(".png"))
                .Select(f => f.Name!)
                .Select(name => name.Replace("final_", "").Replace(".png", ""))
                .Where(s => int.TryParse(s, out _))
                .Select(int.Parse)
                .DefaultIfEmpty(0)
                .Max();

            return maxIndex + 1;
        }
    }
}