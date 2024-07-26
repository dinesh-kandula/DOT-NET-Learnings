using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsAndDS
{
    internal class ObservableCollectionTest
    {

        public void ObservableCollectionLearning() {
            
            ObservableCollection<string> collection = new ObservableCollection<string>();
            collection.Add("Item 1");
            collection.Add("Item 2");
            collection.Add("Item 3");
            collection.Add("Item 4");
            collection.Add("Item 5");

            Console.WriteLine("After defining and adding items using collection.Add('Item 1')");
            Console.WriteLine(string.Join(",", collection));

            /*
                 ObservableCollection<string> collection2 = ["Item 1", "Item 2"];             
             */

            Console.WriteLine("Used collection.Insert(2, 'Item 2.5')");
            collection.Insert(2, "Item 2.5");
            Console.WriteLine(string.Join(",", collection));


            Console.WriteLine("Used collection.RemoveAt(2)");
            collection.RemoveAt(2);
            Console.WriteLine(string.Join(",", collection));

            Console.WriteLine("Used collection.Remove('Item 1')");
            collection.Remove("Item 1");
            Console.WriteLine(string.Join(",", collection));

            Console.WriteLine("Used collection.Move(0,1)");
            collection.Move(0, 1);
            Console.WriteLine(string.Join(",", collection));

            Console.WriteLine("collection.Clear()");
            collection.Clear();
            Console.WriteLine(string.Join(",", collection));
        }
    }
}
