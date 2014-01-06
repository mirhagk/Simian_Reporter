using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simian_Reporter
{
    class LatexFormatter
    {
        public Dictionary<string, string> replacementStrings = new Dictionary<string, string>()
        {
            {@">", @"\textrangle "},
            {@"<", @"\textlangle "},
            {@"\", @"\textbackslash "},
            {@"_", @"\_"}
        };
        public static string GenerateReport(Report report)
        {
        }
    }
}
