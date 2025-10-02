using EscapeRoom.Data;
using EscapeRoom.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using System;

namespace EscapeRoom.Views
{
    public sealed partial class RoomWindow : Page
    {
        private int _index;

        public RoomWindow()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _index = e.Parameter is int i ? i : 0;
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

        private void AnswerA_Click(object s, RoutedEventArgs e) => SelectAnswer("Abcdef");
        private void AnswerB_Click(object s, RoutedEventArgs e) => SelectAnswer("Bcdefg");
        private void AnswerC_Click(object s, RoutedEventArgs e) => SelectAnswer("Cdefgh");

        private async void SelectAnswer(string choiceIndex)
        {
            var p = PuzzleRepo.Puzzles[_index];
            bool correct = Sha256Hasher.VerifyHex(choiceIndex, p.CorrectIndex);

            if (!correct)
            {
                Frame.Navigate(typeof(EndWindow));
            }
            else
            {
                if (_index + 1 < PuzzleRepo.Puzzles.Count)
                {
                    Frame.Navigate(typeof(RoomWindow), _index + 1);
                }
                else
                {
                    Frame.Navigate(typeof(WinnerWindow));
                }
            }
        }
    }
}
