using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System;

namespace EscapeRoom.Views
{
    public sealed partial class EndWindow : Page
    {
        public EndWindow()
        {
            this.InitializeComponent();
            DeadEndImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/dead_end.png"));
        }

        private void toMainWindow_button(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(MainWindow));
            }
        }

        private void exit_button(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
