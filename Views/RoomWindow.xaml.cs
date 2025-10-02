using EscapeRoom.Data;
using EscapeRoom.ManageTime;
using EscapeRoom.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Linq;

namespace EscapeRoom.Views
{
    public sealed partial class RoomWindow : Page
    {
        private int _index;
        private int _timeLeft;
        private DispatcherTimer? _timer;

        public RoomWindow()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _index = e.Parameter is int i ? i : 0;
            LoadPuzzle();
            StartCountdown();
        }

        private void LoadPuzzle()
        {
            var p = PuzzleRepo.Puzzles[_index];

            PuzzleImage.Source = new BitmapImage(new Uri($"ms-appx:///{p.ImagePath ?? ""}"));
            PuzzleText.Text = p.Question ?? "";

            BtnAText.Text = p.Options.ElementAtOrDefault(0) ?? "";
            BtnBText.Text = p.Options.ElementAtOrDefault(1) ?? "";
            BtnCText.Text = p.Options.ElementAtOrDefault(2) ?? "";
        }

        private void StartCountdown()
        {
            _timeLeft = 60;
            AnalogClockControl.SetCountdown(_timeLeft);

            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object? sender, object e)
        {
            _timeLeft--;

            if (_timeLeft >= 0)
                AnalogClockControl.SetCountdown(_timeLeft);

            if (_timeLeft <= 0)
            {
                _timer?.Stop();
                TimeUp();
            }
        }

        private void TimeUp()
        {
            Frame?.Navigate(typeof(EndWindow));
        }

        private void AnswerA_Click(object s, RoutedEventArgs e) => SelectAnswer("Abcdef");
        private void AnswerB_Click(object s, RoutedEventArgs e) => SelectAnswer("Bcdefg");
        private void AnswerC_Click(object s, RoutedEventArgs e) => SelectAnswer("Cdefgh");

        private void SelectAnswer(string choiceIndex)
        {
            _timer?.Stop();

            var p = PuzzleRepo.Puzzles[_index];
            if (p == null)
            {
                Frame?.Navigate(typeof(EndWindow));
                return;
            }

            bool correct = Sha256Hasher.VerifyHex(choiceIndex, p.CorrectIndex ?? "");

            if (!correct)
            {
                Frame?.Navigate(typeof(EndWindow));
            }
            else
            {
                if (_index + 1 < PuzzleRepo.Puzzles.Count)
                    Frame?.Navigate(typeof(RoomWindow), _index + 1);
                else
                    Frame?.Navigate(typeof(WinnerWindow));
            }
        }
    }
}
