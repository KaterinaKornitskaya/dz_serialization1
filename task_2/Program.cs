using System.Runtime.Serialization;
using System.Text.Json;
using System.Xml.Serialization;

namespace task_2
{
    [Serializable]
    [DataContract]
    public class Journal
    {
        [DataMember]
        public string? Name { get; set; }
        [DataMember]
        public string? ProducerName { get; set; }
        [DataMember]
        public DateTime DateOfIssue;
        [DataMember]
        public int? NumberOfPages { get; set; }
        [DataMember]
        public List<Article> Articles;

        public Journal() { }
        public Journal(string name, string productName, int numberOfPages, DateTime dateOfIssue, List<Article> articles)
        {
            Name = name;
            ProducerName = productName;
            NumberOfPages = numberOfPages;
            DateOfIssue = dateOfIssue;
            Articles = articles;
        }
        public Journal InputInfo()
        {
            Console.WriteLine("Enter name of journal:");
            string? name = Console.ReadLine();

            Console.Write("Enter name of journal producer:");
            string? producerName = Console.ReadLine();

            Console.WriteLine("Enter date of creation:");
            int day;
            int month;
            int year;
            Console.Write("Day: ");
            day = Convert.ToInt32(Console.ReadLine());
            Console.Write("Month: ");
            month = Convert.ToInt32(Console.ReadLine());
            Console.Write("Year: ");
            year = Convert.ToInt32(Console.ReadLine());
            DateTime dateOfIssue = new DateTime(year, month, day);

            Console.Write("Enter number of pages: ");
            int numberOfPages = Convert.ToInt32(Console.ReadLine());
            List<Article>? articles = new List<Article>();

            Console.Write("Enter how much articles you want add: ");
            int count = Convert.ToInt32(Console.ReadLine());
            while (count != 0)
            {
                Article article = new Article();
                article.InputArticleData();
                articles.Add(article);
                count--;
            }
            Journal journal = new Journal(name, producerName, numberOfPages, dateOfIssue, articles);
            return journal;
        }
        public void ShowAll()
        {
            Console.WriteLine(this.ToString());
            Console.WriteLine("---------------------------------------");
            foreach (var item in Articles)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("---------------------------------------");
            Console.ResetColor();

        }

        public override string ToString()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            return $"Name of journal: {Name}\n" +
                   $"Name of producer: {ProducerName}\n" +
                   $"Number of pages: {NumberOfPages}\n" +
                   $"Date of issue: {DateOfIssue.ToShortDateString()}\n";
        }
    }
    [Serializable]
    [DataContract]
    public class Article
    {
        [DataMember]
        public string? ArticleText { get; set; }
        [DataMember]
        public string? ArticleAnnouncement { get; set; }
        [DataMember]
        public int? SymbolsAmount;

        public Article() { }

        public Article(string? articleText, string? articleAnnouncement)
        {
            ArticleText = articleText;
            ArticleAnnouncement = articleAnnouncement;
            SymbolsAmount = articleText?.Length;
        }
        public void InputArticleData()
        {
            Console.WriteLine("Enter article announcement: ");
            ArticleAnnouncement = Console.ReadLine();
            Console.WriteLine("Enter full article:");
            ArticleText = Console.ReadLine();
            SymbolsAmount = ArticleText?.Length;
        }
        public override string ToString()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            return $"Article announcement: {ArticleAnnouncement}\n" +
                $"Article text\n{ArticleText}\n" +
                $"Amount of symbols in article: {SymbolsAmount}\n";

        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //Ex1()
            Ex2_3_4();
        }
        static void Ex1()
        {
            int size;
            Console.WriteLine("Enter size of array");
            size = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("--------------------------------------------");

            double[]? arr = new double[size];

            for (int i = 0; i < size; i++)
            {
                Console.Write($"Enter {i + 1} element of array: ");
                arr[i] = Convert.ToDouble(Console.ReadLine());
            }
            Console.WriteLine("--------------------------------------------");

            Console.WriteLine("Entered array:");
            foreach (var item in arr)
                Console.Write(item + "  ");

            Console.WriteLine("\n--------------------------------------------");

            FileStream stream = new FileStream("text.xml", FileMode.Create, FileAccess.Write);
            XmlSerializer serializer = new XmlSerializer(typeof(double[]));

            serializer.Serialize(stream, arr);
            if (stream.CanWrite)
                Console.WriteLine("Array has been serialised succesfully)))");
            stream.Close();
            Console.WriteLine("--------------------------------------------");

            stream = new FileStream("text.xml", FileMode.Open, FileAccess.Read);
            if (stream.CanRead)
            {
                arr = (double[])serializer.Deserialize(stream);
                Console.WriteLine("Deserialize successfully");
                Console.WriteLine("--------------------------------------------");
            }

            stream.Close();
            stream.Dispose();

            Console.WriteLine("Array after deserialization:");
            foreach (var item in arr)
                Console.Write(item + "  ");
        }
        static void Ex2_3_4()
        {
            Journal[] journals =
            {
                new Journal("Journal1", "Producer1", 150, new DateTime(1995,01,01), new List<Article>
                {
                    new Article("dfgadgagg", "A"),
                    new Article("sefwf sdfasgsa wetawegt", "B"),
                    new Article("werrrrrwr", "C"),
                    new Article("argaetese", "D")
                }),
                new Journal("Journal2", "Producer2", 130, new DateTime(2000,05,01), new List<Article>
                {
                    new Article("qWEQEQWEQE", "A"),
                    new Article("SADAD", "B"),
                    new Article("ASD", "C"),
                    new Article("YILUILI NUI", "D")
                }),
                new Journal("Journal3", "Producer3", 200, new DateTime(1999,08,01), new List<Article>
                {
                    new Article("qweqeqeqe", "A"),
                    new Article("ghjg", "B"),
                    new Article("gjghj jhgjghj gjghjghjghj gjghjghjgj", "C"),
                    new Article("gjhghjghj", "D")
                }),
                new Journal("Journal4", "Producer4", 120, new DateTime(2015,01,01), new List<Article>
                {
                    new Article("qweqeqeqe sdfsdf sfsdf", "A"),
                    new Article("ghjg sdfsfsdf", "B"),
                    new Article("gjghj gjghjghjghj gjghjghjgj", "C"),
                    new Article("sfsdfsdf", "D")
                })
            };
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Before serialization:");
            Console.ResetColor();
            foreach (var item in journals)
                item.ShowAll();
            Console.WriteLine("-------------------------------------------");

            FileStream stream = new FileStream("journals.xml", FileMode.Create, FileAccess.Write);
            XmlSerializer serializer = new XmlSerializer(typeof(Journal[]));
            if (stream.CanWrite)
            {
                serializer.Serialize(stream, journals);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Serialization successfully");
                stream.Close();
                stream.Dispose();
                Console.WriteLine("-------------------------------------------");
                Console.ResetColor();
            }

            stream = new FileStream("journals.xml", FileMode.Open, FileAccess.Read);
            if (stream.CanRead)
            {
                journals = (Journal[])serializer.Deserialize(stream);
                Console.ForegroundColor= ConsoleColor.Green;
                Console.WriteLine("Deserialization succesfully");
                stream.Close();
                stream.Dispose();
                Console.WriteLine("-------------------------------------------");
                Console.ResetColor();
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("After deserialization:");
            Console.ResetColor();
            foreach (var item in journals)
                item.ShowAll();

        }
    }
}