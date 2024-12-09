using System;
using System.IO;

namespace MarioKart
{
    public class PlayersMenu
    {
        public RaceResults[] raceResults = new RaceResults[100];
        KartsFile kartsFile = new KartsFile();
        private static string[] tracks = {"Rainbow Road", "Bowser's Castle", "Mario Circuit", "Luigi's Mansion"};
        string[] resultIDs = new string[100];

        public PlayersMenu()
        {
            GetAllResultsFromFile();
        }

        public void GetAllResultsFromFile()
        {
            StreamReader inFile = new StreamReader("results.txt");
            RaceResults.SetCount(0);
            string line = inFile.ReadLine();
            while (line != null)
            {
                string[] temp = line.Split('#');
                raceResults[RaceResults.GetCount()] = new RaceResults(
                    int.Parse(temp[0]), temp[1], int.Parse(temp[2]), temp[3], int.Parse(temp[4]), int.Parse(temp[5]), temp[6], bool.Parse(temp[7])
                );
                resultIDs[RaceResults.GetCount()] = temp[0];
                RaceResults.IncCount();
                line = inFile.ReadLine();
            }
            inFile.Close();
        }

        public void SaveResultsToFile()
        {
            StreamWriter outFile = new StreamWriter("results.txt");
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
            outFile.Close();
        }

        public void ViewAvailableKarts()
        {
            Console.Clear();
            Console.WriteLine("Available Karts:");
            for (int i = 0; i < Karts.GetCount(); i++)
            {
                if (kartsFile.karts[i] != null && kartsFile.karts[i].GetAvailability())
                {
                    Console.WriteLine(kartsFile.karts[i].ToString());
                }
            }
        }

        public void RaceKart(Karts kartsFile)
        {
            Console.Clear();
            Console.WriteLine("Enter your email address to race:");
            string email = Console.ReadLine();

            ViewAvailableKarts();

            Console.WriteLine("Enter the Kart ID to race:");
            int kartID = int.Parse(Console.ReadLine());

            int kartIndex = kartsFile.FindID(kartID);
            while (kartID != -1)
            {
                if (kartIndex >= 0)
                {
                    if (kartsFile.karts[kartIndex].GetAvailability())
                    {
                        Console.Clear();
                        Console.WriteLine(kartsFile.karts[kartIndex].ToString());
                        Console.WriteLine("\nWould you like to race this kart? (Y/N)");
                        string input = Console.ReadLine();
                        if (input.ToUpper() == "Y")
                        {
                            AddRaceResult(kartIndex, email, kartID);
                            kartID = -1;
                        }
                        else
                        {
                            kartID = -1;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Kart is unavailable, please enter another Kart ID.");
                        kartID = int.Parse(Console.ReadLine());
                        kartIndex = kartsFile.FindID(kartID);
                    }
                }
                else
                {
                    Console.WriteLine("Kart ID does not exist. Please enter valid Kart ID.");
                    kartIndex = kartsFile.FindID(int.Parse(Console.ReadLine()));
                }
            }
        }

        public void AddRaceResult(int index, string email, int kartID)
        {
            Console.Clear();
            RaceResults newResult = new RaceResults(
                RaceResults.GetCount() + 1, email, kartID, DateTime.Now.ToString("yyyy-MM-dd"), 0, 0, "", false
            );

            Console.WriteLine("Select a track:");
            for (int i = 0; i < tracks.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {tracks[i]}");
            }
            int trackChoice = int.Parse(Console.ReadLine());
            string selectedTrack = tracks[trackChoice - 1];
            newResult.SetTrack(selectedTrack);

            Console.WriteLine("Enter your race time (in seconds):");
            int raceTime = int.Parse(Console.ReadLine());
            newResult.SetRaceTime(raceTime);
            newResult.SetTimeElapsed(raceTime);

            raceResults[RaceResults.GetCount()] = newResult;
            RaceResults.IncCount();

            kartsFile.karts[index].SetAvailability(false);
            SaveResultsToFile();

            Console.WriteLine("Race completed and result saved.");
        }

        public void ViewRacedKarts(string email)
        {
            Console.Clear();
            Console.WriteLine($"Karts raced by {email}:");
            for (int i = 0; i < RaceResults.GetCount(); i++)
            {
                if (raceResults[i].GetPlayerEmail().Equals(email, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(raceResults[i].ToString());
                }
            }
        }

        public void ReturnKart()
        {
            Console.Clear();
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