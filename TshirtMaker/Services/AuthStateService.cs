using TshirtMaker.Models.Core;
using TshirtMaker.Services.Supabase;

namespace TshirtMaker.Services;

public class AuthStateService
{
    private readonly SupabaseAuthService _supabaseAuth;
    private User? _currentUser = null;

    public AuthStateService(SupabaseAuthService supabaseAuth)
    {
        _supabaseAuth = supabaseAuth;
    }

    public event Action? OnUserChanged;

    public User? CurrentUser
    {
        get => _currentUser;
        set
        {
            _currentUser = value;
            OnUserChanged?.Invoke();
        }
    }

    public bool IsAuthenticated => CurrentUser != null;

    public async Task<User?> InitializeAsync()
    {
        if (CurrentUser != null)
        {
            return CurrentUser;
        }

        try
        {

            await _supabaseAuth.RestoreSessionFromStorageAsync();

            var userDto = await _supabaseAuth.GetCurrentUserAsync();
            if (userDto != null)
            {
                CurrentUser = userDto.ToModel();
                return CurrentUser;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error initializing auth state: {ex.Message}");
        }

        CurrentUser = null;
        return null;
    }


    public async Task<User?> RefreshAsync()
    {

        CurrentUser = null;


        return await InitializeAsync();
    }

    public void SetUser(User user)
    {
        CurrentUser = user;
    }

    public void ClearUser()
    {
        CurrentUser = null;
    }
}
