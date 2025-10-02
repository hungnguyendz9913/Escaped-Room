using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace EscapeRoom.ManageTime
{
    public sealed partial class AnalogClock : UserControl
    {
        private readonly DispatcherTimer _timer;
        private int _countdownSeconds;

        public AnalogClock()
        {
            this.InitializeComponent();
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += Timer_Tick;
        }

        
        public void SetCountdown(int seconds)
        {
            _countdownSeconds = seconds;

            if (_countdownSeconds > 0)
            {
                CountdownText.Text = _countdownSeconds.ToString();
                _timer.Start();
            }
            else
            {
                _timer.Stop();
            }
        }

        private void Timer_Tick(object? sender, object e)
        {
            if (_countdownSeconds > 0)
            {
                _countdownSeconds--;
                CountdownText.Text = _countdownSeconds.ToString();
            }
            else
            {
                _timer.Stop();
                // Optional: trigger "time-up" effect here
                CountdownText.Text = "0";
            }
        }
    }
}
