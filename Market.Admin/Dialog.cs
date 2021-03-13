using System;

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
    }
}