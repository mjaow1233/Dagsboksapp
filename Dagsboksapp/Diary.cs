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
            return entries.Where(e => e.Date.Date == date.Date).ToList();
        }

        public void Save(string filePath)
        {
            var lines = entries.Select(e => e.ToString()).ToArray();
            File.WriteAllLines(filePath, lines);
        }


    }
}
