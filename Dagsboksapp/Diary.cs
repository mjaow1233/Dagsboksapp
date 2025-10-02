using System.Globalization;

namespace Dagsboksapp
{
    public class Entry
    {
        public DateTime Date { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return $"{Date:yyyy-MM-dd HH:mm}: {Text}";
        }
    }

    public class Diary
    {
        private List<Entry> entries = new List<Entry>();

        public void AddEntry(string text)
        {
            Console.WriteLine("type date (yyyy-MM-dd) or press Enter for today:");
            string? input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input) &&
 DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out DateTime entryDate))
            {
                entries.Add(new Entry { Date = entryDate, Text = text });
                return;
            }
            entries.Add(new Entry { Date = DateTime.Now, Text = text });
        }
        public List<Entry> ViewEntry()
        {
            return entries;

        }
        /*public List<Entry> FindByWord(string word)
        {
            List<Entry> foundword = new List<Entry>();

        }*/
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

                if (line.Length < 20) continue;

                string datePart = line.Substring(0, 19);
                string textPart = line.Substring(20).Trim();

                if (DateTime.TryParse(datePart, out DateTime entryDate))
                {
                    entries.Add(new Entry
                    {
                        Date = entryDate,
                        Text = textPart
                    });
                }
            }

        }

        public void Save(string filePath)
        {
            List<string> lines = new List<string>();
            foreach (Entry entry in entries)
            {
                lines.Add($"{entry.Date} {entry.Text}");
            }
            File.WriteAllLines(filePath, lines);
        }

    }
}



