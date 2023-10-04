using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace CS50
{
    class Sort
    {
        //TODO datatypes: linked list, binary tree, hash map
        public static void SortMain(){
            while (true) {
                Console.WriteLine("Select function: Test, Race, Exit");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "Test":
                        AlgoTest();
                        break;
                    case "Race":
                        AlgoRace();
                        break;
                    case "Exit":
                        return;
                        break;
                    default:
                        Console.WriteLine("Incorrect Input");
                        break;
                }
                Console.WriteLine();
            }
        }
        public static void AlgoRace() {

            Console.WriteLine("Num of ints:");
            int num = int.Parse(Console.ReadLine());
            Console.WriteLine("Num of reps:");
            int reps = int.Parse(Console.ReadLine());
            Console.WriteLine("Select algorithms: Selection, Bubble, Merge");
            string[] names = Console.ReadLine().Split(", ");

            long[] randoms = new long[names.Length];
            long[] nonrandoms = new long[names.Length];

            for (int i = 0; i < reps; i++) {
                Console.Write("rep: " + i + "... ");
                for (int j = 0; j < names.Length; j++) {
                    randoms[j] += Test(names[j], GenerateArr(num, true));
                    nonrandoms[j] += Test(names[j], GenerateArr(num, false));
                }
            }
            Console.WriteLine();
            for (int j = 0; j < names.Length; j++)
            {
                Console.WriteLine(names[j] + " random: " + (randoms[j] / reps) + "ms sorted: " + (nonrandoms[j] / reps) + "ms");
            }

            static long Test(string name, int[] array)
            {
                Stopwatch stopWatch = new();
                stopWatch.Start();
                typeof(Sort).GetMethod(name, BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, new [] {array});
                stopWatch.Stop(); 
                return stopWatch.ElapsedMilliseconds;
            }
        }

        public static void AlgoTest(){
            Console.WriteLine("Select sort: Selection, Bubble, Merge");
            string input = Console.ReadLine();

            Console.WriteLine("Num of ints:");
            int num = int.Parse(Console.ReadLine());
            int[] nums = GenerateArr(num, true);

            switch (input){
                case "Selection":
                    PrintArr(Selection(nums));
                    break;
                case "Bubble":
                    PrintArr(Bubble(nums));
                    break;
                case "Merge":
                    PrintArr(Merge(nums));
                    break;
                default:
                    Console.WriteLine("Incorrect Input");
                    break;
            }
        }

        static int[] GenerateArr(int length, bool isRandom)
        {
            int[] nums = new int[length];
            Random random = new();
            int temp;
            int index;

            for(int i = 0; i < length; i++){
                nums[i] = i;
            }

            if (isRandom){
                for (int i = 0; i < length; i++) {
                    index = random.Next(i, length);
                    temp = nums[i];
                    nums[i] = nums[index];
                    nums[index] = temp;
                }
            }

            return nums;
        }

        static void PrintArr(int[] array){
            for (int i = 0; i < array.Length; i++){
                Console.Write(array[i]);
            }
            Console.WriteLine();
        }

        static int[] Selection(int[] array){
            int sortedUpTO = 0;
            int smallest;
            int smallestId;
            bool sorted = false;

            while (!sorted){
                if(sortedUpTO == array.Length) { sorted = true;  continue; }
                smallestId = sortedUpTO;

                for (int i = sortedUpTO; i < array.Length; i++){
                    if(array[i] < array[smallestId]){
                        smallestId = i;
                    }
                }

                smallest = array[smallestId];
                array[smallestId] = array[sortedUpTO];
                array[sortedUpTO] = smallest;
                sortedUpTO++;
            }

            return array;
        }

        static int[] Bubble(int[] array){
            int temp;
            bool sorted = false;

            while(!sorted){
                bool swapped = false;

                for (int i = 0; i < array.Length-1; i++){
                    if (array[i] > array[i + 1]){
                        temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        swapped = true;
                    }
                }
                if (!swapped) { sorted = true;  }
            }

            return array;
        }

        static int[] Merge(int[] array)
        {
            int length = array.Length;
            int[] temp = new int[length];

            for(int i = 0; i < length; i++){
                temp[i] = array[i];
            }
            return split(array, 0, length, temp);

            static int[] split(int[] B, int begin, int end, int[] A)
            {
                
                if(end - begin > 1) {
                    int middle = (begin + end) / 2;
                    
                    //sort left
                    split(A, begin, middle, B);
                    //sort right 
                    split(A, middle, end, B);

                    //merge
                    int i = begin;
                    int j = middle;

                    for (int k = begin; k < end; k++) {
                        if (i < middle && (j >= end || A[i] <= A[j])) {
                            B[k] = A[i];
                            i++;
                        } else {
                            B[k] = A[j];
                            j++;
                        }
                    }
                }
                
                return B;
            }
        }
    }
}
