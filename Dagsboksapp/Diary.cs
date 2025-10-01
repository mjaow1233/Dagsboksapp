using Dagsboksapp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}

