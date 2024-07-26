using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsAndDS
{
    internal class QueueTest
    {

        /// <summary>
        /// First In First OUT
        /// </summary>
        public void QueueLearning()
        {
            Queue<int> queue = new Queue<int>();

            Console.WriteLine("After creating the new Queue");
            Console.WriteLine(string.Join(",", queue));

            // Add Value to Stack 
            Console.WriteLine("--------");
            queue.Enqueue(10);
            queue.Enqueue(20);
            queue.Enqueue(30);
            Console.WriteLine("After adding 3 values using queue.Enqueue() - 10,20,30");
            Console.WriteLine(string.Join(",", queue));

            // Access the last value
            Console.WriteLine("--------");
            Console.WriteLine("Accessing value using stack.Peek() without removing");
            Console.WriteLine(queue.Peek());

            // remove
            Console.WriteLine("--------");
            Console.WriteLine("Remove the value which is on initially inserted");
            queue.Dequeue(); // Directly mentioning value
            Console.WriteLine(string.Join(",", queue));

            //Count
            Console.WriteLine("--------");
            Console.WriteLine("Get the length of stack");
            Console.WriteLine(queue.Count());
            Console.WriteLine(string.Join(",", queue));

            // Contains
            Console.WriteLine("Validating by directly mentionng value using stack.Contains(30);");
            Console.WriteLine(queue.Contains(30)); // Mentioning direct value
        }
    }
}
