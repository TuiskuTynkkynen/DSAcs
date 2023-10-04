using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace CS50
{
    internal class Datatypes
    {
        //TODO datatypes: add linked list methods(binary search?), binary tree, hash map
        public static void DTMain(){
            LinkedList<int> list = new();
            
            while (true)
            {
                Console.WriteLine("Select function: Add, Remove, Count, Contains, Index, Value, Print, Free");
                string input = Console.ReadLine();
                int num;

                switch (input)
                {
                    case "Add":
                        Console.WriteLine("Int to be added:");
                        num = int.Parse(Console.ReadLine());
                        list.Add(num);
                        break;
                    case "Remove":
                        Console.WriteLine("Int to be removed:");
                        num = int.Parse(Console.ReadLine());
                        list.Remove(num);
                        break;
                    case "Count":
                        Console.WriteLine("Count = " + list.Count);
                        break;
                    case "Contains":
                        Console.WriteLine("Int to be checked:");
                        num = int.Parse(Console.ReadLine());
                        Console.WriteLine(list.Contains(num));
                        break;
                    case "Index":
                        Console.WriteLine("Int to get index of:");
                        num = int.Parse(Console.ReadLine());
                        Console.WriteLine("Index = " + list.Index(num));
                        break;
                    case "Value":
                        Console.WriteLine("Index to get value of:");
                        num = int.Parse(Console.ReadLine());
                        Console.WriteLine("Value = " + list.Value(num));
                        break;
                    case "Print":
                        list.Print();
                        break;
                    case "Free":
                        list.Free();
                        break;
                    case "Exit":
                        list.Free();
                        return;
                        break;
                    default:
                        Console.WriteLine("Incorrect Input");
                        break;
                }
                Console.WriteLine();
            }
        }
        
        unsafe struct LinkedList<T>
        {
            public Node<T>* List { get; set; }
            public GCHandle GCH { get; set; }

                /*Node* prev = List; 
                for (int i = 1; i < num; i++){
                    Node next = new(value);
                    prev->GCH = GCHandle.Alloc(next, GCHandleType.Pinned);
                    prev->Next = (Node*)prev->GCH.AddrOfPinnedObject();
                    prev = prev->Next;
                }
                */
            

            public readonly int Count{
                get{
                    int x = 1;
                    Node<T>* node = this.List;
                    if (node == null) { return 0; }
                    while (node->Next != null)
                    {
                        node = node->Next;
                        x++;
                    }

                    return x;
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
                } else if (index != null) {
                    Node<T>* node = this.List;
                    for (int i = 0; i < index-1; i++)
                    node->GCH.Free();
                    node->GCH = node->Next->GCH;
                    node->Next = node->Next->Next;
                }
            }

            public readonly bool Contains(T val)
            {
                Node<T>* node = this.List;
                if (node == null) { return false; }

                while (node->Next != null)
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
                if (node == null) { return null; }
                int x = 0;

                while (node->Next != null)
                {
                    if (node->Value.Equals(val))
                    {
                        return x;
                    }
                    node = node->Next;
                    x++;
                }
                return null;
            }

            public readonly void Print()
            {
                Node<T>* node = this.List;
                if (node == null) {  Console.WriteLine("List is empty"); return; }
                Console.Write("Linked list: ");
                while (node->Next != null) {
                    Console.Write(node->Value + ", ");
                    node = node->Next;
                }
                Console.Write(node->Value);
                Console.WriteLine();
            }


            public readonly void Free() {
                Node<T>* node = this.List;
                if (node == null) { return; }
                this.GCH.Free();
                while (node->Next != null) {
                    Node<T>* tmp = node->Next;
                    node->GCH.Free();
                    node->Next = null;
                    node = tmp;
                }
                GC.Collect();
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
