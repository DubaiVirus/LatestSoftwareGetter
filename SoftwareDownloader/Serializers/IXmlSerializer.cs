using System.Threading.Tasks;

namespace SoftwareDownloader.Serializers
{
    public interface IXmlSerializer
    {
        Task SaveConfigAsync(object obj, string path);
        Task<T> LoadConfigAsync<T>(string path);
    }
}