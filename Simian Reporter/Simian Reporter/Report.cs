using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Simian_Reporter
{
    class Report
    {
        public class Set
        {
            public class Block
            {
                public string SourceFile { get; set; }
                public int StartLine { get; set; }
                public int EndLine { get; set; }
            }
        }
        public class Summary
        {

        }
        public class Rules
        {

        }
        private static string CleanXML(string xml)
        {
            int index = xml.IndexOf("<?xml");
            return xml.Substring(index);
        }
        public static Report LoadFromXML(string xml)
        {
            xml = CleanXML(xml);
            XmlDocument document = new XmlDocument();
            document.LoadXml(xml);

            foreach (XmlNode node in document.FirstElementChild().FirstChild.ChildNodes)
            {
                Console.WriteLine(node.Name);
            }
            return new Report();
        }
    }
}
