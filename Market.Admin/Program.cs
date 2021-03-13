using System;

namespace Market.Admin
{
    class Program
    {
        static void Main(string[] args)
        {
            int order = Dialog.ShowCommands();
            Console.WriteLine((Commands)order);
        }
    }
}