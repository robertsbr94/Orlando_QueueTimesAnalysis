using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Xml;

namespace OrlandoQueueTimes
{
    public class Park
    {
        public readonly string Name;
        private int seqNo;
        private XmlDocument response;
        public readonly Dictionary<String, Land> Lands;
        public readonly Dictionary<String, Ride> Rides;

        public Park(int seqNo, string Name)
        {
            this.Name = Name;
            this.seqNo = seqNo;            
            this.response = GetQueueResponse();
            this.Lands = new Dictionary<string, Land>();
            foreach (XmlElement land in this.response.SelectNodes("Root/lands"))
            {
                var nav = land.CreateNavigator();
                string landName = nav.SelectSingleNode("name").Value;
                this.Lands.Add(landName, new Land(landName, this.response));
            }
            this.Rides = new Dictionary<string, Ride>();
            foreach (XmlElement ride in this.response.SelectNodes("Root/rides"))
            {
                var nav = ride.CreateNavigator();
                string parkName = nav.SelectSingleNode("name").Value;
                this.Rides.Add(parkName, new Ride(ride));
            }
        }

        public Land GetLand(string landName)
        {
            return this.Lands[landName];
        }
        private XmlDocument GetQueueResponse()
        {
            using HttpClient queueClient = new HttpClient();
            HttpResponseMessage queueResponse = queueClient.GetAsync($"https://queue-times.com/parks/{this.seqNo}/queue_times.json").Result;

            if (queueResponse.IsSuccessStatusCode)
            {
                HttpContent responseContent = queueResponse.Content;

                string responseString = responseContent.ReadAsStringAsync().Result;

                return MiscMethods.xDocumentToXmlDocument(JsonConvert.DeserializeXNode(responseString, "Root"));
            }
            else
            {
                return null;
            }
        }
    }
}
