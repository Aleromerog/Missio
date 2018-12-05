namespace MissioServer.Services
{
    public interface IWebClientService
    {
        byte[] DownloadData(string address);
    }
}