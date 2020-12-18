using System;
using System.Collections.Generic;
using System.Drawing;

namespace LogMem
{
    public static class LeventExtension
    {
        static List<string> _pref = new List<string>();
        static List<Color> _color = new List<Color>();

        static LeventExtension()
        {
            foreach (Levent value in Enum.GetValues(typeof(Levent)))
                _pref.Add(value.ToPref());
        }

        public static Levent ToLevent(this int index)
        {
            var t = Enum.GetValues(typeof(Levent));
            if (index < 0 || index > t.Length) return Levent.None;
            return (Levent)t.GetValue(index);
        }
        public static int IndexPref(this string prefix)
        {
            prefix = prefix.Replace("[", null).Replace("]", null).Trim();
            if (prefix.Length < 3) return -1;
            if (prefix.Length > 3) prefix = prefix.Substring(0, 3);
            return _pref.FindIndex(s => s == prefix.ToUpper());
        }
        public static string ToPref(this Levent value)
        {
            return value.ToString().Substring(0, 3).ToUpper();
        }
    }
}
