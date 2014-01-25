using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simian_Reporter
{
    class Program
    {
        static void Main(string[] args)
        {
            var report = Report.LoadFromXML(System.IO.File.ReadAllText("test.xml"));
            System.IO.File.WriteAllText("test.tex", LatexFormatter.GenerateReport(report));
            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
