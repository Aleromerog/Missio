using System.Net;

namespace MissioServer.Services
{
    public class WebClientService : IWebClientService
    {
        /// <inheritdoc />
        public byte[] DownloadData(string address)
        {
            byte[] result;
            using (var webClient = new WebClient())
            {
                result = webClient.DownloadData(address);
            }
            return result;
        }
    }
}