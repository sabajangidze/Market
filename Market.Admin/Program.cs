using Market.DB;
using Market.IO;
using Market.Tools;
using System;
using System.Collections.Generic;

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
                        ShowDataFlow();
                        Console.WriteLine("\n\n");
                        break;
                    case Commands.InportData:
                        Console.Clear();
                        InportDataFlow();
                        Console.WriteLine("\n\n");
                        break;
                    case Commands.Exit:
                        return;
                    default:
                        Console.WriteLine("Unknown Command \n");
                        break;
                }
            }
        }

        static void ShowDataFlow()
        {
            try
            {
                Dialog.ShowTable(DBTools.GetTable());
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
            }
        }

        static void InportDataFlow()
        {
            List<Product> products = null;

            try
            {
                products = IOTools.ReadFile(Dialog.AskFilePath());
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
                return;
            }

            if (products != null)
            {
                try
                {
                    DBTools.InportData(products);
                }
                catch(Exception ex)
                {
                    WriteError(ex.Message);
                    if (ex.InnerException != null)
                        WriteError(ex.InnerException.Message);
                }
            }
        }

        static void WriteError(string errorText)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorText);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}