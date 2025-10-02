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

        private void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainWindow));
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
