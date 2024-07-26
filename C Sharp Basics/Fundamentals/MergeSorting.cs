using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamentals
{
    internal class MergeSorting
    {
        public static void MergeSort(int[] array)
        {
            if (array.Length <= 1)
                return;

            int midPoint = array.Length / 2;

            int[] left = new int[midPoint];
            int[] right = new int[array.Length - midPoint];

            Array.Copy(array, 0, left, 0, midPoint);
            Array.Copy(array, midPoint, right, 0, array.Length - midPoint);

            MergeSort(left);
            MergeSort(right);

            Merge(array, left, right);
        }

        public static void Merge(int[] array, int[] left, int[] right)
        {
            int i = 0, j = 0, k = 0;

            while (i < left.Length && j < right.Length)
            {
                if (left[i] <= right[j])
                {
                    array[k++] = left[i++];
                }
                else
                {
                    array[k++] = right[j++];
                }
            }

            while (i < left.Length)
            {
                array[k++] = left[i++];
            }

            while (j < right.Length)
            {
                array[k++] = right[j++];
            }
        }
    }
}
