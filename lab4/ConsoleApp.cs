using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Program
    {
        public interface IVisitable
        {
            void Accept(ICompositeVisitor visitor);
        }

        public interface ICompositeVisitor
        {   
            void VisitEmployee(Employee employee);
            void VisitChair(Chair chair);
            void VisitFaculty(Faculty faculty);
            void VisitUniversity(University university);

        }
        public interface ICompositeComponent
        {
            ICompositeComponent Add(ICompositeComponent component);
            ICompositeComponent Remove(ICompositeComponent component);
            bool IsComposite { get; }
        }

        public class CompositeComponent : ICompositeComponent, IVisitable
        {
            public string name;

            public CompositeComponent(string name)
            {
                this.name = name;
            }
            public virtual ICompositeComponent Add(ICompositeComponent component)
            {
                throw new Exception($"Can't add {component} to {this}");
            }

            public virtual ICompositeComponent Remove(ICompositeComponent component)
            {
                throw new Exception($"Can't remove {component} from {this}");
            }

            public virtual bool IsComposite { get { return false; } }

            public virtual void Accept(ICompositeVisitor visitor)
            {

            }

            public override string ToString()
            {
                return name;
            }
        }

        public class Employee : CompositeComponent
        {
            public float experience { get; set; }
            public float salary { get; set; }

            public Employee(string name, float experience, float salary) : base(name)
            {
                this.experience = experience;
                this.salary = salary;
            }
            public override string ToString()
            {
                return $"Employee {name} with salary {salary}\n";
            }
            public override void Accept(ICompositeVisitor visitor)
            {
                visitor.VisitEmployee(this);
            }
        }

        public class Chair : CompositeComponent
        {
            public List<Employee> employees { get; set; } = new List<Employee>();

            public Chair(string name) : base(name)
            {

            }
            public ICompositeComponent Add(Employee employee)
            {
                employees.Add(employee);
                return this;
            }
            public ICompositeComponent Remove(Employee employee)
            {
                employees.Remove(employee);
                return this;
            }
            public override bool IsComposite { get { return true; } }
            public override string ToString()
            {   
                string str = string.Empty;
                str += $"Chair: {name}\n";
                foreach (Employee employee in employees)
                {
                    str += "\t\t\t" + employee.ToString();
                }
                return str;
            }
            public override void Accept(ICompositeVisitor visitor)
            {
                visitor.VisitChair(this);
            }
        }

        public class Faculty : CompositeComponent
        {
            public List<Chair> chairs { get; set; } = new List<Chair>();

            public Faculty(string name) : base(name)
            {

            }
            public ICompositeComponent Add(Chair chair)
            {
                chairs.Add(chair);
                return this;
            }
            public ICompositeComponent Remove(Chair chair)
            {
                chairs.Remove(chair);
                return this;
            }
            public override bool IsComposite { get { return true; } }
            public override string ToString()
            {
                string str = string.Empty;
                str += $"Faculty: {name}\n";
                foreach (Chair chair in chairs)
                {
                    str += "\t\t" + chair.ToString();
                }
                return str;
            }
            public override void Accept(ICompositeVisitor visitor)
            {
                visitor.VisitFaculty(this);
            }
        }

        public class University : CompositeComponent
        {
            public List<Faculty> faculties { get; set; } = new List<Faculty>();

            public University(string name) : base(name)
            {
            }
            public ICompositeComponent Add(Faculty faculty)
            {
                faculties.Add(faculty);
                return this;
            }
            public ICompositeComponent Remove(Faculty faculty)
            {
                faculties.Remove(faculty);
                return this;
            }
            public override bool IsComposite { get { return true; } }
            public override string ToString()
            {
                string str = string.Empty;
                str += $"University: {name}\n";
                foreach (Faculty faculty in faculties)
                {
                    str += "\t" + faculty.ToString();
                }
                return str;
            }

            public override void Accept(ICompositeVisitor visitor)
            {
                visitor.VisitUniversity(this);
            }
        }

        public class CountSalaryVisitor : ICompositeVisitor
        { 
            public void VisitEmployee(Employee employee)
            {
                Console.WriteLine(employee);
            }
            public void VisitChair(Chair chair)
            {
                Console.WriteLine($"{chair.name}: {chair.employees.Select(employee => employee.salary).Sum()}");
            }
            public void VisitFaculty(Faculty faculty)
            {
                Console.WriteLine($"{faculty.name}: {faculty.chairs.Select(chair => chair.employees.Select(employee => employee.salary).Sum()).Sum()}");
            }
            public void VisitUniversity(University university)
            {
                Console.WriteLine($"{university.name}: {university.faculties.Select(faculty => faculty.chairs.Select(chair => chair.employees.Select(employee => employee.salary).Sum()).Sum()).Sum()}");
            }
        }
        public class RaiseSalaryVisitor: ICompositeVisitor
        {
            float experience;
            float percentage;

            public RaiseSalaryVisitor(float experience, float percentage)
            {
                this.experience = experience;
                this.percentage = percentage;
            }

            public void VisitEmployee(Employee employee)
            {
                if (employee.experience > this.experience)
                {
                    employee.salary *= (percentage / 100 + 1);
                }
            }
            public void VisitChair(Chair chair)
            {
                chair.employees.ForEach(employee => employee.Accept(this));
            }
            public void VisitFaculty(Faculty faculty)
            {
                faculty.chairs.ForEach(chair => chair.Accept(this));
            }
            public void VisitUniversity(University university)
            {
                university.faculties.ForEach(faculty => faculty.Accept(this));
            }
        }
        static void Main(string[] args)
        {
            University university = new University("Uzhnu");
            /*
            university
                .Add(
                    new Faculty("FMDT")
                        .Add(
                            new Chair("SA")
                                .Add(new Employee("Bob", 10, 1000))
                                .Add(new Employee("Max", 5, 500))
                        )
                        .Add(
                            new Chair("M")
                                .Add(new Employee("Olivia", 12, 1200))
                                .Add(new Employee("Alex", 7, 700))
                        )
                )
                .Add(
                        new Faculty("FIT")
                        .Add(
                            new Chair("WEB")
                                .Add(new Employee("Jack", 11, 1100))
                                .Add(new Employee("Jake", 6, 600))
                        )
                        .Add(
                            new Chair("GameDev")
                                .Add(new Employee("Jason", 20, 2000))
                                .Add(new Employee("Jamal", 8, 800))
                        )
                );
            */
            
            // better to understand

            Employee e1 = new Employee("Bob", 10, 1000);
            Employee e2 = new Employee("Max", 5, 500);
            Employee e3 = new Employee("Olivia", 12, 1200);
            Employee e4 = new Employee("Alex", 7, 700);
            Employee e5 = new Employee("Jack", 11, 1100);
            Employee e6 = new Employee("Jake", 6, 600);
            Employee e7 = new Employee("Jason", 20, 2000);
            Employee e8 = new Employee("Jamal", 8, 800);

            Chair c1 = new Chair("SA");
            Chair c2 = new Chair("M");
            Chair c3 = new Chair("WEB");
            Chair c4 = new Chair("GameDev");

            Faculty f1 = new Faculty("FMDT");
            Faculty f2 = new Faculty("FIT");
            
            c1.Add(e1);
            c1.Add(e2);
            c2.Add(e3);
            c2.Add(e4);
            c3.Add(e5);
            c3.Add(e6);
            c4.Add(e7);
            c4.Add(e8);

            f1.Add(c1);
            f1.Add(c2);
            f2.Add(c3);
            f2.Add(c4);

            university.Add(f1);
            university.Add(f2);

            Console.WriteLine(university);

            CountSalaryVisitor visitor1 = new CountSalaryVisitor();

            e1.Accept(visitor1);
            c1.Accept(visitor1);
            f1.Accept(visitor1);
            university.Accept(visitor1);

            RaiseSalaryVisitor visitor2 = new RaiseSalaryVisitor(5, 100);

            e1.Accept(visitor2);
            Console.WriteLine(e1);
            c1.Accept(visitor2);
            Console.WriteLine(c1);
            f1.Accept(visitor2);
            Console.WriteLine(f1);
            university.Accept(visitor2);
            Console.WriteLine(university);
        }
    }
}
