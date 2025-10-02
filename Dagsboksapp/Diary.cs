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

        private Dictionary<DateTime, List<Entry>> entryDict = new Dictionary<DateTime, List<Entry>>();
        public void AddEntry(string text)
        {
            DateTime entryDate;



            while (true)
            {
                Console.Write("Type date (yyyy-MM-dd) or press Enter for today: ");
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    entryDate = DateTime.Now.Date;
                    break;
                }

                if (DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                                           DateTimeStyles.None, out entryDate))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please use yyyy-MM-dd.");
                }
            }

            var newEntry = new Entry { Date = entryDate, Text = text };

            AddToCollections(newEntry);
            Console.WriteLine("Entry added successfully.");
        }
        public bool DeleteEntry(int index)
        {
            if (index < 0 || index >= entries.Count) return false;

            var entry = entries[index];
            entries.RemoveAt(index);

            if (entryDict.TryGetValue(entry.Date.Date, out var list))
            {
                list.Remove(entry);
                if (list.Count == 0)
                {
                    entryDict.Remove(entry.Date.Date);
                }
            }

            return true;
        }
        private void AddToCollections(Entry entry)
        {
            entries.Add(entry);

            if (!entryDict.ContainsKey(entry.Date.Date))
            {
                entryDict[entry.Date.Date] = new List<Entry>();
            }
            entryDict[entry.Date.Date].Add(entry);
        }


        public List<Entry> ViewEntry()
        {
            return entries;

        }

        public List<Entry> FindByDate(DateTime date)
        {
            if (entryDict.TryGetValue(date.Date, out var found))
            {
                return found;
            }
            return new List<Entry>();
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
                var fileDict = new Dictionary<DateTime, List<Entry>>();

                foreach (string line in lines)
                {
                    if (line.Length < 11) continue;
                    string datePart = line.Substring(0, 10);
                    string textPart = line.Substring(11).Trim();

                    if (DateTime.TryParseExact(datePart, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                                               DateTimeStyles.None, out DateTime entryDate))
                    {
                        var entry = new Entry { Date = entryDate, Text = textPart };
                        fileEntries.Add(entry);

                        if (!fileDict.ContainsKey(entryDate))
                        {
                            fileDict[entryDate] = new List<Entry>();
                        }
                        fileDict[entryDate].Add(entry);
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
                        foreach (var entry in fileEntries)
                        {
                            if (!entries.Any(x => x.Date.Date == entry.Date.Date && x.Text == entry.Text))
                            {
                                AddToCollections(entry);
                            }
                        }
                    }
                    else
                    {
                        entries = fileEntries;
                        entryDict = fileDict;
                    }
                }
                else
                {
                    entries = fileEntries;
                    entryDict = fileDict;
                }

                Console.WriteLine($"Diary loaded from {filePath}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading file: {ex.Message}");
                File.AppendAllText("error.log", $"{DateTime.Now}: {ex}");
            }
            finally
            {
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey(true);
            }
        }
        public void Save(string filePath)
        {
            try
            {

                var linesToSave = entries.OrderBy(entry => entry.Date)
                                         .Select(entry => entry.ToString())
                                         .ToList();

                if (File.Exists(filePath))
                {
                    Console.WriteLine("A file already exists");
                    Console.WriteLine("Press 1 to merge current entries with file entries.");
                    Console.WriteLine("Press 2 to overwrite current entries with file entries.");
                    string? input = Console.ReadLine();

                    if (input == "1")
                    {
                        string[] existingLines = File.ReadAllLines(filePath);
                        foreach (string line in existingLines)
                        {
                            if (line.Length < 11) continue;
                            string datePart = line.Substring(0, 10);
                            string textPart = line.Substring(11).Trim();
                            if (DateTime.TryParseExact(datePart, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                                                       DateTimeStyles.None, out DateTime entryDate))
                            {
                                if (!linesToSave.Any(x => x.StartsWith(entryDate.ToString("yyyy-MM-dd")) && x.Contains(textPart)))
                                {
                                    linesToSave.Add($"{entryDate:yyyy-MM-dd}: {textPart}");
                                    AddToCollections(new Entry { Date = entryDate, Text = textPart });
                                }
                            }
                        }
                    }
                }

                File.WriteAllLines(filePath, linesToSave);
                Console.WriteLine($"Diary saved to {filePath}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not save file: {ex.Message}");
                File.AppendAllText("error.log", $"{DateTime.Now}: {ex}");
            }
            finally
            {
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey(true);
            }
        }

     

    }
}



