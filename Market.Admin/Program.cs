using System;
using Market.DB;

namespace Market.Admin
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                int order = -1;

                try
                {
                    order = Dialog.ShowCommands();
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Unknown Command \n");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                switch ((Commands)order)
                {
                    case Commands.ShowData:
                        Console.Clear();
                        Dialog.ShowTable(DBTools.GetTable());
                        Console.WriteLine("\n\n");
                        break;
                    case Commands.InportData:
                        Console.Clear();
                        Console.WriteLine("InportData \n");
                        break;
                    case Commands.Exit:
                        return;
                    default:
                        Console.WriteLine("Unknown Command \n");
                        break;
                }
            }
        }
    }
}