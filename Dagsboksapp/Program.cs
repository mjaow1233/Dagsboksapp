using System.ComponentModel.Design;

namespace Dagsboksapp
{

    internal class Program
    {
        private static readonly string dagbokFilePath = "dagbok.txt";


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
                    /*case MenuChoice.ViewEntry:
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
                        Savefile();
                        break;
                    case MenuChoice.LoadFile:
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

        

        private static void AddEntry()
        {
            Console.WriteLine("Type.");
            string? entry = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(entry))
            {
                Console.WriteLine("Entry cannot be empty, try again.");
                return;
            }
            try
            {
                string entryWithTimestamp = $"{DateTime.Now}: {entry}";
                File.AppendAllText(dagbokFilePath, entryWithTimestamp + Environment.NewLine);
                Console.WriteLine("Entry recorded.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"File did not save {ex.Message}");
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
        private static void SearchEntry()
        {
            Console.Write("Todays date: YYMMDD");
            string? input = Console.ReadLine();

            if (!DateTime.TryParse(input, out DateTime searchDate))
            {
                Console.WriteLine("Incorrect format");
                return;
            }

            try
            {
                if (!File.Exists(dagbokFilePath))
                {
                    Console.WriteLine("");
                    return;
                }

                string[] entries = File.ReadAllLines(dagbokFilePath);
                bool found = false;

                foreach (string entry in entries)
                {
                   
                    int firstColon = entry.IndexOf(':');
                    if (firstColon == -1) continue;

                    string datePart = entry.Substring(0, firstColon);
                    if (DateTime.TryParse(datePart, out DateTime entryDate))
                    {
                        if (entryDate.Date == searchDate.Date)
                        {
                            Console.WriteLine(entry);
                            found = true;
                        }
                    }
                }

                if (!found)
                {
                    Console.WriteLine("Nothing found for that date");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

}
