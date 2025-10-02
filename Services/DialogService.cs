using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;

namespace EscapeRoom.Services
{
    /// <summary>
    /// Service for handling dialog operations
    /// </summary>
    public class DialogService : IDialogService
    {
        /// <summary>
        /// Show error dialog with title and message
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="message">Dialog message</param>
        /// <param name="xamlRoot">XamlRoot for the dialog</param>
        /// <returns>Task representing the async operation</returns>
        public async Task ShowErrorDialogAsync(string title, string message, XamlRoot? xamlRoot = null)
        {
            try
            {
                var dialog = new ContentDialog
                {
                    Title = title,
                    Content = message,
                    CloseButtonText = "OK",
                    XamlRoot = xamlRoot
                };

                await dialog.ShowAsync();
            }
            catch (Exception)
            {
                // Fallback: log error or show system notification
            }
        }
    }

    /// <summary>
    /// Interface for dialog service
    /// </summary>
    public interface IDialogService
    {
        Task ShowErrorDialogAsync(string title, string message, XamlRoot? xamlRoot = null);
    }
}