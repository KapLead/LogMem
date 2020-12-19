using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
                Message = tmp[2].Split(new []{'|'}, StringSplitOptions.RemoveEmptyEntries).ToArray();
        }

        public override string ToString()
        {
            string msg = Message.Aggregate<string, string>(null, (current, s) => current + ((current == null ? "" : "|") + s));
            if (msg.EndsWith("|"))
                msg = msg.Substring(0, msg.Length - 2);
            return $"[{Type.ToPref()}]\t{Date:T}\t{msg?.Trim()}";
        }
    }
}
