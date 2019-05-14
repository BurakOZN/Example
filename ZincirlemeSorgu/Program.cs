using System;
using System.Collections.Generic;
using static System.Console;

namespace ZincirlemeSorgu
{
    // Class'lar gelisime acik ama degisime kapali olmali
    public enum Color
    {
        Red, Green, Blue
    }

    public enum Size
    {
        Small, Medium, Large, Yuge
    }

    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;

        public Product(string name, Color color, Size size)
        {
            Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
            Color = color;
            Size = size;
        }
    }
    public interface IFilter<T>
    {
        ProductFilter FilterByColor(Color color);
        ProductFilter FilterBySize(Size size);
        List<T> ToList();
    }
    public class ProductFilter : IFilter<Product>
    {
        private List<Product> products;
        private List<Product> productNew = new List<Product>();

        public ProductFilter(List<Product> pr)
        {
            products = pr;//Dışarıdan gelene Listi almak için
        }

        public ProductFilter FilterByColor(Color color)// Metodları tekrar kullanabilmek için kendi classını döndürür.
        {
            foreach (var p in products)
                if (p.Color == color)
                    productNew.Add(p);
            products = null;
            var productFilter = new ProductFilter(productNew);
            return productFilter;
        }

        public ProductFilter FilterBySize(Size size)
        {
            foreach (var p in products)
                if (p.Size == size)
                    productNew.Add(p);
            products = null;//mevcut class Listi boşa çıkartılır.
            var productFilter = new ProductFilter(productNew);//yeni list ile yeni PF new'lenir..
            return productFilter;

        }
        public List<Product> ToList()//Metodlardan sonra product kullanılmak için çağırılır. Çıkış metodu olarak düşünülebilir.
        {
            return products;
        }
    }

    public class Demo
    {
        static void Main(string[] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);

            List<Product> products = new List<Product>();
            products.Add(apple);
            products.Add(tree);
            products.Add(house);

            var pf = new ProductFilter(products);
            WriteLine("Green products (old):");
            foreach (var p in pf.FilterByColor(Color.Green).FilterBySize(Size.Large).ToList())
                WriteLine($" - {p.Name} is gren");
        }
    }
}
