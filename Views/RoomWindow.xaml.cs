using EscapeRoom.Data;
using EscapeRoom.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System;

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
                    new RoomWindow(_index + 1).Activate();
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
        }
    }
}
