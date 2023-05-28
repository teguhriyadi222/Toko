

using System;
using System.IO;
using System.Collections.Generic;

namespace toko;
class Product
{
    public int Id { get; set; }
    public string Name { get; set;}
    public decimal Price { get; set; }
    public int Stock { get; set; }
}

class Stock<T> where T : Product
{
    private int productId;
    public List<T> Products { get; set; }

    private string filePath; 

    public Stock(string filePath)
    {
        this.filePath = filePath;
        Products = LoadStock();
    }
     protected bool productName(string productname)
    {

        return Products.Any(p => p.Name == productname);
    }

    public void Add(T product)
    {
        if (productName(product.Name))
        {
            throw new ArgumentException("Product with the same name already exists in stock.");
        }
        else
        {
            product.Id = Products.Count + 1;
            Products.Add(product);
            Save();

        }
    }

    public void Update(int productId, T update)
    {
        T product = Products.Find(p => p.Id == productId);

        if (product != null)
        {
            product.Name = update.Name;
            product.Price = update.Price;
            product.Stock = update.Stock;
            Save();
        }
    }

    public void Remove(int productId)
    {
        Products.RemoveAll(p => p.Id == productId);
        Save();
    }

    private void Save()
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (T product in Products)
            {
                string line = $"{product.Id},{product.Name},{product.Price},{product.Stock}";
                writer.WriteLine(line);
            }
        }
    }

    private List<T> LoadStock()
    {
        List<T> products = new List<T>();
        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(',');

                    int id = int.Parse(data[0]);
                    string name = data[1];
                    decimal price = decimal.Parse(data[2]);
                    int stock = int.Parse(data[3]);

                    T product = Activator.CreateInstance<T>();
                    product.Id = id;
                    product.Name = name;
                    product.Price = price;
                    product.Stock = stock;

                    products.Add(product);
                }
            }
        }
        return products;
    }
}
