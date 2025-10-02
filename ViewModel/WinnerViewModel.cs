using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using EscapeRoom.Services;
using EscapeRoom.ViewModel;
using CommunityToolkit.Mvvm.Input;

namespace EscapeRoom.ViewModel
{
    /// <summary>
    /// ViewModel for Winner Window following MVVM pattern
    /// </summary>
    public class WinnerViewModel : INotifyPropertyChanged
    {
        private readonly IClipboardService _clipboardService;
        private readonly IEmailService _emailService;
        private readonly IDialogService _dialogService;

        private bool _isCopySuccessVisible;
        private string _contactEmail = string.Empty;
        private string _emailSubject = string.Empty;
        private string _emailBody = string.Empty;
        private XamlRoot? _xamlRoot;
        private CancellationTokenSource? _autoHideCancellationTokenSource;
        private int _copySuccessCounter = 0;

        public WinnerViewModel(
            IClipboardService clipboardService,
            IEmailService emailService,
            IDialogService dialogService)
        {
            _clipboardService = clipboardService ?? throw new ArgumentNullException(nameof(clipboardService));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            // Initialize default values
            // TODO: Replace "changeHere" with the actual base64-encoded key
            ContactEmail = AesGcmHelper.DecryptFromBase64(AesGcmHelper.getActualKey("changeHere"), AesGcmHelper.EncryptedEmail);
            EmailSubject = "Chúc mừng! - Hoàn thành Escape Room";
            EmailBody = "Xin chào,\n\nTôi vừa hoàn thành thử thách Escape Room và muốn liên hệ với bạn.\n\nTrân trọng,";

            // Initialize commands
            CopyEmailCommand = new RelayCommand(async () => await CopyEmailAsync());
            OpenGmailCommand = new RelayCommand(async () => await OpenGmailAsync());
            OpenOutlookCommand = new RelayCommand(async () => await OpenOutlookAsync());
            CloseCommand = new RelayCommand(CloseWindow);
        }

        #region Properties

        /// <summary>
        /// Contact email address
        /// </summary>
        public string ContactEmail
        {
            get => _contactEmail;
            set => SetProperty(ref _contactEmail, value);
        }

        /// <summary>
        /// Email subject template
        /// </summary>
        public string EmailSubject
        {
            get => _emailSubject;
            set => SetProperty(ref _emailSubject, value);
        }

        /// <summary>
        /// Email body template
        /// </summary>
        public string EmailBody
        {
            get => _emailBody;
            set => SetProperty(ref _emailBody, value);
        }

        /// <summary>
        /// Visibility of copy success message
        /// </summary>
        public bool IsCopySuccessVisible
        {
            get => _isCopySuccessVisible;
            set 
            { 
                if (SetProperty(ref _isCopySuccessVisible, value))
                {
                    if (!value)
                    {
                        // When InfoBar is closed, cancel auto-hide task and increment counter
                        _autoHideCancellationTokenSource?.Cancel();
                        _copySuccessCounter++; // Invalidate any pending auto-hide
                    }
                }
            }
        }

        #endregion

        #region Commands

        public ICommand CopyEmailCommand { get; }
        public ICommand OpenGmailCommand { get; }
        public ICommand OpenOutlookCommand { get; }
        public ICommand CloseCommand { get; }

        #endregion

        #region Private Methods

        /// <summary>
        /// Copy email to clipboard
        /// </summary>
        private async Task CopyEmailAsync()
        {
            try
            {
                var success = await _clipboardService.CopyTextToClipboardAsync(ContactEmail);
                
                if (success)
                {
                    // Cancel any existing auto-hide task
                    _autoHideCancellationTokenSource?.Cancel();
                    _autoHideCancellationTokenSource = new CancellationTokenSource();

                    // Ensure InfoBar shows by incrementing counter (forces property change)
                    _copySuccessCounter++;
                    
                    // Always set to true to show InfoBar
                    IsCopySuccessVisible = true;

                    // Auto-hide after 3 seconds
                    var currentCounter = _copySuccessCounter;
                    try
                    {
                        await Task.Delay(3000, _autoHideCancellationTokenSource.Token);
                        
                        // Only hide if this is still the current copy operation
                        if (_copySuccessCounter == currentCounter)
                        {
                            IsCopySuccessVisible = false;
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        // Task was cancelled, which is expected if user manually closes InfoBar
                    }
                }
                else
                {
                    await _dialogService.ShowErrorDialogAsync(
                        "Lỗi Copy", 
                        "Không thể copy email vào clipboard. Vui lòng thử lại.",
                        _xamlRoot);
                }
            }
            catch (Exception ex)
            {
                await _dialogService.ShowErrorDialogAsync(
                    "Lỗi không mong muốn", 
                    $"Đã xảy ra lỗi khi copy email: {ex.Message}",
                    _xamlRoot);
            }
        }

        /// <summary>
        /// Open Gmail compose window
        /// </summary>
        private async Task OpenGmailAsync()
        {
            try
            {
                var success = await _emailService.OpenGmailComposeAsync(ContactEmail, EmailSubject, EmailBody);
                
                if (!success)
                {
                    await _dialogService.ShowErrorDialogAsync(
                        "Lỗi mở Gmail", 
                        "Không thể mở Gmail. Vui lòng kiểm tra trình duyệt của bạn.",
                        _xamlRoot);
                }
            }
            catch (Exception ex)
            {
                await _dialogService.ShowErrorDialogAsync(
                    "Lỗi Gmail", 
                    $"Đã xảy ra lỗi khi mở Gmail: {ex.Message}",
                    _xamlRoot);
            }
        }

        /// <summary>
        /// Open Outlook compose window
        /// </summary>
        private async Task OpenOutlookAsync()
        {
            try
            {
                var success = await _emailService.OpenOutlookComposeAsync(ContactEmail, EmailSubject, EmailBody);
                
                if (!success)
                {
                    await _dialogService.ShowErrorDialogAsync(
                        "Lỗi mở Outlook", 
                        "Không thể mở Outlook. Vui lòng kiểm tra trình duyệt của bạn.",
                        _xamlRoot);
                }
            }
            catch (Exception ex)
            {
                await _dialogService.ShowErrorDialogAsync(
                    "Lỗi Outlook", 
                    $"Đã xảy ra lỗi khi mở Outlook: {ex.Message}",
                    _xamlRoot);
            }
        }

        /// <summary>
        /// Close the winner window
        /// </summary>
        private void CloseWindow()
        {
            // This would be handled by the View layer
            // The ViewModel should not directly manipulate UI navigation
            CloseRequested?.Invoke();
        }

        /// <summary>
        /// Set XamlRoot for DialogService to display dialogs properly
        /// </summary>
        /// <param name="xamlRoot">XamlRoot from the view</param>
        public void SetXamlRoot(XamlRoot? xamlRoot)
        {
            _xamlRoot = xamlRoot;
        }

        /// <summary>
        /// Cleanup resources
        /// </summary>
        public void Dispose()
        {
            _autoHideCancellationTokenSource?.Cancel();
            _autoHideCancellationTokenSource?.Dispose();
        }

        #endregion

        #region Events

        /// <summary>
        /// Event raised when close is requested
        /// </summary>
        public event Action? CloseRequested;

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion
    }
}