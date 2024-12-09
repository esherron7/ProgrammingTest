using MarioKart;

namespace MarioKartApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            MenuSystem();
        }

        static void MenuSystem()
        {
            Console.Clear();
            string menuChoice = "";
            while (menuChoice != "4")
            {
                Console.Clear();
                System.Console.WriteLine("Welcome to the UA Mario Kart Tournament! Please make a menu selection:");
                System.Console.WriteLine("\n1. Admin Menu \n2. Player Menu \n3. Race Report Menu \n4. Exit Application");
                menuChoice = GetMenuChoice();
                RouteEmMain(menuChoice);
            }
        }

        static string GetMenuChoice()
        {
            return Console.ReadLine();
        }

        static void RouteEmMain(string menuChoice)
        {
            switch (menuChoice)
            {
                case "1":
                    AdminMenu();
                    break;
                case "2":
                    PlayerMenu();
                    break;
                case "3":
                    ReportMenu();
                    break;
                case "4":
                    Goodbye();
                    break;
                default:
                    if (DisplayErrorMessage())
                    {
                        return;
                    }
                    break;
            }
        }

        static void AdminMenu()
{
    Console.Clear();
    KartsFile kartsFile = new KartsFile();
    string menuChoice = "";
    while (menuChoice != "5")
    {
        System.Console.WriteLine("Welcome to the Admin Menu!! Please make your menu choice:");
        System.Console.WriteLine("\n1. Add a new Kart to inventory \n2. Remove a Kart from inventory \n3. Edit information about a current Kart \n4. Print all Karts by size \n5. Return to previous Menu");
        menuChoice = GetMenuChoice();
        RouteEmAdmin(menuChoice);
    }
}

        static void RouteEmAdmin(string menuChoice)
{
    KartsFile kartsFile = new KartsFile();
    switch (menuChoice)
    {
        case "1":
            Console.Clear();
            kartsFile.GetAllKartsFromFile();
            kartsFile.AddKart();
            break;
        case "2":
            Console.Clear();
            kartsFile.GetAllKartsFromFile();
            kartsFile.DeleteKart();
            break;
        case "3":
            Console.Clear();
            kartsFile.GetAllKartsFromFile();
            kartsFile.EditKart();
            break;
        case "4":
            Console.Clear();
            kartsFile.GetAllKartsFromFile();
            kartsFile.PrintAllKartsBySize();
            break;
        case "5":
            Goodbye();
            break;
        default:
            if (DisplayErrorMessage())
            {
                return;
            }
            break;
    }
}

        static void PlayerMenu()
        {
        Console.Clear();
        RaceResults reultsFile = new RaceResults();
        string menuChoice = "";
        menuChoice=GetMenuChoice();
        while (menuChoice != "5"){
            System.Console.WriteLine("Welcome to the Players Menu! Please Make your selection: \n1. View Available Karts \n2. Race a Kart \n3. \n4. View Karts that you have previously used \n5. Return a Kart \n6. Exit");
            menuChoice=GetMenuChoice();
        }
        }
       static void RouteEmPlayer(string menuChoice)
{
    RaceResults resultsFile = new RaceResults();
    switch (menuChoice)
    {
        case "1":
            Console.Clear();
            resultsFile.GetAllResultsFromFile();
            resultsFile.ViewAvailableKarts();
            break;
        case "2":
            Console.Clear();
            resultsFile.GetAllResultsFromFile();
            resultsFile.RaceKart();
            break;
        case "3":
            Console.Clear();
            kartsFile.GetAllKartsFromFile();
            kartsFile.EditKart();
            break;
        case "4":
            Console.Clear();
            kartsFile.GetAllKartsFromFile();
            kartsFile.PrintAllKartsBySize();
            break;
        case "5":
            Goodbye();
            break;
        default:
            if (DisplayErrorMessage())
            {
                return;
            }
            break;
    }
}

        static void ReportMenu()
        {
            System.Console.WriteLine("Report Menu Goes Here");
        }

        static void Goodbye()
        {
            System.Console.WriteLine("Goodbye, Thanks For Visiting!!");
            Environment.Exit(0);
        }

        static bool DisplayErrorMessage()
        {
            System.Console.WriteLine("Invalid menu choice, returning to main menu...");
            return true;
        }
    }
}
