using System;
using System.IO;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Web;
using System.Threading.Tasks;

namespace Hilti_parser
{
    public class htmlLinkLister
    {
        List<string> articles = new List<string>();

        public List<Array> linkList()
        {
            List<Array> res = new List<Array>();
            int iterator = 0;
            foreach(string article in articles)
            {
                    iterator++;
                Console.Write($"{iterator}. {article} ");
                try
                {
                    var html = $@"https://www.hilti.ru/search?text={article}";
                    HtmlWeb web = new HtmlWeb();
                    var htmlDoc = web.Load(html);
                    //var linkNode = htmlDoc.DocumentNode.SelectSingleNode("//a/@data-parent-link-ref");
                    string link = htmlDoc.DocumentNode.SelectSingleNode("//a/@data-parent-link-ref").GetAttributeValue("href", "Failed to get a link!");
                    Console.Write($"has link {link}\n");
                    string[] arr = { article, link };
                    res.Add(arr);
                }
                catch
                {
                    Console.Write("failed!\n");
                    string[] arr = { article, "Failed!" };
                    res.Add(arr);
                } 
            }
            using (StreamWriter writer = new StreamWriter(@"linksLog.csv"))
            {   
                foreach(Array line in res)
                {
                    writer.WriteLine($"{line.GetValue(0)};{line.GetValue(1)}");
                }
                
            }
            return res;
        }



        public htmlLinkLister(List<string> list)
        {
            articles = list;
        }
    }
}
