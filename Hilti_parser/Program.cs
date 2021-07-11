using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;



namespace Hilti_parser
{
    class Program
    {
        static void Main(string[] args)
        {
            /*csvArticleGrubber csvGrub = new csvArticleGrubber();
            csvGrub.fOpen();

            foreach(string element in csvGrub.resultsList)
            {
                Console.WriteLine(element);
            }
            Console.WriteLine(csvGrub.resultsList.Count);
            htmlLinkLister linkLister = new htmlLinkLister(csvGrub.resultsList);
            List<Array> links = linkLister.linkList();
            Console.WriteLine(links.Count);*/
            Console.WriteLine(@"Выберите действие:");
            Console.WriteLine("    1 Создать список ссылок (нужно иметь файл с артикулами в формате CSV в папке программы;");
            Console.WriteLine("    2 Получить цены (нужно иметь файл с ссылками (из п. 1 или без него) в папке программы;");
            Console.WriteLine("Введите номер действия:");
            string action = Console.ReadLine();
            switch(action)
            {
                case "1":
                    getLinks();
                    break;
                case "2":
                    priceGrubber prices = new priceGrubber(readLinksCsv());
                    prices.grubPrices();
                    break;
            }

            
                Console.ReadKey();
        }
        static List<Array> readLinksCsv()
        {
            List<Array> links = new List<Array>();
            string line;
            using (StreamReader sr = new StreamReader(@"linksLog.csv"))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    Array lineArray = line.Split(';');
                    links.Add(lineArray);
                }
            }
            return links;
        }
        static void getLinks()
        {
            csvArticleGrubber csvGrub = new csvArticleGrubber();
            csvGrub.fOpen();
            htmlLinkLister linkLister = new htmlLinkLister(csvGrub.resultsList);
            List<Array> links = linkLister.linkList();
        }
    }   
}
