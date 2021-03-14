using System;

namespace Market.Tools
{
    public class Product
    {
        public Product()
        {

        }

        public Product(string categoryName, string productName, double price)
        {
            CategoryName = categoryName;
            ProductName = productName;
            Price = price;
        }
        
        public string CategoryName { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }

        public static explicit operator Product(object[] data)
        {
            if (data.Length < 0 || data.Length > 3)
                throw new InvalidCastException("Invalid array");

            return new Product((string)data[0], (string)data[1], Convert.ToDouble(data[2]));
        }
    }
}
