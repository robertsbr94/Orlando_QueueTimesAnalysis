using System.Xml;
using System.Xml.Linq;

namespace OrlandoQueueTimes
{
    static class MiscMethods
    {
        public static XmlDocument xDocumentToXmlDocument(XDocument xDocument)
        {
            XmlDocument xmlDocument = new XmlDocument();
            using (XmlReader xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }
            return xmlDocument;
        }
    }
}
