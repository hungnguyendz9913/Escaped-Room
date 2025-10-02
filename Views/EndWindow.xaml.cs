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
<<<<<<< HEAD
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(MainWindow));
            }
=======
            Frame.Navigate(typeof(MainWindow));
>>>>>>> cedd46ba71384b1466809f69f13ae252b5b950e7
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
