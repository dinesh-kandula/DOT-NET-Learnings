using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsAndDS
{
    internal class StackTest
    {
        public string ReverseParentheses(string s)
        {
            // Convert the input string to a character array
            char[] arr = s.ToCharArray();
            // Stack to store the positions of opening parentheses
            Stack<int> stack = new Stack<int>();

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == '(')
                {
                    // Push the position of '(' onto the stack
                    stack.Push(i);
                }
                else if (arr[i] == ')')
                {
                    // Pop the position of the last '('
                    int j = stack.Pop();
                    // Reverse the substring between the positions j and i
                    Reverse(arr, j + 1, i - 1);
                }
            }

            // Build the final result string, excluding parentheses
            StringBuilder result = new StringBuilder();
            foreach (char ch in arr)
            {
                if (ch != '(' && ch != ')')
                {
                    result.Append(ch);
                }
            }

            return result.ToString();
        }

        private void Reverse(char[] arr, int left, int right)
        {
            while (left < right)
            {
                char temp = arr[left];
                arr[left] = arr[right];
                arr[right] = temp;
                left++;
                right--;
            }
        }








        /// <summary>
        /// First In Last Out
        /// Last In First Out
        /// </summary>
        public void StackLearning()
        {
            Stack<int> stack = new Stack<int>();

            Console.WriteLine("After creating the new Stack");
            Console.WriteLine(string.Join(",", stack));

            // Add Value to Stack 
            Console.WriteLine("--------");
            stack.Push(10);
            stack.Push(20);
            stack.Push(30);
            Console.WriteLine("After adding 3 values using stack.Push() - 10,20,30");
            Console.WriteLine(string.Join(",", stack));

            // Access the last value
            Console.WriteLine("--------");
            Console.WriteLine("Accessing value using stack.Peek()");
            Console.WriteLine(stack.Peek());

            // remove
            Console.WriteLine("--------");
            Console.WriteLine("Remove the value which is on top/last inserted");
            stack.Pop(); // Directly mentioning value
            Console.WriteLine(string.Join(",", stack));

            //Count
            Console.WriteLine("--------");
            Console.WriteLine("Get the length of stack");
            Console.WriteLine(stack.Count());
            Console.WriteLine(string.Join(",", stack));

            // Contains
            Console.WriteLine("Validating by directly mentionng value using stack.Contains(30);");
            Console.WriteLine(stack.Contains(30)); // Mentioning direct value
        }


        /// <summary>
        /// Addition using Recursion
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int stackSum(Stack<int> s)
        {
            if (s.Count == 0) return 0;

            return (s.Pop() + stackSum(s));
        }
    }
}
