using System.ComponentModel.Design;

namespace Dagboksapp
{
    internal class Program
    {
        private static readonly string dagbokFilePath = "dagbok.txt";
        private static string? entry;

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
                        Console.WriteLine("Lägger till en ny uppgift...");
                        AddEntry();
                        break;
                    case MenuChoice.ViewEntry:
                        Console.WriteLine("Visar alla uppgifter...");
                        ViewEntry();
                        break;
                    case MenuChoice.SearchEntry:
                        Console.WriteLine("Visar alla uppgifter...");
                        SearchEntry();
                        break;

                    case MenuChoice.SaveFile:
                        Console.WriteLine("Visar alla uppgifter...");
                        savefile();
                        break;
                    case MenuChoice.Loadfile:
                        Console.WriteLine("Visar alla uppgifter...");
                        Loadfile();
                        break;

                    case MenuChoice.Exit:
                        Console.WriteLine("Avslutar programmet. Hej då!");
                        return;
                    default:
                        Console.WriteLine("Ogiltigt val. Försök igen.");
                        break;
                }

            }
        }

        private static void ViewEntry()
        {
            try
            {
                if (!File.Exists(dagbokFilePath))
                {
                    Console.WriteLine("Inga uppgifter hittades.");
                    return;
                }

                string[] entries = File.ReadAllLines(dagbokFilePath);
                if (entries.Length == 0)
                {
                    Console.WriteLine("Inga uppgifter hittades.");
                    return;
                }

                Console.WriteLine("Dina uppgifter:");
                for (int i = 0; i < entry.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {entries[i]}");
                }
                Console.WriteLine($"Totalt: {entries.Length} uppgifter.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel uppstod vid läsning av uppgifterna: {ex.Message}");
            }
        }

        private static void AddEntry()
        {
            Console.WriteLine("skriv in");
            string? entry = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(entry))
            {
                Console.WriteLine("Uppgiften kan inte vara tom. Försök igen.");
                return;
            }
            try
            {
                string entryWithTimestamp = $"{DateTime.Now}: {entry}";
                File.AppendAllText(dagbokFilePath, entryWithTimestamp + Environment.NewLine);
                Console.WriteLine("Uppgiften har lagts till.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel uppstod vid lagring av uppgiften: {ex.Message}");
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
    }
}
