using System;
using System.Linq;

namespace Lab5
{
    abstract class Engine
    {
        public double Power { get; set; }

        public Engine() { Power = 0; Console.WriteLine("[Engine] Викликано конструктор за замовчуванням"); }
        public Engine(double power) { Power = power; Console.WriteLine("[Engine] Викликано конструктор з 1 параметром (Power)"); }
        public Engine(double power, bool log) { Power = power; if(log) Console.WriteLine("[Engine] Викликано конструктор з 2 параметрами"); }

        ~Engine() { Console.WriteLine("[Engine] Деструктор"); }

        public abstract void Show();
    }

    class InternalCombustionEngine : Engine
    {
        public int Cylinders { get; set; }

        public InternalCombustionEngine() : base() { Cylinders = 4; Console.WriteLine("[ICE] Викликано конструктор за замовчуванням"); }
        public InternalCombustionEngine(double power, int cylinders) : base(power) { Cylinders = cylinders; Console.WriteLine("[ICE] Викликано конструктор з 2 параметрами"); }
        public InternalCombustionEngine(double power, int cylinders, bool log) : base(power, log) { Cylinders = cylinders; Console.WriteLine("[ICE] Викликано конструктор з 3 параметрами"); }

        ~InternalCombustionEngine() { Console.WriteLine("[ICE] Деструктор"); }

        public override void Show()
        {
            Console.WriteLine($"[ДВЗ] Потужність: {Power} к.с., Циліндри: {Cylinders}");
        }
    }

    class DieselEngine : InternalCombustionEngine
    {
        public bool HasTurbo { get; set; }

        public DieselEngine() : base() { HasTurbo = false; Console.WriteLine("[Diesel] Викликано конструктор за замовчуванням"); }
        public DieselEngine(double power, int cylinders, bool hasTurbo) : base(power, cylinders) { HasTurbo = hasTurbo; Console.WriteLine("[Diesel] Викликано конструктор з 3 параметрами"); }
        public DieselEngine(double power, int cylinders, bool hasTurbo, bool log) : base(power, cylinders, log) { HasTurbo = hasTurbo; Console.WriteLine("[Diesel] Викликано конструктор з 4 параметрами"); }

        ~DieselEngine() { Console.WriteLine("[Diesel] Деструктор"); }

        public override void Show()
        {
            Console.WriteLine($"[Дизель] Потужність: {Power} к.с., Циліндри: {Cylinders}, Турбіна: {(HasTurbo ? "Так" : "Ні")}");
        }
    }

    class JetEngine : Engine
    {
        public double Thrust { get; set; }

        public JetEngine() : base() { Thrust = 0; Console.WriteLine("[JetEngine] Викликано конструктор за замовчуванням"); }
        public JetEngine(double power, double thrust) : base(power) { Thrust = thrust; Console.WriteLine("[JetEngine] Викликано конструктор з 2 параметрами"); }
        public JetEngine(double power, double thrust, bool log) : base(power, log) { Thrust = thrust; Console.WriteLine("[JetEngine] Викликано конструктор з 3 параметрами"); }

        ~JetEngine() { Console.WriteLine("[JetEngine] Деструктор"); }

        public override void Show()
        {
            Console.WriteLine($"[Реактивний Двигун] Потужність: {Power} к.с., Тяга: {Thrust} кН");
        }
    }


    abstract class Function
    {
        public abstract double Calculate(double x);
        public abstract void PrintInfo();
    }

    class Line : Function
    {
        public double A { get; set; }
        public double B { get; set; }
        public Line(double a, double b) { A = a; B = b; }
        public override double Calculate(double x) => A * x + B;
        public override void PrintInfo() => Console.WriteLine($"Пряма: y = {A}x + {B}");
    }

    class Quadratic : Function
    {
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
        public Quadratic(double a, double b, double c) { A = a; B = b; C = c; }
        public override double Calculate(double x) => A * x * x + B * x + C;
        public override void PrintInfo() => Console.WriteLine($"Парабола: y = {A}x^2 + {B}x + {C}");
    }

    class Hyperbola : Function
    {
        public double K { get; set; }
        public Hyperbola(double k) { K = k; }
        public override double Calculate(double x) => (x == 0) ? double.NaN : K / x;
        public override void PrintInfo() => Console.WriteLine($"Гіпербола: y = {K}/x");
    }

    sealed partial class Triangle
    {
        private int a, b, c;
        private int color;

        public Triangle(int a, int b, int c, int color)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.color = color;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ЗАВДАННЯ 1 та 2 (Двигуни)");
            
            Engine[] engines = new Engine[]
            {
                new InternalCombustionEngine(150, 4),
                new InternalCombustionEngine(200, 6, true),
                new DieselEngine(120, 4, true),
                new DieselEngine(300, 8, false, true),
                new JetEngine(50000, 150),
                new JetEngine()
            };

            Console.WriteLine("\n Масив двигунів (впорядкований за потужністю)");
            var sortedEngines = engines.OrderBy(e => e.Power).ToArray();
            foreach (var e in sortedEngines)
            {
                e.Show();
            }

            Console.WriteLine("\nЗАВДАННЯ 3 (Функції)");
            Function[] functions = new Function[]
            {
                new Line(2, 5),
                new Quadratic(1, -3, 2),
                new Hyperbola(10)
            };

            double pointX = 2.5;
            Console.WriteLine($"Обчислення значень функцій у точці x = {pointX}:");
            foreach (var f in functions)
            {
                f.PrintInfo();
                Console.WriteLine($"  Значення: {f.Calculate(pointX)}");
            }

            Console.WriteLine("\n ЗАВДАННЯ 4 (Частковий запечатаний клас Triangle)");
            Triangle t = new Triangle(3, 4, 5, 1);
            t.PrintSides();
            Console.WriteLine($"Площа: {t.GetArea()}");

            Console.WriteLine("\n ЗВІЛЬНЕННЯ ПАМ'ЯТІ (Спрацьовування деструкторів)");
            engines = null;
            functions = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Console.WriteLine("Програма завершена.");
        }
    }
}