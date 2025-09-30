using Microsoft.UI.Xaml;
using EscapeRoom.Views;
using EscapeRoom.Services;

namespace EscapeRoom
{
    public sealed partial class MainWindow : Window
    {

        public MainWindow()
        {
            this.InitializeComponent();
            WindowResizeService.Resize(this, 1000, 800);
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            new RoomWindow(0).Activate();
            this.Close();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
