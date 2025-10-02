using Windows.ApplicationModel.DataTransfer;
using System;
using System.Threading.Tasks;

namespace EscapeRoom.Services
{
    /// <summary>
    /// Service for handling clipboard operations
    /// </summary>
    public class ClipboardService : IClipboardService
    {
        /// <summary>
        /// Copy text to clipboard
        /// </summary>
        /// <param name="text">Text to copy</param>
        /// <returns>True if successful, false otherwise</returns>
        public async Task<bool> CopyTextToClipboardAsync(string text)
        {
            try
            {
                if (string.IsNullOrEmpty(text))
                    return false;

                var dataPackage = new DataPackage();
                dataPackage.SetText(text);
                Clipboard.SetContent(dataPackage);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    /// <summary>
    /// Interface for clipboard service
    /// </summary>
    public interface IClipboardService
    {
        Task<bool> CopyTextToClipboardAsync(string text);
    }
}