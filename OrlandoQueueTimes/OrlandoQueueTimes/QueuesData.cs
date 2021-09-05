using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace OrlandoQueueTimes { 
	public class QueuesData
	{

		private XmlDocument parksData;
		public DateTime floridaTime;
		public Dictionary<string, Park> parks;

		public QueuesData()
		{
			string parksDataString = @"<Parks>
			  <Park Name='EPCOT' SeqNo='5'/>
			  <Park Name='Magic Kingdom' SeqNo='6'/>
			  <Park Name='MGM Studios' SeqNo='7'/>
			  <Park Name='Animal Kingdom' SeqNo='8'/>
			  <Park Name='Seaworld' SeqNo='21'/>
			  <Park Name='Busch Gardens' SeqNo='24'/>
			  <Park Name='Island of Adventure' SeqNo='64'/>
			  <Park Name='Universal Studios' SeqNo='65'/>
			</Parks>";
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
