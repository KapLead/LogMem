using LogMem;
using System;

namespace TestLogMem
{
    class Program
    {
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            var types = Enum.GetValues(typeof(Levent));
            using (Logger log = new Logger { Name = "Test" })
            {
                log.OnSave += (sender, eventArgs) => Console.WriteLine("SAVED " + eventArgs.Element);
                for (int i = 0; i < 1000; i++)
                {
                    string msg = null;
                    for (int j = 0; j < rnd.Next(3, 14); j++)
                    {
                        msg += gen(rnd.Next(2, 5));
                        if (i % 2 == 0) msg += "|";
                    }
                    log.Add((Levent)types.GetValue(rnd.Next(0, types.Length)), msg);
                    //Console.WriteLine(i + " " + log.Items.Last());
                }
            }
            Console.WriteLine("Генерация завершена. Нажмите любую кнопку для чтения из файла");
            Console.ReadKey();
            using (Logger log = new Logger { Name = "Test" })
            {
                log.LoadFile(Logger.Path + log.Name + Logger.Extension);
            }
            Console.ReadKey();
        }


        static string gen(int num)
        {
            string ret = null;
            for (int i = 0; i < num; i++)
            {
                ret += rnd.Next((int)'a', (int)'Я');
            }
            return ret + " ";
        }
    }
}
