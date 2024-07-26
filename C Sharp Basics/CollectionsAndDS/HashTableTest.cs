using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsAndDS
{
    internal class HashTableTest
    {
        public void HashTableLearning()
        {
            Hashtable hashtable = new Hashtable();
        
            Console.WriteLine("After creating the new HashTable");
            Console.WriteLine(string.Join(",", hashtable));

            // Add Value to Stack 
            Console.WriteLine("--------");
            hashtable.Add("Ten", 10);
            hashtable.Add(20, "Twenty");
            hashtable.Add("Thirty", 30);
            Console.WriteLine("After adding 3 values using hashTable.Add() - ['Ten', 10],[20, 'Twenty'],['Thirty',30]");
            Console.WriteLine(string.Join(",", hashtable));

            //Iterate over
            foreach (DictionaryEntry entry in hashtable)
            {
                Console.WriteLine($"{entry.Key} : {entry.Value}");
            }


            // Access the last value
            Console.WriteLine("--------");
            Console.WriteLine("Accessing value using hashTable.");
            Console.WriteLine(hashtable.Keys);

            // remove
            Console.WriteLine("--------");
            Console.WriteLine("Remove the value which is on top/last inserted");
            hashtable.Remove(20); // Directly mentioning value
            Console.WriteLine(string.Join(",", hashtable));

            //Count
            Console.WriteLine("--------");
            Console.WriteLine("Get the length of stack");
            Console.WriteLine(hashtable.Count);
            Console.WriteLine(string.Join(",", hashtable));

            // Contains
            Console.WriteLine("Validating by directly mentionng value using stack.Contains(30);");
            Console.WriteLine(hashtable.Contains(30)); // Mentioning direct value
        }
    }
}
