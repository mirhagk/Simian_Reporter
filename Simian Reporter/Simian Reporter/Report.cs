using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
                public Block(XElement node)
                {
                    SourceFile = node.Attribute("sourceFile").Value;
                    StartLine = node.Attribute("startLineNumber").Value.AsInt();
                    EndLine = node.Attribute("endLineNumber").Value.AsInt();
                }
            }
            public int LineCount { get; set; }
            public Block[] Blocks { get; set; }
            public Set(XElement node)
            {
                LineCount = node.Attribute("lineCount").Value.AsInt();
                List<Block> blocks = new List<Block>();
                foreach (XElement child in node.Elements())
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
            public Summary(XElement node)
            {
                DuplicateFileCount = node.Attribute("duplicateFileCount").Value.AsInt();
                DuplicateLineCount = node.Attribute("duplicateLineCount").Value.AsInt();
                DuplicateBlockCount = node.Attribute("duplicateBlockCount").Value.AsInt();
                TotalFileCount = node.Attribute("totalFileCount").Value.AsInt();
                TotalRawLineCount = node.Attribute("totalRawLineCount").Value.AsInt();
                TotalSignificantLineCount = node.Attribute("totalSignificantLineCount").Value.AsInt();
                ProcessingTime = node.Attribute("processingTime").Value.AsInt();
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
            var document = XDocument.Parse(xml);

            List<Set> sets = new List<Set>();
            Summary summary = null;
            var group = document.Element("simian").Element("check");
            foreach (var node in group.Elements("sets"))
            {
                sets.Add(new Set(node));
            }
            foreach (var node in group.Elements("summary"))
            {
                summary = new Summary(node);
            }
            return new Report() { Sets = sets.ToArray(), summary = summary };
        }
    }
}
