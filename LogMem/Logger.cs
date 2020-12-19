using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace LogMem
{
    public class Logger : IDisposable
    {
        /// <summary> Путь к папке с логами </summary>
        public static string Path = Application.StartupPath + @"\log\";
        /// <summary> Расширение файла логов </summary>
        public static string Extension = ".log";
        /// <summary> колличество линий, которые будут выгружаться при превышении </summary>
        public static int CountLineDeterminate = 10;
        /// <summary> колличество видимых линий которые будут всегда присутствовать в логе </summary>
        public static int CountShowLine = 100;
        public string Name { get; set; } = "default";
        public BindingList<Item> Items { get; private set; } = null;

        public Logger()
        {
            Items = new BindingList<Item>();
            Items.AddingNew+= ItemsOnAddingNew;
        }
        private void ItemsOnAddingNew(object sender, AddingNewEventArgs e)
        {
            if (Items.Count > CountShowLine + CountLineDeterminate)
            {
                Save(CountLineDeterminate);
            }
        }

        private void Save(int countLineDeterminate=-1)
        {
            List<Item> f = new List<Item>();
            if (countLineDeterminate <= 0) countLineDeterminate = Items.Count;
            for (int i = countLineDeterminate - 1; i >= 0; i--)
            {
                f.Add(Items[i]);
                Items.RemoveAt(i);
            }
            File.AppendAllLines(Path+Name+ Extension,f.Select(s=>s.ToString()), Encoding.UTF8);
        }

        private void ItemsAdd(Item item)
        {
            Items.Add(item);
        }
        private string[] ToArray(string value) => value.Split(new[] {"\r\n", "\r", "\n"}, StringSplitOptions.RemoveEmptyEntries);
        public void Add(Levent type, string msg) => ItemsAdd(new Item(DateTime.Now, type,ToArray(msg)));
        public void Info(string msg) => ItemsAdd(new Item(DateTime.Now, Levent.Info, ToArray(msg)));
        public void Bug(string msg) => ItemsAdd(new Item(DateTime.Now, Levent.Bug, ToArray(msg)));
        public void Default(string msg) => ItemsAdd(new Item(DateTime.Now, Levent.Default, ToArray(msg)));
        public void Diagnostics(string msg) => ItemsAdd(new Item(DateTime.Now, Levent.Diagnostics, ToArray(msg)));
        public void Error(string msg) => ItemsAdd(new Item(DateTime.Now, Levent.Error, ToArray(msg)));
        public void Fatal(string msg) => ItemsAdd(new Item(DateTime.Now, Levent.Fatal, ToArray(msg)));
        public void Warning(string msg) => ItemsAdd(new Item(DateTime.Now, Levent.Warning, ToArray(msg)));
        public void Exception(Exception msg) => ItemsAdd(new Item(DateTime.Now, Levent.Warning, new []{msg.Message,msg.StackTrace}));
        public void Dispose()
        {
            Save();
        }
    }
}
