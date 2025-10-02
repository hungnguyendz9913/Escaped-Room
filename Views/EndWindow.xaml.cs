using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EscapeRoom.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EndWindow : Window
    {
        public EndWindow()
        {
            InitializeComponent();
            this.Title = "Dead End!";

            DeadEndImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/dead_end.png"));
        }

        private void toMainWindow_button(object sender, RoutedEventArgs e)
        {
            new MainWindow().Activate();
            this.Close();
        }

        private void exit_button(object sender, RoutedEventArgs e)
        {
            this.Close();
        }  
    }
}
