using System;

namespace CS50
{
    class Seacrh
    {
        public static void SearchMain()
        {
            Console.WriteLine("Select search: OInt, !OInt, String, Contact");
            string input = Console.ReadLine();
            
            switch (input)
            {
                case "!OInt":
                    int[] UOnums = { 4, 6, 1, 0, 10, 3 };
                    UnorderedInt(UOnums);
                    break;
                case "OInt":
                    Console.WriteLine("Num of ints:");
                    int Onum = int.Parse(Console.ReadLine());
                    int[] Onums = new int[Onum];
                    for (int i = 0; i < Onum; i++)
                    {
                        Onums[i] = i;
                    }
                    OrderedInt(Onum, Onums);
                    break;
                case "String":
                    String();
                    break;
                case "Contact":
                    Contacts();
                    break;
                default:
                    Console.WriteLine("Incorrect Input");
                    break;
            }
        }

        static void UnorderedInt(int[] nums)
        {
            Console.WriteLine("Int to be found");
            int find = int.Parse(Console.ReadLine());

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == find)
                {
                    Console.WriteLine("Found");
                    return;
                }
            }
            Console.WriteLine("Not found");
        }
        static void OrderedInt(int num, int[] nums)
        {
            Console.WriteLine("Int to be found:");
            int find = int.Parse(Console.ReadLine());


            while (true)
            {
                if (find < 0 && find < num) {
                    Console.WriteLine("Out of bounds");
                    return; 
                }
                int x = num / 2;
                int i;
                for (i = 2; i <= num; i++)
                {
                    if (nums[x] > find)
                    {
                        Console.WriteLine(i + "/" + (2 * i) );
                        x -= num / (2*i);
                        continue;
                    }
                    else if (nums[x] < find)
                    {
                        x += num / (2*i);
                        continue;
                    }
                    Console.WriteLine("Found");
                    i = num + 1;
                }

                if (i <= num)
                {
                    Console.WriteLine("Not found");
                }
                Console.WriteLine("Int to be found:");
                find = int.Parse(Console.ReadLine());
            }
        }

        static void String()
        {
            Console.WriteLine("String to be found");
            string find = Console.ReadLine();

            string[] names = {"Bill", "Charlie", "Fred", "Ginny", "Percy", "Ron"};
            for (int i = 0; i < names.Length; i++)
            {
                if (names[i].Equals(find))
                {
                    Console.WriteLine("Found");
                    return;
                }
            }
            Console.WriteLine("Not found");
        }

        static void Contacts() {
            Console.WriteLine("Name of contact");
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