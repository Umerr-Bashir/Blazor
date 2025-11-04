using Blazored.LocalStorage;

namespace BlazorApp1.Session
{
    public class SessionManager
    {
        private readonly ILocalStorageService _localStorage;
        private readonly SessionState _session;

        public SessionManager(ILocalStorageService localStorage, SessionState session)
        {
            _localStorage = localStorage;
            _session = session;
        }

        public async Task RestoreSessionAsync()
        {
            var isLoggedIn = await _localStorage.GetItemAsync<bool>("IsLoggedIn");
            if (isLoggedIn)
            {
                _session.Id = await _localStorage.GetItemAsync<int>("UserId");
                _session.Username = await _localStorage.GetItemAsync<string>("UserName");
                _session.IsLoggedIn = true;
            }
        }
    }

}
