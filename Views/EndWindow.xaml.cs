using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System;

namespace EscapeRoom.Views
{
    public sealed partial class EndWindow : Page
    {
        public EndWindow()
        {
            InitializeComponent();
            DeadEndImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/dead_end.png"));
        }
    }
}
