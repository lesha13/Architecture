using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    internal class Program
    {
        class Product
        {
            public string name { get; set; }
            public float price { get; set; }

            public Product(string name, float price)
            {
                this.name = name;
                this.price = price;
            }
        }
        interface IImplementation
        {
            string OperationImplementation();
        }
        class Sales
        {
            public int X;
            public int Y;
            public int Z;
            public int N;
            public float A;
            public Sales(int x, int y, int z, int n, float a)
            {
                X = x;
                Y = y;
                Z = z;
                N = n;
                A = a;
            }
        }
        class Abstraction
        {
            protected IImplementation _implementation;
            public Abstraction(IImplementation implementation)
            {
                this._implementation = implementation;
            }
            public virtual string Operation()
            {
                return "Abstract: Base operation with:\n" +
                _implementation.OperationImplementation();
            }
        }
        class Order: Abstraction
        {
            public Sales sales { get; set; }
            public List<Product> products { get; set; } = new List<Product>();

            public Order(IImplementation implementation, Sales sales) : base(implementation)
            {
                this.sales = sales;
            }

            public Order Add(Product product)
            {
                products.Add(product);
                return this;
            }

            public override string Operation()
            {
                float sale = 0;
                float totalPrice = products.Sum(item => item.price);
                if (products.Count > 2)
                {
                    Console.WriteLine("If you by more than 2 products: 10% sale");
                    sale += sales.X;
                }
                if (_implementation.GetType().Name == "BuyWithSelfTake")
                {
                    Console.WriteLine("If you by more than 2 products: 10% sale");
                    sale += sales.Y;
                }
                if (totalPrice > sales.A)
                {
                    Console.WriteLine("If you by more than 2 products: 10% sale");
                    sale += sales.Z;
                }
                return $"{_implementation.OperationImplementation()} {totalPrice}$";
            }
        }
        class BuyWithDebit : IImplementation
        {
            public string OperationImplementation()
            {
                return "Bought with debit:";
            }
        }
        class BuyWithLoan : IImplementation
        {
            public string OperationImplementation()
            {
                return "Bought with loan:";
            }
        }
        class BuyWithSending : IImplementation
        {
            public string OperationImplementation()
            {
                return "Bought by sending money:";
            }
        }
        class BuyWithSelfTake : IImplementation
        {
            public string OperationImplementation()
            {
                return "Bought when took by yourself:";
            }
        }
        static void Main(string[] args)
        {
            Sales s = new Sales(10, 10, 10, 2, 300);
            Order o1 = new Order(new BuyWithDebit(), s)
                .Add(new Product("AAA", 123))
                .Add(new Product("BBB", 123))
                .Add(new Product("BBB", 123));

            Order o2 = new Order(new BuyWithLoan(), s)
                .Add(new Product("AAA", 123))
                .Add(new Product("BBB", 123))
                .Add(new Product("BBB", 123));

            Order o3 = new Order(new BuyWithSending(), s)
                .Add(new Product("AAA", 123))
                .Add(new Product("BBB", 123))
                .Add(new Product("BBB", 123));

            Order o4 = new Order(new BuyWithSelfTake(), s)
                .Add(new Product("AAA", 123))
                .Add(new Product("BBB", 123))
                .Add(new Product("BBB", 123));

            Console.WriteLine(o1.Operation());
            Console.WriteLine(o2.Operation());
            Console.WriteLine(o3.Operation());
            Console.WriteLine(o4.Operation());

            //Console.WriteLine(new BuyWithSelfTake().GetType().Name);
        }
    }
}
