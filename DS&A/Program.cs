namespace DSA
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Select program: Datatypes, Search, Sort");
            string input = Console.ReadLine();

            switch (input)
            {
                case "Datatypes":
                    Datatypes.DTMain();
                    break;
                case "Search":
                    Search.SearchMain();
                    break;
                case "Sort":
                    Sort.SortMain();
                    break;

            }
        }
    }
}