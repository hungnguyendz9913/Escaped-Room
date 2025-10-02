using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System;
using System.Runtime.InteropServices;
using Windows.Graphics;

namespace EscapeRoom.Services
{
    public static class WindowPlacementServiceClass
    {
        public static class WindowPlacementService
        {
            private static double? _left;
            private static double? _top;
            private static double? _width;
            private static double? _height;

            /// <summary>
            /// Save position and size of the current window
            /// </summary>
            public static void Save(Window window)
            {
                var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
                if (GetWindowRect(hwnd, out RECT rect))
                {
                    _left = rect.Left;
                    _top = rect.Top;
                    _width = rect.Right - rect.Left;
                    _height = rect.Bottom - rect.Top;
                }
            }

            /// <summary>
            /// Restore position and size for a new window
            /// </summary>
            public static void Restore(Window window)
            {
                if (_left.HasValue && _top.HasValue && _width.HasValue && _height.HasValue)
                {
                    var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
                    MoveWindow(hwnd, (int)_left.Value, (int)_top.Value,
                               (int)_width.Value, (int)_height.Value, true);
                }
            }

            #region PInvoke
            [DllImport("user32.dll")]
            private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

            [DllImport("user32.dll")]
            private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

            private struct RECT
            {
                public int Left;
                public int Top;
                public int Right;
                public int Bottom;
            }
            #endregion
        }
    }
}
