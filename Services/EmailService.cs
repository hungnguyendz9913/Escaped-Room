using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.System;

namespace EscapeRoom.Services
{
    /// <summary>
    /// Service for handling email operations
    /// </summary>
    public class EmailService : IEmailService
    {
        private const string GmailComposeBaseUrl = "https://mail.google.com/mail/?view=cm&fs=1";
        private const string OutlookComposeBaseUrl = "https://outlook.office.com/mail/0/deeplink/compose";
        
        /// <summary>
        /// Open Gmail compose window with pre-filled data
        /// </summary>
        /// <param name="to">Recipient email</param>
        /// <param name="subject">Email subject</param>
        /// <param name="body">Email body</param>
        /// <returns>True if successfully opened, false otherwise</returns>
        public async Task<bool> OpenGmailComposeAsync(string to, string subject = "", string body = "")
        {
            try
            {
                var url = BuildGmailUrl(to, subject, body);
                var uri = new Uri(url);
                return await Launcher.LaunchUriAsync(uri);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Open Outlook compose window with pre-filled data
        /// </summary>
        /// <param name="to">Recipient email</param>
        /// <param name="subject">Email subject</param>
        /// <param name="body">Email body</param>
        /// <returns>True if successfully opened, false otherwise</returns>
        public async Task<bool> OpenOutlookComposeAsync(string to, string subject = "", string body = "")
        {
            try
            {
                var url = BuildOutlookUrl(to, subject, body);
                var uri = new Uri(url);
                return await Launcher.LaunchUriAsync(uri);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Build Gmail compose URL with parameters
        /// </summary>
        private string BuildGmailUrl(string to, string subject, string body)
        {
            var url = $"{GmailComposeBaseUrl}&to={Uri.EscapeDataString(to)}";
            
            if (!string.IsNullOrEmpty(subject))
                url += $"&su={Uri.EscapeDataString(subject)}";
            
            if (!string.IsNullOrEmpty(body))
                url += $"&body={Uri.EscapeDataString(body)}";

            return url;
        }

        /// <summary>
        /// Build Outlook compose URL with parameters
        /// </summary>
        private string BuildOutlookUrl(string to, string subject, string body)
        {
            var url = $"{OutlookComposeBaseUrl}?to={Uri.EscapeDataString(to)}";
            
            if (!string.IsNullOrEmpty(subject))
                url += $"&subject={Uri.EscapeDataString(subject)}";
            
            if (!string.IsNullOrEmpty(body))
                url += $"&body={Uri.EscapeDataString(body)}";

            return url;
        }

        /// <summary>
        /// Open default email client with pre-filled data
        /// </summary>
        /// <param name="to">Recipient email</param>
        /// <param name="subject">Email subject</param>
        /// <param name="body">Email body</param>
        /// <returns>True if successfully opened, false otherwise</returns>
        public async Task<bool> OpenDefaultEmailClientAsync(string to, string subject = "", string body = "")
        {
            try
            {
                var url = $"mailto:{Uri.EscapeDataString(to)}";
                
                var parameters = new List<string>();
                if (!string.IsNullOrEmpty(subject))
                    parameters.Add($"subject={Uri.EscapeDataString(subject)}");
                
                if (!string.IsNullOrEmpty(body))
                    parameters.Add($"body={Uri.EscapeDataString(body)}");

                if (parameters.Count > 0)
                    url += "?" + string.Join("&", parameters);

                var uri = new Uri(url);
                return await Launcher.LaunchUriAsync(uri);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    /// <summary>
    /// Interface for email service
    /// </summary>
    public interface IEmailService
    {
        Task<bool> OpenGmailComposeAsync(string to, string subject = "", string body = "");
        Task<bool> OpenOutlookComposeAsync(string to, string subject = "", string body = "");
        Task<bool> OpenDefaultEmailClientAsync(string to, string subject = "", string body = "");
    }
}