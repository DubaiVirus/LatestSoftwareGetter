using System;

namespace SoftwareDownloader.Model
{
    [Serializable]
    public class Download
    {
        public string Name { get; set; }
        public string Link { get; set; }
    }
}