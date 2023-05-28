using System;
using System.IO;
using System.Collections.Generic;

namespace toko
{

class Program
{
    public static void Main()
    {
        string filePath = "stock.txt";
        Stock<Product> stock = new Stock<Product>(filePath);
        Product newProduct = new Product
        {
            Name = "tahu",
            Price = 30000,
            Stock = 20
        };
        stock.Add(newProduct);
        stock.Remove(3);


        foreach (Product product in stock.Products)
        {
            Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}, Stock: {product.Stock}");
        }
    }

}
}

