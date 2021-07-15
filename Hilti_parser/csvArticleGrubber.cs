using System;
using System.IO;
using System.Collections.Generic;

namespace Hilti_parser
{
    public class csvArticleGrubber
    {
        //public string filePath = Path.GetFullPath($@"../../../../1.csv");
        public string filePath = Path.GetFullPath($@"1.csv");
        public List<string> resultsList = new List<string>();

        public void fOpen()
            {
            using (var reader = new StreamReader(filePath))
                {
                    string line;
                    while((line = reader.ReadLine()) != null)
                    {
                        resultsList.Add(line.Trim(';') );
                    }
                }
            }       
        

            public csvArticleGrubber()
            {
                Console.WriteLine(filePath);
                Console.ReadKey();
            }
        }

    public class articleData
    {
        public string Id { get; set; }
    }
}
