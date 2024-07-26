using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsAndDS
{
    internal class FileIOLearning
    {
        public void FileInputOutputLearning()
        {
            string filePath = "c:\\Users\\dinesh.kandula\\Downloads\\MyFile.txt";


            using (StreamWriter streamWriter = new StreamWriter(filePath, true))
            {

                streamWriter.WriteLine("20 + 30 != 999");

            };

            using (StreamReader streamReader = new StreamReader(filePath, true))
            {
                streamReader.BaseStream.Seek(0, SeekOrigin.Current);

                string strData = streamReader.ReadToEnd();
                // To Read the whole file line by line use While Loop as long the strData is not null
                //while (strData != null)
                //{
                //Print the String data
                Console.WriteLine(strData);
                //Then Read the next String data
                //    strData = streamReader.ReadLine();
                //}
            }

            FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            byte[] bytedata = Encoding.Default.GetBytes("C# Is an Object Oriented Programming Language");
            fileStream.Write(bytedata, 0, bytedata.Length);
            fileStream.Close();


            FileStream fileStreamReader = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
            using (StreamReader reader = new StreamReader(fileStreamReader))
            {
                var data = reader.ReadToEnd();
                Console.WriteLine(data);
            }
        }
    }
}
