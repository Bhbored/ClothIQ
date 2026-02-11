using Supabase;
using TshirtMaker.Repositories;
using TshirtMaker.Repositories.Interfaces;
using TshirtMaker.Services.AI;
using TshirtMaker.Services.Supabase;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Microsoft.JSInterop;

namespace TshirtMaker.Services
{
    public static class DIContainer
    {
        public static IServiceCollection RegisterSupabase(
            this IServiceCollection services,
            string supabaseUrl,
            string supabaseAnonKey)
        {
            if (string.IsNullOrWhiteSpace(supabaseUrl))
                throw new ArgumentException("Supabase URL is required.", nameof(supabaseUrl));
            if (string.IsNullOrWhiteSpace(supabaseAnonKey))
                throw new ArgumentException("Supabase anon key is required.", nameof(supabaseAnonKey));

            services.AddSingleton<global::Supabase.Client>(sp =>
            {
                var jsRuntime = sp.GetRequiredService<IJSRuntime>();
                var sessionHandler = new SupabaseSessionHandler(jsRuntime);

                var options = new SupabaseOptions
                {
                    AutoRefreshToken = true,
                    AutoConnectRealtime = false,
                    SessionHandler = sessionHandler
                };

                var client = new global::Supabase.Client(supabaseUrl, supabaseAnonKey, options);

              
                    try
                    {
                         client.InitializeAsync();
                    }
                    catch (Exception ex)
                    {
                        var logger = sp.GetService<ILoggerFactory>()?.CreateLogger("Supabase.Client.Init");
                        logger?.LogError(ex, "Failed to initialize Supabase.Client");
                    }
               

                return client;
            });

            services.AddHttpClient("supabase", client =>
            {
                client.BaseAddress = new Uri(supabaseUrl);
                if (!client.DefaultRequestHeaders.Contains("apikey"))
                    client.DefaultRequestHeaders.Add("apikey", supabaseAnonKey);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(100);
            });

            return services;
        }

        public static IServiceCollection RegisterRepositories(
            this IServiceCollection services,
            string supabaseAnonKey)
        {
            if (string.IsNullOrWhiteSpace(supabaseAnonKey))
                throw new ArgumentException("Supabase anon key is required.", nameof(supabaseAnonKey));

            services.AddScoped<IUserRepository>(sp =>
            {
                var client = sp.GetRequiredService<IHttpClientFactory>().CreateClient("supabase");
                var tokenProvider = sp.GetRequiredService<ISupabaseAccessTokenProvider>();
                return new UserRepository(client, supabaseAnonKey, tokenProvider);
            });

            services.AddScoped<IDesignRepository>(sp =>
            {
                var client = sp.GetRequiredService<IHttpClientFactory>().CreateClient("supabase");
                var tokenProvider = sp.GetRequiredService<ISupabaseAccessTokenProvider>();
                return new DesignRepository(client, supabaseAnonKey, tokenProvider);
            });

            services.AddScoped<IPostRepository>(sp =>
            {
                var client = sp.GetRequiredService<IHttpClientFactory>().CreateClient("supabase");
                var tokenProvider = sp.GetRequiredService<ISupabaseAccessTokenProvider>();
                return new PostRepository(client, supabaseAnonKey, tokenProvider);
            });

            services.AddScoped<ICommentRepository>(sp =>
            {
                var client = sp.GetRequiredService<IHttpClientFactory>().CreateClient("supabase");
                var tokenProvider = sp.GetRequiredService<ISupabaseAccessTokenProvider>();
                return new CommentRepository(client, supabaseAnonKey, tokenProvider);
            });

            services.AddScoped<ILikeRepository>(sp =>
            {
                var client = sp.GetRequiredService<IHttpClientFactory>().CreateClient("supabase");
                var tokenProvider = sp.GetRequiredService<ISupabaseAccessTokenProvider>();
                return new LikeRepository(client, supabaseAnonKey, tokenProvider);
            });

            services.AddScoped<IBookmarkRepository>(sp =>
            {
                var client = sp.GetRequiredService<IHttpClientFactory>().CreateClient("supabase");
                var tokenProvider = sp.GetRequiredService<ISupabaseAccessTokenProvider>();
                return new BookmarkRepository(client, supabaseAnonKey, tokenProvider);
            });

            services.AddScoped<IFollowerRepository>(sp =>
            {
                var client = sp.GetRequiredService<IHttpClientFactory>().CreateClient("supabase");
                var tokenProvider = sp.GetRequiredService<ISupabaseAccessTokenProvider>();
                return new FollowerRepository(client, supabaseAnonKey, tokenProvider);
            });

            services.AddScoped<INotificationRepository>(sp =>
            {
                var client = sp.GetRequiredService<IHttpClientFactory>().CreateClient("supabase");
                var tokenProvider = sp.GetRequiredService<ISupabaseAccessTokenProvider>();
                return new NotificationRepository(client, supabaseAnonKey, tokenProvider);
            });

            services.AddScoped<ICollectionRepository>(sp =>
            {
                var client = sp.GetRequiredService<IHttpClientFactory>().CreateClient("supabase");
                var tokenProvider = sp.GetRequiredService<ISupabaseAccessTokenProvider>();
                return new CollectionRepository(client, supabaseAnonKey, tokenProvider);
            });

            services.AddScoped<IOrderRepository>(sp =>
            {
                var client = sp.GetRequiredService<IHttpClientFactory>().CreateClient("supabase");
                var tokenProvider = sp.GetRequiredService<ISupabaseAccessTokenProvider>();
                return new OrderRepository(client, supabaseAnonKey, tokenProvider);
            });

            services.AddScoped<IOrderItemRepository>(sp =>
            {
                var client = sp.GetRequiredService<IHttpClientFactory>().CreateClient("supabase");
                var tokenProvider = sp.GetRequiredService<ISupabaseAccessTokenProvider>();
                return new OrderItemRepository(client, supabaseAnonKey, tokenProvider);
            });

            services.AddScoped<IShippingAddressRepository>(sp =>
            {
                var client = sp.GetRequiredService<IHttpClientFactory>().CreateClient("supabase");
                var tokenProvider = sp.GetRequiredService<ISupabaseAccessTokenProvider>();
                return new ShippingAddressRepository(client, supabaseAnonKey, tokenProvider);
            });

            services.AddScoped<ITrackingEventRepository>(sp =>
            {
                var client = sp.GetRequiredService<IHttpClientFactory>().CreateClient("supabase");
                var tokenProvider = sp.GetRequiredService<ISupabaseAccessTokenProvider>();
                return new TrackingEventRepository(client, supabaseAnonKey, tokenProvider);
            });

            return services;
        }

        public static IServiceCollection RegisterAuthServices(this IServiceCollection services)
        {
            services.AddSingleton<TestDataService>();
            services.AddSingleton<DeepLinkService>();
            services.AddScoped<SupabaseAuthService>();
            services.AddScoped<ISupabaseAccessTokenProvider>(sp => sp.GetRequiredService<SupabaseAuthService>());
            services.AddScoped<AuthStateService>();
            services.AddScoped<SupabaseStorageService>();
            return services;
        }

        public static IServiceCollection RegisterAppServices(this IServiceCollection services, string? openAiApiKey = null)
        {
            services.AddScoped<IAIDesignService>(sp =>
            {
                var httpClient = sp.GetRequiredService<HttpClient>();
                var storageService = sp.GetRequiredService<SupabaseStorageService>();
                return new OpenAIDesignService(httpClient, storageService, openAiApiKey);
            });

            return services;
        }

        public static IServiceCollection RegisterDependencies(this IServiceCollection services,
            string supabaseUrl,
            string supabaseAnonKey,
            string? openAiApiKey = null)
        {
            return services
                .RegisterSupabase(supabaseUrl, supabaseAnonKey)
                .RegisterRepositories(supabaseAnonKey)
                .RegisterAuthServices()
                .RegisterAppServices(openAiApiKey);
        }
    }
}