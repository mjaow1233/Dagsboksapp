using System.ComponentModel.Design;

namespace Dagsboksapp
{

    internal class Program
    {
        private static readonly string dagbokFilePath = "dagbok.txt";
        private static Diary diary = new Diary();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to your diary app");
            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("Choose an option:");
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
                        Console.WriteLine("");
                        Console.WriteLine("Adding a new entry");
                        AddEntry();
                        break;
                    case MenuChoice.ViewEntry:
                        Console.WriteLine("");
                        Console.WriteLine("View all entries");
                        ViewEntry();
                        break;
                    case MenuChoice.SearchEntry:
                        Console.WriteLine("");
                        Console.WriteLine("Search for an entry");
                        SearchEntry();
                        break;

                    case MenuChoice.SaveFile:
                        Console.WriteLine("");
                        Console.WriteLine("Save to file");
                        SaveFile();
                        break;
                    case MenuChoice.LoadFile:
                        Console.WriteLine("");
                        Console.WriteLine("Load file");
                        LoadFile();
                        break;

                    case MenuChoice.Exit:
                        Console.WriteLine("");
                        Console.WriteLine("Exit the program");
                        return;
                    default:
                        Console.WriteLine("");
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
                Console.WriteLine("");
                Console.WriteLine("No entries found.");
                return;
            }
            Console.WriteLine("");
            Console.WriteLine("Your diary:");
            for (int i = 0; i < entries.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {entries[i]}");
                Console.WriteLine("");
            }
            Console.WriteLine($"You have made:{entries.Count} entries");
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
        private static void LoadFile()
        {
            diary.Load(dagbokFilePath);
        }

    }
}



