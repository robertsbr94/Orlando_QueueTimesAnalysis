using System;
using Aspose.Cells;

namespace OrlandoQueueTimes { 
	public class SpreadsheetBuilder
	{
		public Workbook spreadsheet;
		private readonly ConfigHandler config;
		private readonly QueuesData queuesDataObj;
		public SpreadsheetBuilder(QueuesData queuesDataObj)
		{
			this.config = new ConfigHandler();
			string inSpreadsheetPath = config.ConfigFile.SelectSingleNode("/Config/SpreadsheetConfig/Template/@Input").Value;
			this.spreadsheet = new Workbook(@$"{inSpreadsheetPath}");
			this.queuesDataObj = queuesDataObj;
		}

		public void BuildWorksheet()
		{
			Console.Write("Populating spreadshet...");
			if (this.spreadsheet.Worksheets["Evaluation Warning"] != null)
				this.spreadsheet.Worksheets.RemoveAt(this.spreadsheet.Worksheets.Count - 1);
			string worksheetName = this.queuesDataObj.floridaTime.GetDateTimeFormats()[8];
			Worksheet currentWorksheet;
			if (this.spreadsheet.Worksheets[worksheetName] == null)
			{
				this.spreadsheet.Worksheets.AddCopy("Example");
				currentWorksheet = this.spreadsheet.Worksheets[this.spreadsheet.Worksheets.Count - 1];
				currentWorksheet.Name = worksheetName;
			}
			else
			{
				currentWorksheet = this.spreadsheet.Worksheets[worksheetName];
			}
			int rowNum = 2;
			int colNum = this.queuesDataObj.floridaTime.Hour switch
			{
				9 => 5,
				12 => 6,
				15 => 7,
				18 => 8,
				_ => 5,
			};

			foreach (var park in this.queuesDataObj.parks)
			{
				string parkName = park.Key;
				foreach (var land in this.queuesDataObj.GetPark(parkName).Lands)
				{
					string landName = land.Key;
					foreach (var ride in this.queuesDataObj.GetPark(parkName).GetLand(landName).Rides)
					{
						string rideName = ride.Key;
						currentWorksheet.Cells[rowNum, 1].Value = parkName;
						currentWorksheet.Cells[rowNum, 2].Value = landName;
						currentWorksheet.Cells[rowNum, 3].Value = rideName;
						if (ride.Value.IsOpen)
						{
							currentWorksheet.Cells[rowNum, 4].Value = "Yes";
						}
						else
						{
							currentWorksheet.Cells[rowNum, 4].Value = "No";
						}
						currentWorksheet.Cells[rowNum, colNum].Value = ride.Value.GetQueueTime();
						rowNum++;
					}
				}
				
				foreach (var ride in this.queuesDataObj.GetPark(parkName).Rides)
				{
					string rideName = ride.Key;
					currentWorksheet.Cells[rowNum, 1].Value = parkName;
					currentWorksheet.Cells[rowNum, 2].Value = "";
					currentWorksheet.Cells[rowNum, 3].Value = rideName;
					if (ride.Value.IsOpen)
					{
						currentWorksheet.Cells[rowNum, 4].Value = "Yes";
					}
					else
					{
						currentWorksheet.Cells[rowNum, 4].Value = "No";
					}

					
					
					currentWorksheet.Cells[rowNum, colNum].Value = ride.Value.GetQueueTime();
					rowNum++;
				}
			}
			Console.WriteLine("Complete!\n");
			Console.Write("Saving Spreadsheet...");
			string outSpreadsheetPath = this.config.ConfigFile.SelectSingleNode("/Config/SpreadsheetConfig/Template/@Output").Value;
			this.spreadsheet.Save($@"{outSpreadsheetPath}");
			Console.WriteLine("Complete!\n");
			Console.WriteLine($"Spreadsheet saved to {outSpreadsheetPath}");
		}
	}
}