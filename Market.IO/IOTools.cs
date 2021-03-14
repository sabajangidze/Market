using System;
using System.IO;
using System.Collections.Generic;
using Market.Tools;

namespace Market.IO
{
    public class IOTools
    {
        public static List<Product> ReadFile(string path)
        {
            if (!CheckFileExistence(path))
                throw new FileNotFoundException("Can not locate file");

            List<Product> products = new List<Product>();

            using(StreamReader reader = new StreamReader(path))
            {
                while (!(reader.EndOfStream))
                {
                    string line = reader.ReadLine();
                    products.Add((Product)line.Split(':'));
                }
            }

            return products;
        }

        private static bool CheckFileExistence(string path)
        {
            if (File.Exists(path))
                return true;

            return false;
        }
    }
}