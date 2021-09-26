using System.Xml;
using System.IO;

namespace OrlandoQueueTimes
{
    public class ConfigHandler
    {
        public readonly XmlDocument ConfigFile;
        public ConfigHandler()
        {
            var configString = File.ReadAllText(@"D:\OrlandoQueueTimes\OrlandoQueueTimes\OrlandoQueueTimes\QueueTimesConfig.xml");
            this.ConfigFile = new XmlDocument();
            this.ConfigFile.Load(new StringReader(configString));
        }
    }
}
