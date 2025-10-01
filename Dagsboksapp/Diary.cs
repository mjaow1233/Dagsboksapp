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
            entries.Add(new Entry { Date = DateTime.Now, Text = text });
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
                int firstColon = line.IndexOf(':');
                if (firstColon == -1) continue;

                string datePart = line.Substring(0, firstColon);
                string textPart = line.Substring(firstColon + 1).Trim();

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
                lines.Add(entry.ToString());
            }
            File.WriteAllLines(filePath, lines);
            Console.WriteLine("Diary saved");
        }

    }
}



