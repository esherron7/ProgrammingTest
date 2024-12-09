using System;
using System.IO;
using System.Linq;

namespace MarioKart
{
    public class RaceReport
    {
        private PlayersMenu playersMenu;
        private KartsFile kartsFile;

        public RaceReport()
        {
            playersMenu = new PlayersMenu();
            kartsFile = new KartsFile();
            playersMenu.GetAllResultsFromFile();
            kartsFile.GetAllKartsFromFile();
        }

        public void ViewDailyKartRaceReport()
        {
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            var dailyResults = playersMenu.raceResults.Where(r => r.GetRaceDate() == today).ToArray();

            Console.WriteLine("Daily Kart Race Report:");
            if (dailyResults.Length == 0)
            {
                Console.WriteLine("No races were recorded today.");
            }
            else
            {
                foreach (var result in dailyResults)
                {
                    Console.WriteLine(result.ToString());
                }
            }

            SaveReportToFile(dailyResults, "DailyRaceReport");
        }

        public void ViewKartsCurrentlyInRace()
        {
            var inRaceKarts = playersMenu.raceResults.Where(r => !r.GetKartReturned()).ToArray();

            Console.WriteLine("Karts Currently Being Used in a Race:");
            if (inRaceKarts.Length == 0)
            {
                Console.WriteLine("No karts are currently being used in a race.");
            }
            else
            {
                foreach (var result in inRaceKarts)
                {
                    Console.WriteLine(result.ToString());
                }
            }

            SaveReportToFile(inRaceKarts, "KartsCurrentlyInRace");
        }

        public void ViewAverageRaceResultsByKartSize()
        {
            var groupedResults = playersMenu.raceResults
                .Where(r => r != null && kartsFile.karts[kartsFile.FindID(r.GetKartID())] != null)
                .GroupBy(r => kartsFile.karts[kartsFile.FindID(r.GetKartID())].GetSize())
                .Select(g => new
                {
                    Size = g.Key,
                    AverageTime = g.Average(r => r.GetTimeElapsed())
                });

            Console.WriteLine("Average Race Results by Kart Size:");
            foreach (var group in groupedResults)
            {
                Console.WriteLine($"{group.Size} karts: {group.AverageTime} seconds");
            }

            SaveReportToFile(groupedResults.ToArray(), "AverageRaceResultsByKartSize");
        }

        public void ViewTop5KartsInTournament()
        {
            var topKarts = playersMenu.raceResults
                .Where(r => r != null)
                .GroupBy(r => r.GetKartID())
                .OrderByDescending(g => g.Count())
                .Take(5)
                .Select(g => new
                {
                    KartID = g.Key,
                    RaceCount = g.Count()
                });

            Console.WriteLine("Top 5 Karts Used in the Tournament:");
            foreach (var kart in topKarts)
            {
                Console.WriteLine($"Kart ID: {kart.KartID}, Races: {kart.RaceCount}");
            }

            SaveReportToFile(topKarts.ToArray(), "Top5KartsInTournament");
        }

        private void SaveReportToFile<T>(T[] reportData, string reportName)
        {
            Console.WriteLine("\nWould you like to save this report to a file? (Y/N)");
            string choice = Console.ReadLine();
            if (choice.ToUpper() == "Y")
            {
                string fileName = $"{reportName}_{DateTime.Now.ToString("yyyyMMdd")}.txt";
                using (StreamWriter outFile = new StreamWriter(fileName))
                {
                    outFile.WriteLine($"{reportName} Report:");
                    foreach (var data in reportData)
                    {
                        outFile.WriteLine(data.ToString());
                    }
                }
                Console.WriteLine($"Report saved to {fileName}");
            }
        }
    }
}