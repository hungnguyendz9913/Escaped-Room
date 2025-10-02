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

        /// <summary>
        /// Show success dialog with title and message
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="message">Dialog message</param>
        /// <param name="xamlRoot">XamlRoot for the dialog</param>
        /// <returns>Task representing the async operation</returns>
        public async Task ShowSuccessDialogAsync(string title, string message, XamlRoot? xamlRoot = null)
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

        /// <summary>
        /// Show confirmation dialog with yes/no options
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="message">Dialog message</param>
        /// <param name="xamlRoot">XamlRoot for the dialog</param>
        /// <returns>True if user clicked Yes/Primary, false otherwise</returns>
        public async Task<bool> ShowConfirmationDialogAsync(string title, string message, XamlRoot? xamlRoot = null)
        {
            try
            {
                var dialog = new ContentDialog
                {
                    Title = title,
                    Content = message,
                    PrimaryButtonText = "Có",
                    SecondaryButtonText = "Không",
                    DefaultButton = ContentDialogButton.Primary,
                    XamlRoot = xamlRoot
                };

                var result = await dialog.ShowAsync();
                return result == ContentDialogResult.Primary;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Show custom dialog with custom buttons
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="message">Dialog message</param>
        /// <param name="primaryButtonText">Primary button text</param>
        /// <param name="secondaryButtonText">Secondary button text (optional)</param>
        /// <param name="closeButtonText">Close button text (optional)</param>
        /// <param name="xamlRoot">XamlRoot for the dialog</param>
        /// <returns>ContentDialogResult indicating which button was clicked</returns>
        public async Task<ContentDialogResult> ShowCustomDialogAsync(
            string title, 
            string message, 
            string primaryButtonText,
            string secondaryButtonText = null,
            string closeButtonText = null,
            XamlRoot? xamlRoot = null)
        {
            try
            {
                var dialog = new ContentDialog
                {
                    Title = title,
                    Content = message,
                    PrimaryButtonText = primaryButtonText,
                    XamlRoot = xamlRoot
                };

                if (!string.IsNullOrEmpty(secondaryButtonText))
                    dialog.SecondaryButtonText = secondaryButtonText;

                if (!string.IsNullOrEmpty(closeButtonText))
                    dialog.CloseButtonText = closeButtonText;

                return await dialog.ShowAsync();
            }
            catch (Exception)
            {
                return ContentDialogResult.None;
            }
        }
    }

    /// <summary>
    /// Interface for dialog service
    /// </summary>
    public interface IDialogService
    {
        Task ShowErrorDialogAsync(string title, string message, XamlRoot? xamlRoot = null);
        Task ShowSuccessDialogAsync(string title, string message, XamlRoot? xamlRoot = null);
        Task<bool> ShowConfirmationDialogAsync(string title, string message, XamlRoot? xamlRoot = null);
        Task<ContentDialogResult> ShowCustomDialogAsync(string title, string message, string primaryButtonText, 
            string secondaryButtonText = null, string closeButtonText = null, XamlRoot? xamlRoot = null);
    }
}