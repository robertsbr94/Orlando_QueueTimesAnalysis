using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace OrlandoQueueTimes { 
	public class QueuesData
	{

		private readonly XmlDocument parksData;
		private readonly ConfigHandler config;
		public DateTime floridaTime;
		public Dictionary<string, Park> parks;

		public QueuesData()
		{
			this.config = new ConfigHandler();
			string parksDataString = config.ConfigFile.SelectSingleNode("/Config/QueueTimesConfig").InnerXml;
			this.parksData = new XmlDocument();
			this.parksData.Load(new StringReader(parksDataString));
			this.parks = new Dictionary<string, Park>();
			foreach (XmlNode park in this.parksData.SelectNodes("/Parks/Park"))
			{
				string name = park.Attributes[0].Value;
				int seqNo = Convert.ToInt32(park.Attributes[1].Value);

				this.parks.Add(name, new Park(seqNo, name));
			}
			this.floridaTime = this.GetFloridaTime();
			
		}

		private DateTime GetFloridaTime()
		{
			DateTime ukTime = DateTime.Now;
			return ukTime.AddHours(-5);
		}
		
		public Park GetPark(string parkName)
		{
			return this.parks[parkName];
		}
	}
}
