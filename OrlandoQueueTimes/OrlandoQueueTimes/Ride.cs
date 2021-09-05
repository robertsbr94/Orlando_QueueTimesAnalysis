using System;
using System.Xml;

namespace OrlandoQueueTimes
{
    public class Ride
    {
        public readonly string Name;
        public readonly Boolean IsOpen;
        private int waitTime;
        public Ride(XmlElement ride)
        {
            var nav = ride.CreateNavigator();
            this.Name = nav.SelectSingleNode("name").Value;
            this.IsOpen = nav.SelectSingleNode("is_open").Value == "true";
            this.waitTime = Convert.ToInt32(nav.SelectSingleNode("wait_time").Value);
        }

        public int GetQueueTime()
        {
            return this.waitTime;
        }
    }
}
