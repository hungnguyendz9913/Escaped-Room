using EscapeRoom.Data;
using EscapeRoom.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Threading.Tasks;
using static EscapeRoom.Services.WindowPlacementServiceClass;
using static EscapeRoom.Services.WindowResizeService;
namespace EscapeRoom.Views
{
    public sealed partial class RoomWindow : Window
    {
        private readonly int _index;

        public RoomWindow(int puzzleIndex)
        {
            this.InitializeComponent();
            WindowResizeService.Resize(this, 1000, 800);
            _index = puzzleIndex;
            LoadPuzzle();
            FadeIn();
        }

        private void LoadPuzzle()
        {
            var p = PuzzleRepo.Puzzles[_index];

            PuzzleImage.Source = new BitmapImage(new Uri($"ms-appx:///{p.ImagePath}"));
            PuzzleText.Text = p.Question;
            BtnA.Content = p.Options[0];
            BtnB.Content = p.Options[1];
            BtnC.Content = p.Options[2];
        }

        private void AnswerA_Click(object sender, RoutedEventArgs e) => SelectAnswer(0);
        private void AnswerB_Click(object sender, RoutedEventArgs e) => SelectAnswer(1);
        private void AnswerC_Click(object sender, RoutedEventArgs e) => SelectAnswer(2);

        private async void SelectAnswer(int choiceIndex)
        {
            var p = PuzzleRepo.Puzzles[_index];
            bool correct = choiceIndex == p.CorrectIndex;

            var dialog = new ContentDialog
            {
                Title = correct ? "\uD83C\uDF89 Đúng rồi!" : "\u274C Sai rồi!",
                CloseButtonText = "OK",
                XamlRoot = this.Content.XamlRoot
            };
            await dialog.ShowAsync();

            if (correct)
            {
                if (_index + 1 < PuzzleRepo.Puzzles.Count)
                {
                    await FadeOut();
                    WindowPlacementService.Save(this);
                    var next = new RoomWindow(_index + 1);
                    WindowPlacementService.Restore(next);
                    next.Activate();
                }
                else
                {
                    var winDialog = new ContentDialog
                    {
                        Title = "\uD83C\uDFC6 Thoát thành công!",
                        CloseButtonText = "Thoát",
                        XamlRoot = this.Content.XamlRoot
                    };
                    await winDialog.ShowAsync();
                }
                this.Close();
            }
            else
            {
                await FadeOut();
                WindowPlacementService.Save(this);
                var next = new EndWindow();
                WindowPlacementService.Restore(next);
                next.Activate();
                this.Close();
            }
        }
        private async Task FadeOut()
        {
            var fade = new DoubleAnimation
            {
                To = 0,
                Duration = new Duration(TimeSpan.FromMilliseconds(300))
            };

            var sb = new Storyboard();
            sb.Children.Add(fade);
            Storyboard.SetTarget(fade, RootLayout);
            Storyboard.SetTargetProperty(fade, "Opacity");
            sb.Begin();

            await Task.Delay(300);
        }

        private async void FadeIn()
        {
            RootLayout.Opacity = 0;
            await Task.Delay(100);

            var fade = new DoubleAnimation
            {
                To = 1,
                Duration = new Duration(TimeSpan.FromMilliseconds(400))
            };

            var sb = new Storyboard();
            sb.Children.Add(fade);
            Storyboard.SetTarget(fade, RootLayout);
            Storyboard.SetTargetProperty(fade, "Opacity");
            sb.Begin();
        }
    }
}
