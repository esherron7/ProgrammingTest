namespace MarioKart
{
    public class KartsFile
    {
        public Karts[] karts = new Karts[100];
        public string[] ids = new string[100];

        public KartsFile()
        {
        }

        public void GetAllKartsFromFile()
        {
            using (StreamReader inFile = new StreamReader("kart-inventory.txt"))
            {
                Karts.SetCount(0);
                string line = inFile.ReadLine();

                while (line != null)
                {
                    string[] temp = line.Split('#');
                    karts[Karts.GetCount()] = new Karts(int.Parse(temp[0]), temp[1], temp[2], bool.Parse(temp[3]));
                    ids[Karts.GetCount()] = temp[0];
                    Karts.IncCount();
                    line = inFile.ReadLine();
                }
            }
        }

        public void AddKart()
        {
            Karts newKart = new Karts();
            Console.WriteLine("Taken Kart IDs: ");
            for (int i = 0; i < Karts.GetCount(); i++)
            {
                Console.WriteLine(ids[i] + ",");
            }

            Console.WriteLine("Please Enter the Kart ID: ");
            string searchVal1 = Console.ReadLine();

            while (searchVal1 != "STOP")
            {
                if (FindID(int.Parse(searchVal1)) == -1)
                {
                    newKart.SetID(int.Parse(searchVal1));
                    searchVal1 = "STOP";
                    break;
                }
                else
                {
                    Console.WriteLine("ID already taken. Please enter a different Kart ID or type 'STOP' to exit.");
                    searchVal1 = Console.ReadLine();
                }
            }

            Console.WriteLine("Please enter the name of the Kart you would like to add:");
            newKart.SetName(Console.ReadLine());
            Console.WriteLine("Please enter the size of the Kart (small/medium/large):");
            newKart.SetSize(Console.ReadLine());
            Console.WriteLine("Would you like this cart to be available? (Y/N):");
            newKart.SetAvailability(Console.ReadLine().ToUpper() == "Y");

            karts[Karts.GetCount()] = newKart;
            Karts.IncCount();
            Save();
        }

        public void EditKart()
        {
            Console.WriteLine("Please Enter the Kart ID to edit: ");
            int searchVal1 = int.Parse(Console.ReadLine());

            int kartIndex = FindID(searchVal1);
            if (kartIndex == -1)
            {
                Console.WriteLine("Kart ID not found.");
                return;
            }

            Karts editKart = karts[kartIndex];
            Console.WriteLine("Editing Kart ID: " + searchVal1);

            Console.WriteLine("Current Name: " + editKart.GetName());
            Console.WriteLine("Enter new Name (or press Enter to keep current): ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrEmpty(newName))
            {
                editKart.SetName(newName);
            }

            Console.WriteLine("Current Size: " + editKart.GetSize());
            Console.WriteLine("Enter new Size (small/medium/large) (or press Enter to keep current): ");
            string newSize = Console.ReadLine();
            if (!string.IsNullOrEmpty(newSize))
            {
                editKart.SetSize(newSize);
            }

            Console.WriteLine("Current Availability: " + (editKart.GetAvailability() ? "Yes" : "No"));
            Console.WriteLine("Is this kart available? (Y/N) (or press Enter to keep current): ");
            string newAvailability = Console.ReadLine();
            if (!string.IsNullOrEmpty(newAvailability))
            {
                editKart.SetAvailability(newAvailability.ToUpper() == "Y");
            }

            karts[kartIndex] = editKart;
            Save();
        }

        public int FindID(int kartID)
        {
            for (int i = 0; i < Karts.GetCount(); i++)
            {
                if (kartID == karts[i].GetID())
                {
                    return i;
                }
            }
            return -1;
        }

        private void Save()
        {
            using (StreamWriter outFile = new StreamWriter("kart-inventory.txt"))
            {
                for (int i = 0; i < Karts.GetCount(); i++)
                {
                    outFile.WriteLine(karts[i].GetID() + "#" + karts[i].GetName() + "#" + karts[i].GetSize() + "#" + karts[i].GetAvailability());
                }
            }
        }

        public string FindName(string searchVal)
        {
            for (int i = 0; i < Karts.GetCount(); i++)
            {
                if (karts[i].GetName().ToUpper() == searchVal.ToUpper())
                {
                    return karts[i].GetName();
                }
            }
            return " ";
        }

        public void DeleteKart()
        {
            Console.WriteLine("Kart IDs: ");
            PrintAllKartsBySize();

            Console.WriteLine("Please Enter the Kart ID you would like to Delete: ");
            int idToDelete = int.Parse(Console.ReadLine());

            int indexToDelete = FindID(idToDelete);
            if (indexToDelete == -1)
            {
                Console.WriteLine("Kart ID not found.");
                return;
            }

            for (int i = indexToDelete; i < Karts.GetCount() - 1; i++)
            {
                karts[i] = karts[i + 1];
                ids[i] = ids[i + 1];
            }

            karts[Karts.GetCount() - 1] = null;
            ids[Karts.GetCount() - 1] = null;

            Karts.DecCount();
            Save();
            Console.WriteLine("Kart deleted successfully.");
        }
        public void PrintAllKartsBySize()
    {
    string[] smallKarts = new string[100];
    string[] mediumKarts = new string[100];
    string[] largeKarts = new string[100];

    int smallCount = 0;
    int mediumCount = 0;
    int largeCount = 0;

    for (int i = 0; i < Karts.GetCount(); i++)
    {
        if (karts[i].GetSize().ToLower() == "small")
        {
            smallKarts[smallCount++] = ids[i];
        }
        else if (karts[i].GetSize().ToLower() == "medium")
        {
            mediumKarts[mediumCount++] = ids[i];
        }
        else if (karts[i].GetSize().ToLower() == "large")
        {
            largeKarts[largeCount++] = ids[i];
        }
    }

    Console.WriteLine("Small Kart IDs:");
    for (int i = 0; i < smallCount; i++)
    {
        Console.WriteLine(smallKarts[i]);
    }

    Console.WriteLine("\nMedium Kart IDs:");
    for (int i = 0; i < mediumCount; i++)
    {
        Console.WriteLine(mediumKarts[i]);
    }

    Console.WriteLine("\nLarge Kart IDs:");
    for (int i = 0; i < largeCount; i++)
    {
        Console.WriteLine(largeKarts[i]);
    }
    }
    }
}


