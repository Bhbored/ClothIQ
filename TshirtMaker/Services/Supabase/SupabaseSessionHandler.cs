using Supabase.Gotrue;
using Supabase.Gotrue.Interfaces;
using Microsoft.JSInterop;
using System.Text.Json;

namespace TshirtMaker.Services.Supabase
{
    // Session handler for WASM that persists sessions in browser storage
    public class SupabaseSessionHandler : IGotrueSessionPersistence<Session>, IDisposable
    {
        private readonly IJSRuntime _jsRuntime;
        private const string SessionKey = "supabase_session";
        private volatile Session? _currentSession = null;
        private readonly object _lock = new object();

        public SupabaseSessionHandler(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
            // Attempt to load session on initialization
            _ = LoadSessionInternalAsync(); // Fire and forget initial load
        }

        private async Task SaveSessionInternalAsync(Session session)
        {
            if (session != null)
            {
                try
                {
                    var sessionJson = JsonSerializer.Serialize(session);
                    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", SessionKey, sessionJson);
                    
                    // Also keep in-memory cache synchronized
                    lock (_lock)
                    {
                        _currentSession = session;
                    }
                }
                catch (JSException)
                {
                    // Handle cases where localStorage is not available
                    lock (_lock)
                    {
                        _currentSession = session;
                    }
                }
            }
        }

        private async Task DestroySessionInternalAsync()
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", SessionKey);
                
                // Also clear in-memory cache
                lock (_lock)
                {
                    _currentSession = null;
                }
            }
            catch (JSException)
            {
                // Handle cases where localStorage is not available
                lock (_lock)
                {
                    _currentSession = null;
                }
            }
        }

        private async Task<Session?> LoadSessionInternalAsync()
        {
            try
            {
                var sessionJson = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", SessionKey);
                if (string.IsNullOrEmpty(sessionJson))
                    return null;

                var session = JsonSerializer.Deserialize<Session>(sessionJson);
                
                // Update in-memory cache
                lock (_lock)
                {
                    _currentSession = session;
                }
                
                return session;
            }
            catch (JSException)
            {
                lock (_lock)
                {
                    return _currentSession;
                }
            }
            catch (JsonException)
            {
                // If JSON deserialization fails, remove the corrupted session
                try
                {
                    await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", SessionKey);
                }
                catch (JSException)
                {
                    // Ignore errors during cleanup
                }
                
                lock (_lock)
                {
                    return _currentSession;
                }
            }
        }

        public async Task SaveSessionAsync(Session session)
        {
            await SaveSessionInternalAsync(session);
        }

        public async Task DestroySessionAsync()
        {
            await DestroySessionInternalAsync();
        }

        public async Task<Session?> LoadSessionAsync()
        {
            return await LoadSessionInternalAsync();
        }

        public void SaveSession(Session session)
        {
            // For synchronous calls, update in-memory cache immediately and save to storage async
            lock (_lock)
            {
                _currentSession = session;
            }
            
            // Fire and forget the async storage operation
            _ = SaveSessionInternalAsync(session);
        }

        public void DestroySession()
        {
            // For synchronous calls, clear in-memory cache immediately and destroy storage async
            lock (_lock)
            {
                _currentSession = null;
            }
            
            // Fire and forget the async storage operation
            _ = DestroySessionInternalAsync();
        }

        public Session? LoadSession()
        {
            // For synchronous calls, return from in-memory cache
            lock (_lock)
            {
                return _currentSession;
            }
        }

        public void Dispose()
        {
            // Cleanup if needed
        }
    }
}
