namespace MissioServer.Services.Services
{
    public interface IWebClientService
    {
        byte[] DownloadData(string address);
    }
}