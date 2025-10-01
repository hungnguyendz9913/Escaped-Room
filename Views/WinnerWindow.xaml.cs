using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using EscapeRoom.Services;
using EscapeRoom.ViewModel;

namespace EscapeRoom.Views
{ 
    /// <summary>
    /// Winner window displayed after successfully answering 3 questions
    /// Following MVVM pattern with services
    /// </summary>
    public sealed partial class WinnerWindow : Page
    {
        public WinnerViewModel ViewModel { get; private set; } = null!;

        public WinnerWindow()
        {
            InitializeComponent();
            InitializeViewModel();
        }

        /// <summary>
        /// Initialize ViewModel with dependency injection
        /// </summary>
        private void InitializeViewModel()
        {
            // Create service instances (in real app, use DI container)
            var clipboardService = new ClipboardService();
            var emailService = new EmailService();
            var dialogService = new DialogService();

            // Create ViewModel with services
            ViewModel = new WinnerViewModel(clipboardService, emailService, dialogService);
            
            // Set DataContext for data binding
            DataContext = ViewModel;

            // Subscribe to ViewModel events
            ViewModel.CloseRequested += OnCloseRequested;

            // Set XamlRoot for DialogService
            ViewModel.SetXamlRoot(this.XamlRoot);
        }

        /// <summary>
        /// Handle ViewModel close request
        /// </summary>
        private void OnCloseRequested()
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
        /// Copy email button click handler - delegates to ViewModel
        /// </summary>
        private void CopyEmailButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.CopyEmailCommand.CanExecute(null))
            {
                ViewModel.CopyEmailCommand.Execute(null);
            }
        }

        /// <summary>
        /// Gmail button click handler - delegates to ViewModel
        /// </summary>
        private void GmailButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.OpenGmailCommand.CanExecute(null))
            {
                ViewModel.OpenGmailCommand.Execute(null);
            }
        }

        /// <summary>
        /// Outlook button click handler - delegates to ViewModel
        /// </summary>
        private void OutlookButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.OpenOutlookCommand.CanExecute(null))
            {
                ViewModel.OpenOutlookCommand.Execute(null);
            }
        }

        /// <summary>
        /// Close button click handler - delegates to ViewModel
        /// </summary>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.CloseCommand.CanExecute(null))
            {
                ViewModel.CloseCommand.Execute(null);
            }
        }

        /// <summary>
        /// Page unloaded - cleanup event subscriptions and resources
        /// </summary>
        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            if (ViewModel != null)
            {
                ViewModel.CloseRequested -= OnCloseRequested;
                ViewModel.Dispose();
            }
        }

        /// <summary>
        /// Override OnNavigatedTo to ensure ViewModel is properly initialized
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            
            // Update dialog service with current XamlRoot for proper dialog display
            if (ViewModel != null)
            {
                // The DialogService would need XamlRoot context, 
                // but we keep it simple for now
            }
        }
    }
}
