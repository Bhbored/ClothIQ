using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TshirtMaker.Services;

namespace TshirtMaker
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            // Root components
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // HttpClient
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });

            // Get configuration from appsettings.json
            var configuration = builder.Configuration;
            var supabaseUrl = configuration["Supabase:Url"];
            var supabaseAnonKey = configuration["Supabase:AnonKey"];
            var openAiApiKey = configuration["OpenAI:ApiKey"];

            // If configuration isn't found in appsettings.json, try environment variables
            if (string.IsNullOrEmpty(supabaseUrl))
                supabaseUrl = Environment.GetEnvironmentVariable("SUPABASE_URL");
            if (string.IsNullOrEmpty(supabaseAnonKey))
                supabaseAnonKey = Environment.GetEnvironmentVariable("SUPABASE_ANON_KEY");
            if (string.IsNullOrEmpty(openAiApiKey))
                openAiApiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

            // Validate configuration
            if (string.IsNullOrEmpty(supabaseUrl))
                throw new InvalidOperationException("Supabase:Url configuration is missing. Please set SUPABASE_URL in environment variables or appsettings.json.");

            if (string.IsNullOrEmpty(supabaseAnonKey))
                throw new InvalidOperationException("Supabase:AnonKey configuration is missing. Please set SUPABASE_ANON_KEY in environment variables or appsettings.json.");

            // Register services (you'll need to update RegisterDependencies for WASM)
            builder.Services.RegisterDependencies(supabaseUrl, supabaseAnonKey, openAiApiKey);

            // Additional WASM services
            builder.Services.AddAuthorizationCore(); // If you use authentication

            await builder.Build().RunAsync();
        }
    }
}
