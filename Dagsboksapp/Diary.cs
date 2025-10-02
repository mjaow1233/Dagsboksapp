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
            if (!File.Exists(filePath)) return;

            entries.Clear();
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split('|', 2);
                if (parts.Length == 2 &&
                    DateTime.TryParseExact(parts[0], "yyyy-MM-dd",
                                           CultureInfo.InvariantCulture,
                                           DateTimeStyles.None,
                                           out DateTime entryDate))
                {
                    entries.Add(new Entry
                    {
                        Date = entryDate,
                        Text = parts[1]
                    });
                }
            }
        }


        public void Save(string filePath)
        {
            List<string> lines = new List<string>();
            foreach (Entry entry in entries)
            {
                lines.Add($"{entry.Date:yyyy-MM-dd}|{entry.Text}");
            }
            File.WriteAllLines(filePath, lines);
        }

    }
}



