using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsAndDS
{
    internal class CollectonsGenNonGen
    {

        public void CollectionsGenNonGenLearning()
        {

            // Generc Collections by using System.Generic.Collections;
            Dictionary<int, string> dic = new Dictionary<int, string>();
            List<string> list = new List<string>();
            Queue<int> qInts = new Queue<int>();
            SortedList<int, string> keyValuePairs = new SortedList<int, string>();
            Stack<int> stack = new Stack<int>();
            HashSet<int> visited = new HashSet<int>();
            LinkedList<int> ints = new LinkedList<int>();


            // Non Generic Collections by using System.Collections;
            ArrayList arrayList = new ArrayList();
            Hashtable hashtable = new Hashtable();
            Queue queue = new Queue();
            Stack stack1 = new Stack();
        }
    }
}
