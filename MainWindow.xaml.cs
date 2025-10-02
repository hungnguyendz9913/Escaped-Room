using EscapeRoom.Services;
using EscapeRoom.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Animation;
using System.Threading.Tasks;
using static EscapeRoom.Services.WindowPlacementServiceClass;

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
            WindowPlacementService.Save(this);
            var next = new RoomWindow(0);
            WindowPlacementService.Restore(next);
            next.Activate();
            this.Close();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
