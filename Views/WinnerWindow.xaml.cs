using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.ApplicationModel.DataTransfer;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EscapeRoom.Views
{
    /// <summary>
    /// Winner window displayed after successfully answering 3 questions
    /// </summary>
    public sealed partial class WinnerWindow : Page
    {
        private const string ContactEmail = "contact@escaperoom.com";
        private const string EmailSubject = "Chúc mừng! - Hoàn thành Escape Room";
        private const string EmailBody = "Xin chào,\n\nTôi vừa hoàn thành thử thách Escape Room và muốn liên hệ với bạn.\n\nTrân trọng,";

        public WinnerWindow()
        {
            InitializeComponent();
            EmailTextBlock.Text = ContactEmail;
        }

        /// <summary>
        /// Copy email address to clipboard
        /// </summary>
        private async void CopyEmailButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Copy email to clipboard
                DataPackage dataPackage = new DataPackage();
                dataPackage.SetText(ContactEmail);
                Clipboard.SetContent(dataPackage);

                // Show success InfoBar
                CopySuccessInfoBar.IsOpen = true;

                // Auto-hide InfoBar after 3 seconds
                await Task.Delay(3000);
                CopySuccessInfoBar.IsOpen = false;
            }
            catch (Exception ex)
            {
                // Show error dialog if copy fails
                await ShowErrorDialog("Không thể copy email", ex.Message);
            }
        }

        /// <summary>
        /// Open Gmail compose window with pre-filled email
        /// </summary>
        private async void GmailButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string gmailUrl = $"https://mail.google.com/mail/?view=cm&fs=1&to={ContactEmail}&su={Uri.EscapeDataString(EmailSubject)}&body={Uri.EscapeDataString(EmailBody)}";
                
                var uri = new Uri(gmailUrl);
                var success = await Windows.System.Launcher.LaunchUriAsync(uri);

                if (!success)
                {
                    await ShowErrorDialog("Lỗi", "Không thể mở Gmail. Vui lòng kiểm tra trình duyệt của bạn.");
                }
            }
            catch (Exception ex)
            {
                await ShowErrorDialog("Lỗi khi mở Gmail", ex.Message);
            }
        }

        /// <summary>
        /// Open Outlook compose window with pre-filled email
        /// </summary>
        private async void OutlookButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string outlookUrl = $"https://outlook.office.com/mail/0/deeplink/compose?to={ContactEmail}&subject={Uri.EscapeDataString(EmailSubject)}&body={Uri.EscapeDataString(EmailBody)}";
                
                var uri = new Uri(outlookUrl);
                var success = await Windows.System.Launcher.LaunchUriAsync(uri);

                if (!success)
                {
                    await ShowErrorDialog("Lỗi", "Không thể mở Outlook. Vui lòng kiểm tra trình duyệt của bạn.");
                }
            }
            catch (Exception ex)
            {
                await ShowErrorDialog("Lỗi khi mở Outlook", ex.Message);
            }
        }

        /// <summary>
        /// Close the winner window
        /// </summary>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back or close the window
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
            else
            {
                // Close the app if needed
                Application.Current.Exit();
            }
        }

        /// <summary>
        /// Show error dialog
        /// </summary>
        private async Task ShowErrorDialog(string title, string message)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };

            await dialog.ShowAsync();
        }
    }
}
