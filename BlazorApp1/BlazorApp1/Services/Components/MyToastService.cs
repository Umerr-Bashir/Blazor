using BlazorBootstrap;

namespace BlazorApp1.Services.Components
{
    public class MyToastService
    {
        public event Action<ToastMessage>? OnShow;

        public void ShowToast(ToastType type, string title, string message)
        {
            var toast = new ToastMessage
            {
                Type = type,
                Title = title,
                Message = message,
                HelpText = DateTime.Now.ToString("hh:mm tt")
            };

            OnShow?.Invoke(toast);
        }

        public void Success(string message, string title = "Success") => ShowToast(ToastType.Success, title, message);
        public void Error(string message, string title = "Error") => ShowToast(ToastType.Danger, title, message);
        public void Info(string message, string title = "Info") => ShowToast(ToastType.Info, title, message);
        public void Warning(string message, string title = "Warning") => ShowToast(ToastType.Warning, title, message);
    }

}
