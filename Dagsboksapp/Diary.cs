using System.Globalization;

namespace Dagsboksapp
{
    public class Entry
    {
        public DateTime Date { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return $"{Date:yyyy-MM-dd}: {Text}";
        }
    }

    public class Diary
    {
        private List<Entry> entries = new List<Entry>();

        public void AddEntry(string text)
        {
            DateTime entryDate;

            while (true)
            {
                Console.Write("Type date (yyyy-MM-dd) or press Enter for today: ");
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {

                    entryDate = DateTime.Now;
                    break;
                }

                try
                {

                    entryDate = DateTime.ParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine(" Invalid date format. Please use yyyy-MM-dd.");
                }
            }


            entries.Add(new Entry { Date = entryDate, Text = text });

        }



        public List<Entry> ViewEntry()
        {
            return entries;

        }

        public List<Entry> FindByDate(DateTime date)
        {
            List<Entry> found = new List<Entry>();

            foreach (Entry entry in entries)
            {
                if (entry.Date.Date == date.Date)
                {
                    found.Add(entry);
                }
            }

            return found;
        }

        public void Load(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File '{filePath}' not found.");
                    return;
                }

                string[] lines = File.ReadAllLines(filePath);

                List<Entry> fileEntries = new List<Entry>();
                foreach (string line in lines)
                {
                    if (line.Length < 11) continue;

                    string datePart = line.Substring(0, 10);
                    string textPart = line.Substring(11).Trim();

                    if (DateTime.TryParseExact(datePart, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                                               DateTimeStyles.None, out DateTime entryDate))
                    {
                        fileEntries.Add(new Entry { Date = entryDate, Text = textPart });
                    }
                }

                if (entries.Count > 0)
                {
                    Console.WriteLine("You already have entries.");
                    Console.WriteLine("Press 1 to merge current entries with file entries.");
                    Console.WriteLine("Press 2 to overwrite current entries with file entries.");
                    string? input = Console.ReadLine();
                    if (input == "1")
                    {
                        entries.AddRange(fileEntries);
                    }
                    else
                    {
                        entries = fileEntries;
                    }
                }
                else
                {
                    entries = fileEntries;
                }

                Console.WriteLine($"{filePath} loaded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading file: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Press any key to continue");
                Console.ReadKey(true);
            }
        }
        public void Save(string filePath)
        {
            try
            {
                List<Entry> linesToSave = new List<Entry>(entries);
                if (File.Exists(filePath))
                {
                    Console.WriteLine($"File '{filePath}' already exists.");
                    Console.WriteLine("Press 1 to merge with existing file entries.");
                    Console.WriteLine("Press 2 to overwrite the file.");
                    string? input = Console.ReadLine();

                    if (input == "1")
                    {


                        var existingLines = File.ReadAllLines(filePath);
                        foreach (var line in existingLines)
                        {
                            int colonIndex = line.IndexOf(':');
                            if (colonIndex == -1) continue;

                            string datePart = line.Substring(0, colonIndex).Trim();
                            string textPart = line.Substring(colonIndex + 1).Trim();

                            if (DateTime.TryParseExact(datePart, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                                                       DateTimeStyles.None, out DateTime entryDate))
                            {

                                if (!linesToSave.Any(e => e.Date.Date == entryDate && e.Text == textPart))
                                {
                                    linesToSave.Add(new Entry { Date = entryDate, Text = textPart });
                                }
                            }
                        }
                    }
                    else if (input != "2")
                    {
                        Console.WriteLine("Invalid choice, defaulting to overwrite.");
                    }
                }


                File.WriteAllLines(filePath, linesToSave.Select(e => e.ToString()).ToArray());
                Console.WriteLine($"Saved {linesToSave.Count} entries to {filePath}.");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not save to file: {ex.Message}");
            }
        }
    }
}



