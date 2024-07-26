using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsAndDS
{
    internal class LinkedListTest
    {
        public void  LinkedListLearning()
        {
            LinkedList<int> linkedList = new LinkedList<int>();

            Console.WriteLine("After creating the new Linked List");
            Console.WriteLine(string.Join(",", linkedList));

            // Add Value to First
            Console.WriteLine("--------");
            linkedList.AddFirst(10);
            linkedList.AddFirst(20);
            linkedList.AddFirst(30);
            Console.WriteLine("After adding 3 values using linkedList.AddFirst() - 10,20,30");
            Console.WriteLine(string.Join(",", linkedList));

            //Add Value to Last
            Console.WriteLine("--------");
            linkedList.AddLast(1);
            linkedList.AddLast(2);
            linkedList.AddLast(3);
            Console.WriteLine("After 3 values using linkedlist.AddLast() - 1,2,3");
            Console.WriteLine(string.Join(",", linkedList));

            // Access the first value
            Console.WriteLine("--------");
            Console.WriteLine("Accessing value using linkedList.First()");
            Console.WriteLine(linkedList.First());
            Console.WriteLine("Accessing value using linkedList.Last()");
            Console.WriteLine(linkedList.Last());

            // remove
            Console.WriteLine("--------");
            Console.WriteLine("Remove the value by mentioning direct value using list.Remove(20)");
            linkedList.Remove(20); // Directly mentioning value
            Console.WriteLine(string.Join(",", linkedList));
            
            Console.WriteLine("Remove the value by using  linkedList.Remove(linkedList.First());");
            linkedList.Remove(linkedList.First()); //Removes First Value
            Console.WriteLine(string.Join(",", linkedList));

            Console.WriteLine("Remove the value by using  linkedList.Remove(linkedList.Last());");
            linkedList.Remove(linkedList.Last()); //Removes Last Value
            Console.WriteLine(string.Join(",", linkedList));

            Console.WriteLine("Remove the value by using linkedList.RemoveFirst();");
            linkedList.RemoveFirst(); // Remove First Value
            Console.WriteLine(string.Join(",", linkedList));

            Console.WriteLine("Remove the value by using linkedList.RemoveLast();");
            linkedList.RemoveLast(); // Remove Last Value
            Console.WriteLine(string.Join(",", linkedList));

            // Contains
            Console.WriteLine("Validating by directly mentionng value using linkedList.Contains(30);");
            Console.WriteLine(linkedList.Contains(30)); // Mentioning direct value

        }


    }
}
