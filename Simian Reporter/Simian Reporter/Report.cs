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
                public Block(XmlNode node)
                {
                    SourceFile = node.Attributes["sourceFile"].Value;
                    StartLine = node.AttributeAsInt("startLineNumber");
                    EndLine = node.AttributeAsInt("endLineNumber");
                }
            }
            public int LineCount { get; set; }
            public Block[] Blocks { get; set; }
            public Set(XmlNode node)
            {
                LineCount = int.Parse(node.Attributes["lineCount"].Value);
                List<Block> blocks = new List<Block>();
                foreach (XmlNode child in node.ChildNodes)
                {
                    blocks.Add(new Block(child));
                }
                Blocks = blocks.ToArray();
            }
        }
        public class Summary
        {
            public int DuplicateFileCount{get;set;}
            public int DuplicateLineCount{get;set;}
            public int DuplicateBlockCount{get;set;}
            public int TotalFileCount{get;set;}
            public int TotalRawLineCount{get;set;}
            public int TotalSignificantLineCount{get;set;}
            public int ProcessingTime{get;set;}
            public Summary(XmlNode node)
            {
                DuplicateFileCount = node.AttributeAsInt("duplicateFileCount");
                DuplicateLineCount = node.AttributeAsInt("duplicateLineCount");
                DuplicateBlockCount = node.AttributeAsInt("duplicateBlockCount");
                TotalFileCount = node.AttributeAsInt("totalFileCount");
                TotalRawLineCount = node.AttributeAsInt("totalRawLineCount");
                TotalSignificantLineCount = node.AttributeAsInt("totalSignificantLineCount");
                ProcessingTime = node.AttributeAsInt("processingTime");
            }

        }
        public class Rules
        {

        }
        public Set[] Sets { get; set; }
        public Summary summary { get; set; }
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

            List<Set> sets = new List<Set>();
            Summary summary = null;
            foreach (XmlNode node in document.FirstElementChild().FirstChild.ChildNodes)
            {
                switch (node.Name)
                {
                    case "set":
                        sets.Add(new Set(node));
                        break;
                    case "summary":
                        summary = new Summary(node);
                        break;
                    default:
                        break;
                }
                Console.WriteLine(node.Name);
            }
            return new Report() { Sets = sets.ToArray(), summary = summary };
        }
    }
}
