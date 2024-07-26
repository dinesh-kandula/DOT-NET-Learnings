using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Xml.Serialization;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Collections;
using System.Collections.ObjectModel;

namespace Fundamentals
{
    public class Program
    {
        public static void Main(string[] args)
        {

            ParitialClassTest paritialClassTest  = new ParitialClassTest();
            paritialClassTest.GetEmployeeDetails();
     
            
            // Using LinkedList class
            LinkedList<String> my_list = new LinkedList<String>();

            /*
             ******* Adding
             * list.AddLast("");
             * list.AddFirst("");
             * 
             * ***** Accessing
             * list.First();
             * list.Last();
             * 
             * list.Remove("");
             * list.Remove(list.First());
             * list.Remove(list.Last());
             * list.RemoveFirst();
             * list.RemoveLast();
             * list.Clear();

             */

            // Adding elements in the LinkedList
            // Using AddLast() method
            my_list.AddLast("Zoya");
            my_list.AddLast("Shilpa");
            my_list.AddLast("Rohit");
            my_list.AddLast("Rohan");
            my_list.AddLast("Juhi");
            my_list.AddLast("Zoya");
           


            // Initial number of elements
            Console.WriteLine("Best students of XYZ " +
                            "university initially:");

            // Accessing the elements of 
            // Linkedlist Using foreach loop
            foreach (string str in my_list)
            {
                Console.WriteLine(str);
            }

            // After using Remove(LinkedListNode)
            // method
            Console.WriteLine("my_list.Remove(my_list.First)");


            my_list.Remove(my_list.First());

            foreach (string str in my_list)
            {
                Console.WriteLine(str);
            }

            // After using Remove(T) method
            Console.WriteLine("my_list.Remove('Rohit');");

            my_list.Remove("Rohit");

            foreach (string str in my_list)
            {
                Console.WriteLine(str);
            }

            // After using RemoveFirst() method
            Console.WriteLine("my_list.RemoveFirst()");

            my_list.RemoveFirst();

            foreach (string str in my_list)
            {
                Console.WriteLine(str);
            }

            // After using RemoveLast() method
            Console.WriteLine("my_list.RemoveLast();");

            my_list.RemoveLast();

            foreach (string str in my_list)
            {
                Console.WriteLine(str);
            }

            // After using Clear() method
            Console.WriteLine("my_list.Clear()");
            my_list.Clear();
            Console.WriteLine("Number of students: {0}",
                                        my_list.Count);


            Stack<string> st = new();
            st.Push("Hello");
            st.Push("Bunny");
            st.Push("Good Morning!");

            Queue<string> qu = new();
            qu.Enqueue("Hello");
            qu.Enqueue("Bunny");
            qu.Enqueue("Good Morning!");
            qu.Dequeue();

            Console.WriteLine(string.Join(",", st));
            Console.WriteLine(string.Join(",", qu));


            int[] nums;
            nums = new int[10];

            int[] num;
            num = new int[] { 2, 3, 4, 5, 6 };

            int[] n;
            n = [2, 3, 4, 5, 6];

            string[] w;
            w = [ "ss", "sg", "agagd", "agsd"];

            int len = num.Length;

            // Creates and initializes a new integer array and a new Object array.
            int[] myIntArray = new int[5] { 1, 2, 3, 4, 5 };
            Object[] myObjArray = new Object[5] { 26, 27, 28, 29, 30 };

            ArrayList arrayList = new ArrayList();
            arrayList.Add("hello");
            arrayList.Add(1);
            Console.WriteLine(arrayList[1]);

            // Prints the initial values of both arrays.
            Console.WriteLine("Initially,");
            Console.Write("integer array:");
            Console.WriteLine(string.Join(",", myIntArray));
            Console.Write("Object array: ");
            Console.WriteLine(string.Join(",", myObjArray));

            // Copies the first two elements from the integer array to the Object array.
            Array.Copy(myIntArray, myObjArray, 2);

            // Prints the values of the modified arrays.
            Console.WriteLine("\nAfter copying the first two elements of the integer array to the Object array,");
            Console.Write("integer array:");
            Console.WriteLine(string.Join(",", myIntArray));
            Console.Write("Object array: ");
            Console.WriteLine(string.Join(",", myObjArray));

            int index = Array.BinarySearch(myObjArray, 29);
            Console.WriteLine(index);

            Predicate<int> isMatch = s => s == 3;
            bool exsits = Array.Exists(myIntArray, isMatch);
            Console.WriteLine(exsits);

            string[] words = ["hello", "WORLD", "example"];
            Predicate<string> isUpper = s => s.Equals(s.ToUpper());
            bool exists = Array.Exists(words, isUpper);
            Console.WriteLine(exists); // Output: true


            


            Console.WriteLine("Enter Input string");
            string s = Console.ReadLine()!;
            var dic = s.GroupBy(x => x).Select(s => new { s.Key, Value = s.Count() }).ToDictionary(s => s.Key, s => s.Value);
          
            Console.WriteLine(string.Join(",", dic));
            


            //string[] products = ["mobile", "mouse", "moneypot", "monitor", "mousepad"];
            //IList<string> matchList = products.Where(x => x.StartsWith("mo")).OrderBy(x => x).Take(3).ToList();





        }

       
    }

}
