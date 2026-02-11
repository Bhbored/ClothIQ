using Supabase.Gotrue;
using Supabase.Gotrue.Interfaces;
using Microsoft.JSInterop;
using System.Text.Json;

namespace TshirtMaker.Services.Supabase
{

    public class SupabaseSessionHandler : IGotrueSessionPersistence<Session>, IDisposable
    {
        private readonly IJSRuntime _jsRuntime;
        private const string SessionKey = "supabase_session";
        private volatile Session? _currentSession = null;
        private readonly object _lock = new object();

        public SupabaseSessionHandler(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;

            _ = LoadSessionInternalAsync();
        }

        private async Task SaveSessionInternalAsync(Session session)
        {
            if (session != null)
            {
                try
                {
                    var sessionJson = JsonSerializer.Serialize(session);
                    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", SessionKey, sessionJson);


                    lock (_lock)
                    {
                        _currentSession = session;
                    }
                }
                catch (JSException)
                {

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


                lock (_lock)
                {
                    _currentSession = null;
                }
            }
            catch (JSException)
            {

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

                try
                {
                    await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", SessionKey);
                }
                catch (JSException)
                {

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

            lock (_lock)
            {
                _currentSession = session;
            }


            _ = SaveSessionInternalAsync(session);
        }

        public void DestroySession()
        {

            lock (_lock)
            {
                _currentSession = null;
            }


            _ = DestroySessionInternalAsync();
        }

        public Session? LoadSession()
        {

            lock (_lock)
            {
                return _currentSession;
            }
        }

        public void Dispose()
        {

        }
    }
}
