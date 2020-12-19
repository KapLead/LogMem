using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace LogMem
{
    public class Item
    {
        public Levent Type { get; private set; } = Levent.None;
        public DateTime Date { get; private set; } = DateTime.Now;
        public string[] Message { get; private set; } = new string[0];

        public Item()
        {

        }

        public Item( DateTime dt,Levent type=Levent.Default,string[] msg = null)
        {
            Type = type;
            Date = dt;
            Message = msg;
        }

        public Item(string line)
        {
            var tmp = line.Split('\t').Where(s => !string.IsNullOrEmpty(s)).ToArray();
            Type = tmp?[0]?.IndexPref().ToLevent()??Levent.None;
            Date = DateTime.ParseExact(tmp[1], "T", new DateTimeFormatInfo());
            List<string> t = new List<string>();
            if(tmp.Length>2)
            for (int i = 2; i < tmp.Length; i++) t.Add(tmp[i]);
            Message = t.ToArray();
        }

        public override string ToString()
        {
            return $"[{Type.ToPref()}]\t{Date:T}\t{Message.Aggregate(string.Empty, (current, s) => (current == null ? "" : "\r\n\t\t") + current)}";
        }
    }
}
