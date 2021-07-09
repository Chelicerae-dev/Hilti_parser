using System;
using System.Collections.Generic;
using System.Collections;



namespace Hilti_parser
{
    class Program
    {
        static void Main(string[] args)
        {
            csvArticleGrubber csvGrub = new csvArticleGrubber();
            csvGrub.fOpen();

            /*foreach(string element in csvGrub.resultsList)
            {
                Console.WriteLine(element);
            }*/
            Console.WriteLine(csvGrub.resultsList.Count);
            htmlLinkLister linkLister = new htmlLinkLister(csvGrub.resultsList);
            List<Array> links = linkLister.linkList();
            Console.WriteLine(links.Count);
            Console.ReadKey();
        }
    }   
}
