using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using EscapeRoom;

namespace EscapeRoom.Views
{
    public sealed partial class EndWindow : Page
    {
        public EndWindow()
        {
            InitializeComponent();
            DeadEndImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/dead_end.png"));
        }

        private void toMainWindow_button(object sender, RoutedEventArgs e)
        {
            var frame = new Frame();
            frame.Navigate(typeof(MainWindow));
        }

        private void exit_button(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }  
    }
}
