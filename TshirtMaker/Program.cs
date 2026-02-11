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


            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");


            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });


            var configuration = builder.Configuration;
            var supabaseUrl = configuration["Supabase:Url"];
            var supabaseAnonKey = configuration["Supabase:AnonKey"];
            var openAiApiKey = configuration["OpenAI:ApiKey"];


            if (string.IsNullOrEmpty(supabaseUrl))
                supabaseUrl = Environment.GetEnvironmentVariable("SUPABASE_URL");
            if (string.IsNullOrEmpty(supabaseAnonKey))
                supabaseAnonKey = Environment.GetEnvironmentVariable("SUPABASE_ANON_KEY");
            if (string.IsNullOrEmpty(openAiApiKey))
                openAiApiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");


            if (string.IsNullOrEmpty(supabaseUrl))
                throw new InvalidOperationException("Supabase:Url configuration is missing. Please set SUPABASE_URL in environment variables or appsettings.json.");

            if (string.IsNullOrEmpty(supabaseAnonKey))
                throw new InvalidOperationException("Supabase:AnonKey configuration is missing. Please set SUPABASE_ANON_KEY in environment variables or appsettings.json.");


            builder.Services.RegisterDependencies(supabaseUrl, supabaseAnonKey, openAiApiKey);


            builder.Services.AddAuthorizationCore();

            await builder.Build().RunAsync();
        }
    }
}
