using System.Runtime.InteropServices;

namespace DSA
{
    internal class Datatypes
    {
        public static void DTMain(){
            Console.WriteLine("Create linked list of type: Short, Int, Long, Float or Exit");
            
            while (true)
            {
                string? input = Console.ReadLine();
                switch(input)
                {
                    case "Short":
                        LinkedList<short> shortList = new();
                        FunctionSelector<short>(ref shortList);
                        return;
                    case "Int":
                        LinkedList<int> intList = new();
                        FunctionSelector<int>(ref intList);
                        return;
                    case "Long":
                        LinkedList<long> longList = new();
                        FunctionSelector<long>(ref longList);
                        return;
                    case "Float":
                        LinkedList<float> floatList = new();
                        FunctionSelector<float>(ref floatList);
                        return;
                    case "Exit":
                        return;
                    default:
                        Console.WriteLine("Incorrect Input");
                        break;
                }
            }
        }
        
        private static void FunctionSelector<T>(ref LinkedList<T> list) where T : struct
        {
            while (true)
            {
                Console.WriteLine("Select function: Add, Remove, Count, Contains, Index, Value, Print, Free, Exit");
                string? input = Console.ReadLine();
                T? value;

                switch (input)
                {
                    case "Add":
                        Console.WriteLine("Value to be added:");
                        value = Parse<T>(Console.ReadLine());
                        if (value == null)
                        {
                            Console.WriteLine("Value was not in correct format");
                            break;
                        }
                        list.Add((T)value);
                        break;
                    case "Remove":
                        Console.WriteLine("Value to be removed:");
                        value = Parse<T>(Console.ReadLine());
                        if (value == null)
                        {
                            Console.WriteLine("Value was not in correct format");
                            break;
                        }
                        list.Remove((T)value);
                        break;
                    case "Count":
                        Console.WriteLine("Count = " + list.Count);
                        break;
                    case "Contains":
                        Console.WriteLine("Value to be checked:");
                        value = Parse<T>(Console.ReadLine());
                        if (value == null)
                        {
                            Console.WriteLine("Value was not in correct format");
                            break;
                        }
                        Console.WriteLine(list.Contains((T)value));
                        break;
                    case "Index":
                        Console.WriteLine("Value to get index of:");
                        value = Parse<T>(Console.ReadLine());
                        if (value == null)
                        {
                            Console.WriteLine("Value was not in correct format");
                            break;
                        }
                        Console.WriteLine("Index = " + list.Index((T)value));
                        break;
                    case "Value":
                        Console.WriteLine("Index to get value of:");
                        int index = int.Parse(Console.ReadLine());
                        Console.WriteLine("Value = " + list.Value(index));
                        break;
                    case "Print":
                        list.Print();
                        break;
                    case "Free":
                        list.Free();
                        break;
                    case "Exit":
                        if(list.GCH.IsAllocated)
                        {
                            list.Free();
                        }
                        return;
                    default:
                        Console.WriteLine("Incorrect Input");
                        break;
                }
                Console.WriteLine();
            }
        }

        private static T? Parse<T>(string input) where T : struct
        {
            try
            {
                return (T)Convert.ChangeType(input, typeof(T));
            }
            catch
            {
                return default;
            }
        }
        
        unsafe struct LinkedList<T>
        {
            public Node<T>* List { get; set; }
            public GCHandle GCH { get; set; }

            public readonly int Count{
                get{
                    int count = 1;
                    Node<T>* node = this.List;
                    if (node == null) { return 0; }
                    while (node->Next != null)
                    {
                        node = node->Next;
                        count++;
                    }

                    return count;
                }
            }
            public void Add(T val){
                Node<T>* node = this.List;
                Node<T> newnode = new(val);
                
                if (node == null) { 
                    this.GCH = GCHandle.Alloc(newnode, GCHandleType.Pinned);
                    this.List = (Node<T>*)this.GCH.AddrOfPinnedObject();
                    return;
                }

                while (node->Next != null)
                {
                    node = node->Next;
                }

                node->GCH = GCHandle.Alloc(newnode, GCHandleType.Pinned);
                node->Next = (Node<T>*)node->GCH.AddrOfPinnedObject();
            }

            public void Remove(T val) {
                int? index = this.Index(val);
                if (index == 0) {
                    Node<T>* node = this.List;
                    this.GCH.Free();

                    this.List = node->Next;
                    this.GCH = node->GCH;
                }
                else if (index != null) 
                {
                    Node<T>* node = this.List;
                    for (int i = 0; i < index-1; i++)
                    {
                        node = node->Next;
                    }
                    node->GCH.Free();
                    node->GCH = node->Next->GCH;
                    node->Next = node->Next->Next;
                }
            }

            public readonly bool Contains(T val)
            {
                Node<T>* node = this.List;
                if (node == null) {
                    Console.WriteLine("List is empty");
                    return false; 
                }

                while (node != null)
                {
                    if (node->Value.Equals(val))
                    {
                        return true;
                    }
                    node = node->Next;
                }
                return false;
            }

            public readonly T Value(int index){
                Node<T>* node = this.List;
                int i = 0; 
                while(i < index && node->Next != null){
                    node = node->Next;
                    i++;
                }
                return node->Value;
            }

            public readonly int? Index(T val)
            {
                Node<T>* node = this.List;
                if (node == null) {
                    Console.WriteLine("List is empty");
                    return null; 
                }

                int index = 0;

                while (node != null)
                {
                    if (node->Value.Equals(val))
                    {
                        return index;
                    }
                    node = node->Next;
                    index++;
                }
                return null;
            }

            public readonly void Print()
            {
                Node<T>* node = this.List;
                if (node == null) {  Console.WriteLine("List is empty"); return; }
                
                Console.Write("Linked list: ");
                while (node->Next != null) {
                    Console.Write(node->Value + " ");
                    node = node->Next;
                }
                Console.Write(node->Value);
                Console.WriteLine();
            }


            public void Free() {
                Node<T>* node = this.List;
                this.List = null;
                if (node == null) { return; }
                
                this.GCH.Free();
                
                while (node != null) {
                    Node<T>* tmp = node->Next;
                    if (node->GCH.IsAllocated)
                    {
                        node->GCH.Free();
                    }
                    node->Next = null;
                    node = tmp;
                }
            }

            public struct Node<U>{
                public U Value { get;}
                public GCHandle GCH { get; set; }
                public Node<U>* Next { get; set; }
                public Node(U val){
                    Value = val;
                    Next = null;
                    GCH = new();
                }

            }
        }

    

    }
}
