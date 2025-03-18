using System;
using System.Linq;

class Triangle
{
    protected int a, b, c;
    protected int color;

    public Triangle(int a, int b, int c, int color)
    {
        if (!IsValidTriangle(a, b, c))
            throw new ArgumentException("Трикутник з такими сторонами не існує.");

        this.a = a;
        this.b = b;
        this.c = c;
        this.color = color;
    }

    public int A
    {
        get { return a; }
        set
        {
            if (IsValidTriangle(value, b, c))
                a = value;
            else
                throw new ArgumentException("Недопустиме значення сторони трикутника.");
        }
    }

    public int B
    {
        get { return b; }
        set
        {
            if (IsValidTriangle(a, value, c))
                b = value;
            else
                throw new ArgumentException("Недопустиме значення сторони трикутника.");
        }
    }

    public int C
    {
        get { return c; }
        set
        {
            if (IsValidTriangle(a, b, value))
                c = value;
            else
                throw new ArgumentException("Недопустиме значення сторони трикутника.");
        }
    }

    public int Color => color;

    private static bool IsValidTriangle(int a, int b, int c)
    {
        return (a + b > c) && (a + c > b) && (b + c > a);
    }

    public void PrintSides()
    {
        Console.WriteLine($"Сторони трикутника: a = {a}, b = {b}, c = {c}");
    }

    public int Perimeter() => a + b + c;

    public double Area()
    {
        double p = Perimeter() / 2.0;
        return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
    }
}

class Engine
{
    protected int power;
    protected double weight;

    public Engine(int power, double weight)
    {
        this.power = power;
        this.weight = weight;
    }

    public virtual void Show()
    {
        Console.WriteLine($"Двигун: Потужність = {power} к.с., Вага = {weight} кг");
    }

    public int GetPower() => power;
}

class InternalCombustionEngine : Engine
{
    protected string fuelType;

    public InternalCombustionEngine(int power, double weight, string fuelType)
        : base(power, weight)
    {
        this.fuelType = fuelType;
    }

    public override void Show()
    {
        Console.WriteLine($"ДВЗ: Потужність = {power} к.с., Вага = {weight} кг, Паливо = {fuelType}");
    }
}

class DieselEngine : InternalCombustionEngine
{
    protected int torque;

    public DieselEngine(int power, double weight, int torque)
        : base(power, weight, "Дизельне паливо")
    {
        this.torque = torque;
    }

    public override void Show()
    {
        Console.WriteLine($"Дизельний двигун: Потужність = {power} к.с., Вага = {weight} кг, Крутний момент = {torque} Н·м");
    }
}

class JetEngine : Engine
{
    protected int thrust;

    public JetEngine(int power, double weight, int thrust)
        : base(power, weight)
    {
        this.thrust = thrust;
    }

    public override void Show()
    {
        Console.WriteLine($"Реактивний двигун: Потужність = {power} к.с., Вага = {weight} кг, Тяга = {thrust} Н");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Оберіть завдання:");
        Console.WriteLine("1 - Робота з трикутниками");
        Console.WriteLine("2 - Робота з двигунами");
        int choice = int.Parse(Console.ReadLine());

        if (choice == 1)
        {
            Triangle[] triangles =
            {
                new Triangle(3, 4, 5, 1),
                new Triangle(6, 8, 10, 2),
                new Triangle(7, 24, 25, 3)
            };

            foreach (var triangle in triangles)
            {
                triangle.PrintSides();
                Console.WriteLine($"Периметр: {triangle.Perimeter()}");
                Console.WriteLine($"Площа: {triangle.Area():F2}");
                Console.WriteLine($"Колір: {triangle.Color}");
                Console.WriteLine();
            }
        }
        else if (choice == 2)
        {
            Engine[] engines = new Engine[]
            {
                new InternalCombustionEngine(150, 180, "Бензин"),
                new DieselEngine(200, 250, 500),
                new JetEngine(1000, 800, 5000),
                new DieselEngine(180, 230, 450),
                new JetEngine(1200, 900, 6000)
            };

            Console.WriteLine("Список двигунів:");
            foreach (var engine in engines)
            {
                engine.Show();
            }

            var sortedEngines = engines.OrderBy(e => e.GetPower()).ToArray();

            Console.WriteLine("\nДвигуни після сортування за потужністю:");
            foreach (var engine in sortedEngines)
            {
                engine.Show();
            }
        }
        else
        {
            Console.WriteLine("Невірний вибір.");
        }
    }
}
