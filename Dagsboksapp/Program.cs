using System.ComponentModel.Design;

namespace Dagsboksapp
{

    internal class Program
    {
        private static readonly string dagbokFilePath = "dagbok.txt";
        private static Diary diary = new Diary();

        static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine("Welcome to your diary app");
                Console.WriteLine("1.Add an entry");
                Console.WriteLine("2.Show all entries");
                Console.WriteLine("3.Search for an entry");
                Console.WriteLine("4.Save file");
                Console.WriteLine("5.Load file");
                Console.WriteLine("6.Exit");

                MenuChoice choice = GetMenuChoice();
                switch (choice)
                {
                    case MenuChoice.AddEntry:
                        Console.WriteLine("Adding a new entry");
                        AddEntry();
                        break;
                    case MenuChoice.ViewEntry:
                        Console.WriteLine("View all entries");
                        ViewEntry();
                        break;
                    case MenuChoice.SearchEntry:
                        Console.WriteLine("Search for an entry");
                        string search = (Console.ReadLine());
                        SearchEntry();
                        break;

                    case MenuChoice.SaveFile:
                        Console.WriteLine("Save to file");
                        SaveFile();
                        break;
                    /*case MenuChoice.LoadFile:
                        Console.WriteLine("Load file");
                        LoadFile();
                        break;*/

                    case MenuChoice.Exit:
                        Console.WriteLine("Exit the program");
                        return;
                    default:
                        Console.WriteLine("That's not the right number");
                        break;
                }

            }
        }


        private static MenuChoice GetMenuChoice()
        {
            string? input = Console.ReadLine();
            if (input != null && int.TryParse(input, out int choice) && Enum.IsDefined(typeof(MenuChoice), choice))
            {
                return (MenuChoice)choice;
            }
            return MenuChoice.Invalid;

        }

        private static void AddEntry()
        {
            Console.Write("Type your entry: ");
            string? text = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("Entry cannot be empty.");
                return;
            }

            diary.AddEntry(text);
            Console.WriteLine("Entry recorded.");
        }
        private static void ViewEntry()
        {
            var entries = diary.ViewEntry();
            if (entries.Count == 0)
            {
                Console.WriteLine("No entries found.");
                return;
            }

            Console.WriteLine("Your diary:");
            foreach (var entry in entries)
            {
                Console.WriteLine(entry);
            }
        }

        private static void SearchEntry()
        {
            Console.Write("Enter date (YYYY-MM-DD): ");
            string? input = Console.ReadLine();

            if (!DateTime.TryParse(input, out DateTime searchDate))
            {
                Console.WriteLine("Incorrect format");
                return;
            }

            var found = diary.FindByDate(searchDate);

            if (found.Count == 0)
            {
                Console.WriteLine("Nothing found for that date");
                return;
            }

            Console.WriteLine($"Entries for {searchDate:yyyy-MM-dd}:");
            foreach (var entry in found)
            {
                Console.WriteLine(entry);
            }
        }
        private static void SaveFile()
        {
            diary.Save(dagbokFilePath);
        }

    }
}



