using System;
using excel = Microsoft.Office.Interop.Excel;

namespace Hilti_parser
{
    public class excelArticleGrubber
    {
        excel.Application ex = new excel.Application();



        public excelArticleGrubber()
        {
            Console.WriteLine(ex);
        }
    }
}
