namespace CollectionsAndDS
{
    public static class MyExtensions
    {
        public static int WordCount(this string str)
        {
            return str.Split(' ').Length;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            String str = "I am Dinesh Kumar, I am working as developer in TechVedika";
            Console.WriteLine(str.WordCount());
        }
    }
}



//// LINQ Query Syntax
#region Count and convert To Dictionary
//int[] nums = [5, 1, 1, 2, 2, 2, 3, 4, 4, 4];

//var dicCount = nums.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count())
//   .OrderBy(x => x.Value).ThenByDescending(x => x.Key);

//foreach (var d in dicCount)
//{
//    Console.WriteLine($"{d.Key} = {d.Value}");
//}

#endregion