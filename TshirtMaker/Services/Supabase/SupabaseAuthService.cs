using Supabase.Gotrue;
using TshirtMaker.DTOs;

namespace TshirtMaker.Services.Supabase
{
    public class SupabaseAuthService : ISupabaseAccessTokenProvider
    {
        private readonly global::Supabase.Client _supabase;
        private const string SessionKey = "supabase_access_token";
        private const string RefreshTokenKey = "supabase_refresh_token";

        private string? _cachedAccessToken;
        private string? _cachedRefreshToken;

        public SupabaseAuthService(global::Supabase.Client supabase)
        {
            _supabase = supabase;
        }

        public async Task<UserDto?> SignUpAsync(string email, string password, Dictionary<string, object>? metadata = null)
        {
            var options = new SignUpOptions
            {
                Data = metadata ?? new Dictionary<string, object>()
            };

            var response = await _supabase.Auth.SignUp(email, password, options);
            if (response?.User == null) return null;

            if (!string.IsNullOrEmpty(response.AccessToken))
            {
                StoreSession(response.AccessToken, response.RefreshToken);
            }

            return MapToUserDto(response.User);
        }

        public async Task<UserDto?> SignInAsync(string email, string password)
        {
            var response = await _supabase.Auth.SignIn(email, password);
            if (response?.User == null) return null;

            StoreSession(response.AccessToken, response.RefreshToken);
            return MapToUserDto(response.User);
        }

        public async Task<UserDto?> GetCurrentUserAsync()
        {
            try
            {
                var currentUser = _supabase.Auth.CurrentUser;
                if (currentUser != null)
                {
                    return MapToUserDto(currentUser);
                }


                var accessToken = GetStoredAccessToken();
                if (!string.IsNullOrEmpty(accessToken))
                {
                    var user = await _supabase.Auth.GetUser(accessToken);
                    if (user != null)
                    {
                        return MapToUserDto(user);
                    }
                }

                return null;
            }
            catch
            {

                await TryRefreshSessionAsync();

                try
                {
                    var currentUser = _supabase.Auth.CurrentUser;
                    if (currentUser != null)
                    {
                        return MapToUserDto(currentUser);
                    }
                }
                catch
                {
                    ClearSession();
                }

                return null;
            }
        }


        public async Task<bool> RestoreSessionFromStorageAsync()
        {
            try
            {



                await TryRefreshSessionAsync();


                var currentUser = _supabase.Auth.CurrentUser;
                return currentUser != null;
            }
            catch
            {
                return false;
            }
        }

        public Session? GetSession()
        {
            return _supabase.Auth.CurrentSession;
        }

        public async Task<bool> TryRefreshSessionAsync()
        {
            var refreshToken = GetStoredRefreshToken();
            if (string.IsNullOrEmpty(refreshToken)) return false;

            try
            {
                var response = await _supabase.Auth.RefreshSession();
                if (response?.AccessToken != null)
                {
                    StoreSession(response.AccessToken, response.RefreshToken);
                    return true;
                }
            }
            catch
            {
                ClearSession();
            }

            return false;
        }

        public async Task SignOutAsync()
        {
            try
            {
                await _supabase.Auth.SignOut();
            }
            catch (Exception EX)
            {

                Console.WriteLine(EX.Message);
            }

            ClearSession();
        }

        public async Task<UserDto?> UpdateUserAsync(Dictionary<string, object> attributes)
        {
            var accessToken = GetStoredAccessToken();
            if (string.IsNullOrEmpty(accessToken)) return null;

            var userAttributes = new UserAttributes
            {
                Data = attributes
            };

            var user = await _supabase.Auth.Update(userAttributes);
            return user != null ? MapToUserDto(user) : null;
        }

        public async Task<bool> ResetPasswordForEmailAsync(string email)
        {
            try
            {
                await _supabase.Auth.ResetPasswordForEmail(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string?> GetAccessTokenAsync()
        {
            var s = _supabase.Auth.CurrentSession;
            if (!string.IsNullOrEmpty(s?.AccessToken))
                return s.AccessToken;
            await TryRefreshSessionAsync();
            return _supabase.Auth.CurrentSession?.AccessToken;
        }

        private void StoreSession(string? accessToken, string? refreshToken)
        {
            if (!string.IsNullOrEmpty(accessToken))
                _cachedAccessToken = accessToken;

            if (!string.IsNullOrEmpty(refreshToken))
                _cachedRefreshToken = refreshToken;


        }

        private string? GetStoredAccessToken()
        {
            var session = _supabase.Auth.CurrentSession;
            if (!string.IsNullOrEmpty(session?.AccessToken))
                return session.AccessToken;

            return _cachedAccessToken;
        }

        private string? GetStoredRefreshToken()
        {
            var session = _supabase.Auth.CurrentSession;
            if (!string.IsNullOrEmpty(session?.RefreshToken))
                return session.RefreshToken;

            return _cachedRefreshToken;
        }

        private void ClearSession()
        {
            _cachedAccessToken = null;
            _cachedRefreshToken = null;
        }

        private UserDto MapToUserDto(User user)
        {
            var metadata = user.UserMetadata ?? new Dictionary<string, object>();

            var userModel = new TshirtMaker.Models.Core.User
            {
                Id = Guid.Parse(user.Id!),
                Email = user.Email ?? string.Empty,
                Username = metadata.TryGetValue("username", out var username)
                    ? username?.ToString() ?? string.Empty
                    : user.Email?.Split('@')[0] ?? string.Empty,
                AvatarUrl = metadata.TryGetValue("avatar_url", out var avatarUrl)
                    ? avatarUrl?.ToString() ?? string.Empty
                    : "https://api.dicebear.com/7.x/avataaars/svg?seed=",
                Bio = metadata.TryGetValue("bio", out var bio)
                    ? bio?.ToString()
                    : null,
                Location = metadata.TryGetValue("location", out var location)
                    ? location?.ToString()
                    : null,
                WebsiteUrl = metadata.TryGetValue("website_url", out var websiteUrl)
                    ? websiteUrl?.ToString()
                    : null,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt ?? DateTime.UtcNow,
                LastLoginAt = user.LastSignInAt ?? DateTime.UtcNow
            };

            return UserDto.FromModel(userModel);
        }
    }
}


