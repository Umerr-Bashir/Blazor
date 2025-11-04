using Blazored.LocalStorage;

namespace BlazorApp1.Services
{
    public class AuthState
    {
        public event Action? OnChange;
        private readonly ILocalStorageService _localStorage;

        public AuthState(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public bool IsLoggedIn { get; set; }
        public string? Token { get; set; } 

        public async Task InitializeAsync()
        {
            Token = await _localStorage.GetItemAsync<string>("authToken");
            IsLoggedIn = !string.IsNullOrEmpty(Token);
            NotifyStateChanged();
        }

        public async Task SetTokenAsync(string token)
        {
            await _localStorage.SetItemAsync("authToken", token);
            Token = token;
            IsLoggedIn = true;
            NotifyStateChanged();
        }

        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync("authToken");
            Token = null;
            IsLoggedIn = false;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
