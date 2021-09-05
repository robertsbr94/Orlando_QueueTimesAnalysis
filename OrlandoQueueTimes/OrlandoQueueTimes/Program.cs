using System;

namespace OrlandoQueueTimes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Obtaining queue data...");
            QueuesData queuesObj = new QueuesData();
            Console.WriteLine("Complete!");

            Console.WriteLine("\n");
            Console.WriteLine($"Data obtained at {queuesObj.floridaTime.Hour}:{queuesObj.floridaTime.Minute} Orlando time.");
            Console.WriteLine("\n");

            SpreadsheetBuilder sSheet = new SpreadsheetBuilder(queuesObj);
            sSheet.BuildWorksheet();
        }

    }
}
