namespace DSA
{
    class Search
    {
        public static void SearchMain()
        {
            Console.WriteLine("Select search: Ordered Int, Unordered Int, String, Contact, Exit");
            string input = Console.ReadLine();
            
            switch (input)
            {
                case "Unordered Int":
                    UnorderedInt();
                    break;
                case "Ordered Int":
                    OrderedInt();
                    break;
                case "String":
                    String();
                    break;
                case "Contact":
                    Contacts();
                    break;
                case "Exit":
                    return;
                default:
                    Console.WriteLine("Incorrect Input");
                    break;
            }
        }

        static void UnorderedInt()
        {
            int[] nums = { 4, 6, 1, 0, 10, 3 };

            Console.WriteLine("Int to be found in list 4, 6, 1, 0, 10, 3");
            int find = int.Parse(Console.ReadLine());

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == find)
                {
                    Console.WriteLine($"Found at index {i}");
                    return;
                }
            }
            Console.WriteLine("Not found");
        }
      
        static void OrderedInt()
        {
            Console.WriteLine("Num of ints:");
            int num = int.Parse(Console.ReadLine());
            int[] nums = new int[num];
            for (int i = 0; i < num; i++)
            {
                nums[i] = i;
            }

            Console.WriteLine("Int to be found:");
            int find = int.Parse(Console.ReadLine());

            if (find < 0 || find >= num) {
                Console.WriteLine("Out of bounds");
                return; 
            }

            int lower = 0;
            int upper = num - 1;
            int middle = -1;
            bool found = false;

            while (lower <= upper && !found)
            {
                middle = (lower + upper) / 2;
                if (find < nums[middle])
                {
                    upper = middle - 1;
                }
                else if(find > nums[middle])
                {
                    lower = middle + 1;
                }
                else
                {
                    found = true;
                }
            }

            if (found)
            {
                Console.WriteLine($"Found at position {middle}");
            } 
            else
            {
                Console.WriteLine($"Not found");
            }
        }

        static void String()
        {
            Console.WriteLine("String to be found in list \"Bill\", \"Charlie\", \"Fred\", \"Ginny\", \"Percy\", \"Ron\"");
            string find = Console.ReadLine();

            string[] names = {"Bill", "Charlie", "Fred", "Ginny", "Percy", "Ron"};
            for (int i = 0; i < names.Length; i++)
            {
                if (names[i].Equals(find))
                {
                    Console.WriteLine($"Found at index {i}");
                    return;
                }
            }
            Console.WriteLine("Not found");
        }

        static void Contacts() {
            Console.WriteLine("Name of contact in list \"Bill\", \"Charlie\", \"Fred\"");
            string find = Console.ReadLine();

            People[] peeps = new People[3];
            peeps[0] = new People();
            peeps[0].name = "Bill";
            peeps[0].number = "+1 202-918-2132";

            peeps[1] = new People();
            peeps[1].name = "Charlie";
            peeps[1].number = "+1 505-644-2944";

            peeps[2] = new People();
            peeps[2].name = "Fred";
            peeps[2].number = "+1 225-463-2824";

            for (int i = 0; i < peeps.Length; i++)
            {
                if (peeps[i].name.Equals(find))
                {
                    Console.WriteLine("Found contact " + peeps[i].name);
                    Console.WriteLine("Number: " + peeps[i].number);
                    return;
                }
            }
            Console.WriteLine("Not found");
        }

        public class People
        {
            public string name { get; set; }
            public string number { get; set; }
        }
    }
    
}