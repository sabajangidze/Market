using System;
using System.Data;

namespace Market.Admin
{
    public static class Dialog
    {
        public static int ShowCommands()
        {
            string[] commands = Enum.GetNames((typeof(Commands)));
            
            for (int i = 0; i < commands.Length; i++)
            {
                Console.WriteLine($"[{i}] {commands[i]}");
            }

            Console.WriteLine("\n");
            Console.Write("Enter Command Number: ");
            int order = int.Parse(Console.ReadLine());

            if (0 <= order && order < commands.Length)
                return order;

            throw new ArgumentOutOfRangeException("Argument is Out Of Range");
        }

        public static void ShowTable(DataTable table)
        {
            int[] sizes = { 13, 15, 16, 9 };
            int index = 0;

            foreach (var column in table.Columns)
            {
                Console.Write($"{column.ToString()}   |");
            }
            Console.WriteLine();
            PrintBottom();

            foreach (DataRow row in table.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    Console.Write($"{item}{new string(' ', sizes[index++] - item.ToString().Length-1)}|");
                }
                Console.WriteLine();
                PrintBottom();
                index = 0;
            }

            void PrintBottom()
            {
                for (int i = 0; i < sizes.Length; i++)
                {
                    Console.Write(new string('-', sizes[i]));
                }
                Console.WriteLine();
            }
        }
    }
}