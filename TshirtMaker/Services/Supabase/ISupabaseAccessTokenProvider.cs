namespace TshirtMaker.Services.Supabase;

public interface ISupabaseAccessTokenProvider
{
    Task<string?> GetAccessTokenAsync();
}
