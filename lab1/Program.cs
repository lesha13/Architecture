using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    internal class Program
    {
        class SQLQuerry
        {
            public List<object> parts = new List<object>();

            public void Add(string part)
            {
                this.parts.Add(part);
            }

            public override string ToString()
            {
                return String.Join("", this.parts);
            }
        }

        public interface IBuilder
        {
            IBuilder AddSelect(string field);
            IBuilder AddWhere(string field);
            IBuilder AddOrderBy(string field);
        }

        class SQLHandler : IBuilder
        {
            private SQLQuerry querry = new SQLQuerry();

            public void Reset()
            {
                querry = new SQLQuerry();
            }

            public IBuilder AddSelect(string field)
            {
                if (this.querry.parts.Contains("*"))
                {
                    throw new Exception("Already selected all");
                }

                if (!this.querry.parts.Contains("SELECT "))
                {
                    this.querry.Add("SELECT ");
                }
                else
                {
                    this.querry.Add(", ");
                }
                this.querry.Add(field);
                return this;
            }
            public IBuilder AddSelect()
            {
                if (!this.querry.parts.Contains("SELECT "))
                {
                    this.querry.Add("SELECT ");
                    this.querry.Add("*");
                }
                else
                {
                    throw new Exception("Already selected all");
                }
                return this;
            }

            public IBuilder AddWhere(string field)
            {
                if (!this.querry.parts.Contains("WHERE "))
                {
                    this.querry.Add(Environment.NewLine);
                    this.querry.Add("WHERE ");
                }
                else
                {
                    this.querry.Add(" AND ");
                }                    
                this.querry.Add(field);
                return this;
            }

            public IBuilder AddOrderBy(string field)
            {
                if (!this.querry.parts.Contains("ORDER BY "))
                {
                    this.querry.Add(Environment.NewLine);
                    this.querry.Add("ORDER BY ");
                }
                else
                {
                    this.querry.Add(", ");
                }
                this.querry.Add(field);
                return this;
            }

            public SQLQuerry GetQuerry()
            {
                return this.querry;
            }
        }

        static void Main(string[] args)
        {
            SQLHandler handler = new SQLHandler();
            handler
                .AddSelect("column1")
                .AddSelect("column2")
                .AddSelect("column3")
                .AddWhere("column1 > 20")
                .AddWhere("column2 < 50")
                .AddWhere("column3 < 50")
                .AddOrderBy("column1")
                .AddOrderBy("column2")
                .AddOrderBy("column3");
            Console.WriteLine(handler.GetQuerry());

            /*
            handler.Reset();
            handler
                .AddSelect()
                .AddSelect("column1")
                .AddWhere("column1 > 20")
                .AddWhere("column2 < 50")
                .AddWhere("column3 < 50")
                .AddOrderBy("column1")
                .AddOrderBy("column2")
                .AddOrderBy("column3");
            Console.WriteLine(handler.querry);
            */
        }
    }
}
