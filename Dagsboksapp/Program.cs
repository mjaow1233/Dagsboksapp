
using System.Globalization;


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
                try
                {
                    Console.WriteLine("");
                    Console.WriteLine("1. Add an entry");
                    Console.WriteLine("2. Show all entries");
                    Console.WriteLine("3. Search for an entry");
                    Console.WriteLine("4. Edit an entry");
                    Console.WriteLine("5. Delete an entry");
                    Console.WriteLine("6. Save file");
                    Console.WriteLine("7. Load file");
                    Console.WriteLine("8. Exit");
                    Console.Write("");

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
                            ViewEntry();
                            break;
                        case MenuChoice.SearchEntry:
                            Console.WriteLine("");
                            Console.WriteLine("Search for an entry");
                            SearchEntry();
                            break;

                        case MenuChoice.EditEntry:
                            Console.WriteLine("");
                            Console.WriteLine("Pick an entry to edit");
                            EditEntry();
                            break;
                        case MenuChoice.DeleteEntry:
                            Console.WriteLine("");
                            Console.WriteLine("Pick an entry to delete");
                            DeleteEntry();
                            break;

                        case MenuChoice.SaveFile:
                            Console.WriteLine("");
                            
                            SaveFile();
                            break;
                        case MenuChoice.LoadFile:
                            Console.WriteLine("");
                            ;
                            LoadFile();
                            break;

                        case MenuChoice.Exit:
                            Console.WriteLine("");
                            Console.WriteLine("Exit the program");
                            return;
                        default:
                            Console.WriteLine("");
                            Console.WriteLine("That's not a menu-number");
                            break;
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
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

        }
        private static void ViewEntry()
        {
            var entries = diary.ViewEntry()
                               .OrderBy(e => e.Date)
                               .ToList();

            if (entries.Count == 0)
            {
                Console.WriteLine("No entries found.");
                return;
            }

            Console.WriteLine($"You have made: {entries.Count} entries");
            for (int i = 0; i < entries.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {entries[i]}");
            }
        }

        private static void SearchEntry()
        {
            DateTime searchDate;

            while (true)
            {
                Console.Write("Enter date (YYYY-MM-DD): ");
                Console.Write("");
                string? input = Console.ReadLine();

                if (DateTime.TryParseExact(input, "yyyy-MM-dd",
                                           CultureInfo.InvariantCulture,
                                           DateTimeStyles.None,
                                           out searchDate))
                {
                    break;
                }

                Console.WriteLine("Incorrect format. Please use YYYY-MM-DD. Try again.");
            }

            var found = diary.FindByDate(searchDate);

            if (found.Count == 0)
            {
                Console.WriteLine("");
                Console.WriteLine("No entries found for that date.");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey(true);
                return;
            }

            Console.WriteLine($"Entries for {searchDate:yyyy-MM-dd}:");
            foreach (var entry in found)
            {
                Console.WriteLine("");
                Console.WriteLine(entry);
                Console.WriteLine("Press any key to continue");
                Console.ReadKey(true);

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
        private static void EditEntry()
        {
            var entries = diary.ViewEntry();

            if (entries.Count == 0)
            {
                Console.WriteLine("No entries to edit.");
                return;
            }

            Console.WriteLine("Your diary entries:");
            for (int i = 0; i < entries.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {entries[i]}");
            }

            int choice;
            while (true)
            {
                Console.Write("Edit Entry: ");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out choice) && choice >= 1 && choice <= entries.Count)
                {
                    break;
                }

                Console.WriteLine("Invalid input try again.");
            }
            var selectedEntry = entries[choice - 1];

            Console.WriteLine("");
            Console.Write("Enter new text");
            Console.WriteLine("");
            string? newText = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newText))
            {
                selectedEntry.Text = newText;
            }
        }
        private static void DeleteEntry()
        {
            var entries = diary.ViewEntry();

            if (entries.Count == 0)
            {
                Console.WriteLine("No entries to delete.");
                return;
            }

            Console.WriteLine("Your diary entries:");
            for (int i = 0; i < entries.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {entries[i]}");
            }

            int choice;
            while (true)
            {
                Console.Write("Select entry number to delete: ");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out choice) && choice >= 1 && choice <= entries.Count)
                {
                    break;
                }
                Console.WriteLine("Invalid input, try again.");
            }

            if (diary.DeleteEntry(choice - 1))
            {
                Console.WriteLine("Entry Deleted.");
            }
            else
            {
                Console.WriteLine("Failed to delete entry.");
            }
        }

    }
}










