using System;
using Microsoft.Office.Interop.Excel;

namespace Hilti_parser
{
    class Program
    {
        static void Main(string[] args)
        {
            excelArticleGrubber excelGrub = new excelArticleGrubber();
            Console.ReadKey();
        }
    }
}
