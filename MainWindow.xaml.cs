using EscapeRoom.Data;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace EscapeRoom
{
    public sealed partial class MainWindow : Page
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private async void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            await PuzzleRepo.EnsureLoadedAsync();

            MenuPanel.Visibility = Visibility.Collapsed;
            RootFrame.Visibility = Visibility.Visible;

            RootFrame.Navigate(typeof(EscapeRoom.Views.RoomWindow), 0); // RoomWindow là Page
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
