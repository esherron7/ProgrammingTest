using System;
using System.IO;

namespace MarioKart
{
    public class PlayersMenu
    {
        public RaceResults[] raceResults = new RaceResults[100];
        public string[] resultIDs = new string[100];
        private static string[] tracks = { "Rainbow Road", "Bowser's Castle", "Mario Circuit", "Luigi's Mansion" };

        public PlayersMenu()
        {
            GetAllResultsFromFile();
        }

        public void GetAllResultsFromFile()
        {
            if (File.Exists("results.txt"))
            {
                using (StreamReader inFile = new StreamReader("results.txt"))
                {
                    RaceResults.SetCount(0); // Reset the count
                    string line = inFile.ReadLine();

                    while (line != null)
                    {
                        string[] temp = line.Split('#');
                        raceResults[RaceResults.GetCount()] = new RaceResults(
                            int.Parse(temp[0]),    // Interaction ID
                            temp[1],               // Player Email
                            int.Parse(temp[2]),    // Kart ID
                            temp[3],               // Race Date
                            int.Parse(temp[4]),    // Race Time
                            int.Parse(temp[5]),    // Time Elapsed
                            temp[6],               // Track
                            bool.Parse(temp[7])    // Kart Returned
                        );
                        resultIDs[RaceResults.GetCount()] = temp[0]; // Store the Interaction ID
                        RaceResults.IncCount();
                        line = inFile.ReadLine();
                    }
                }
            }
            else
            {
                Console.WriteLine("Results file not found. Starting with an empty results list.");
            }
        }

        public void SaveResultsToFile()
        {
            using (StreamWriter outFile = new StreamWriter("results.txt"))
            {
                for (int i = 0; i < RaceResults.GetCount(); i++)
                {
                    outFile.WriteLine(
                        $"{raceResults[i].GetInteractionID()}#" +
                        $"{raceResults[i].GetPlayerEmail()}#" +
                        $"{raceResults[i].GetKartID()}#" +
                        $"{raceResults[i].GetRaceDate()}#" +
                        $"{raceResults[i].GetRaceTime()}#" +
                        $"{raceResults[i].GetTimeElapsed()}#" +
                        $"{raceResults[i].GetTrack()}#" +
                        $"{raceResults[i].GetKartReturned()}"
                    );
                }
            }
        }

        public void ViewAvailableKarts(KartsFile kartsFile)
        {
            Console.WriteLine("Available Karts:");
            for (int i = 0; i < Karts.GetCount(); i++)
            {
                if (kartsFile.karts[i].GetAvailability())
                {
                    Console.WriteLine(kartsFile.karts[i].ToString());
                }
            }
        }

        public void RaceKart(KartsFile kartsFile)
        {
            Console.WriteLine("Enter your email address to race:");
            string email = Console.ReadLine();

            ViewAvailableKarts(kartsFile);

            Console.WriteLine("Enter the Kart ID to race:");
            int kartID = int.Parse(Console.ReadLine());

            int kartIndex = kartsFile.FindID(kartID);
            if (kartIndex == -1 || !kartsFile.karts[kartIndex].GetAvailability())
            {
                Console.WriteLine("Invalid Kart ID or Kart is unavailable.");
                return;
            }

            Console.WriteLine("Select a track:");
            for (int i = 0; i < tracks.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {tracks[i]}");
            }

            int trackChoice = int.Parse(Console.ReadLine());
            if (trackChoice < 1 || trackChoice > tracks.Length)
            {
                Console.WriteLine("Invalid track selection.");
                return;
            }

            string selectedTrack = tracks[trackChoice - 1];

            Console.WriteLine("Enter your race time (in seconds):");
            int raceTime = int.Parse(Console.ReadLine());

            RaceResults newResult = new RaceResults(
                RaceResults.GetCount() + 1,
                email,
                kartID,
                DateTime.Now.ToString("yyyy-MM-dd"),
                raceTime,
                raceTime, // Placeholder for time elapsed
                selectedTrack,
                false);

            raceResults[RaceResults.GetCount()] = newResult;
            RaceResults.IncCount();

            kartsFile.karts[kartIndex].SetAvailability(false);
            SaveResultsToFile();

            Console.WriteLine("Race completed and result saved.");
        }

        public void ViewRacedKarts(string email)
        {
            Console.WriteLine($"Karts raced by {email}:");
            for (int i = 0; i < RaceResults.GetCount(); i++)
            {
                if (raceResults[i].GetPlayerEmail().Equals(email, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(raceResults[i].ToString());
                }
            }
        }

        public void ReturnKart(KartsFile kartsFile)
        {
            Console.WriteLine("Enter your email address:");
            string email = Console.ReadLine();

            Console.WriteLine("Enter the Kart ID to return:");
            int kartID = int.Parse(Console.ReadLine());

            bool found = false;
            for (int i = 0; i < RaceResults.GetCount(); i++)
            {
                if (raceResults[i].GetPlayerEmail().Equals(email, StringComparison.OrdinalIgnoreCase) &&
                    raceResults[i].GetKartID() == kartID &&
                    !raceResults[i].GetKartReturned())
                {
                    found = true;
                    raceResults[i].SetKartReturned(true);
                    kartsFile.karts[kartsFile.FindID(kartID)].SetAvailability(true);
                    SaveResultsToFile();
                    Console.WriteLine("Kart returned successfully.");
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine("No matching kart found for return.");
            }
        }
    }
}
