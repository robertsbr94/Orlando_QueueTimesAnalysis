using System.Collections.Generic;
using System.Xml;

namespace OrlandoQueueTimes
{
    public class Land
    {
        public readonly string Name;
        private XmlDocument response;
        public readonly Dictionary<string, Ride> Rides;

        public Land(string landName, XmlDocument response)
        {
            this.Name = landName;
            this.response = response;
            this.Rides = new Dictionary<string, Ride>();
            foreach (XmlElement ride in this.response.SelectNodes($"/Root/lands[name=\"{this.Name}\"]/rides"))
            {
                var nav = ride.CreateNavigator();
                string parkName = nav.SelectSingleNode("name").Value;
                this.Rides.Add(parkName, new Ride(ride));
            }
        }

        public Ride GetRide(string rideName)
        {
            return this.Rides[rideName];
        }
    }
}
