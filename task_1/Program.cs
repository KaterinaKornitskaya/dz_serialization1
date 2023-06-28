using System.Text.Json;

namespace task_1
{
    class Fraction
    {
        public int numerator { get; set; }
        public int denominator { get; set; }

        public Fraction(int numerator, int denominator)
        {
            this.numerator = numerator;
            this.denominator = denominator;
        }

        public override string ToString()
        {
            return numerator + "/" + denominator;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Fraction[] _fraction = new Fraction[3];

            Console.WriteLine("Введите 3 дроби в формате 'числитель/знаменатель':");

            for (int i = 0; i < 3; i++)
            {
                string[] input = Console.ReadLine().Split('/');
                int numerator = int.Parse(input[0]);
                int denominator = int.Parse(input[1]);
                _fraction[i] = new Fraction(numerator, denominator);
            }
            Console.Clear();
            Console.WriteLine("Получили :");
            foreach (var item in _fraction)
            {
                Console.WriteLine(item);
            }

            string Json = JsonSerializer.Serialize(_fraction);

            Console.WriteLine($"\nПреобразовали в Json: {Json}");

            // Сохранение сериализованного массива в файл
            File.WriteAllText("fractions.json", Json);

            string JsonString = File.ReadAllText("fractions.json");

            // Десериализация массива дробей
            Fraction[] loadedFractions =
            JsonSerializer.Deserialize<Fraction[]>(JsonString);

            Console.WriteLine("\nСериализованный массив дробей:");
            Console.WriteLine(JsonString);
            Console.Read();
        }
    }
}