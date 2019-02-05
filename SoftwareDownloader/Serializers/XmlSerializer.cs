using System;
using System.IO;
using System.Threading.Tasks;

namespace SoftwareDownloader.Serializers
{
    public class XmlSerializer : IXmlSerializer
    {
        public async Task SaveConfigAsync(object obj, string path)
        {
            await SerializeXmlAsync(path, obj);
        }

        public async Task<T> LoadConfigAsync<T>(string path)
        {
            var deserializedObj = await DeserializeXmlAsync<T>(path);
            return deserializedObj;
        }

        private async Task SerializeXmlAsync(string path, object obj)
        {
            try
            {
                Directory.CreateDirectory(path.Substring(0, path.LastIndexOf('\\') + 1));

                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(obj.GetType());
                using (var sw = new StreamWriter(path))
                {
                    xs.Serialize(sw, obj);
                    while (sw.BaseStream.Position < sw.BaseStream.Length)
                    {
                        await sw.WriteAsync(path);
                    }
                }
            }
            catch (Exception) { }
        }

        private async Task<T> DeserializeXmlAsync<T>(string path)
        {
            try
            {
                Directory.CreateDirectory(path.Substring(0, path.LastIndexOf('\\') + 1));

                if (!File.Exists(path))
                    return default(T);

                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
                using (var sr = new StreamReader(path))
                {
                    T deserializedObj = (T)xs.Deserialize(sr);
                    return await Task.FromResult(deserializedObj);
                }
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}