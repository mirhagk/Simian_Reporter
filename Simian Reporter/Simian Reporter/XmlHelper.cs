using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Simian_Reporter
{
    public static class XmlHelper
    {
        public static XmlNode FirstElementChild(this XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.NodeType == XmlNodeType.Element)
                    return child;
            }
            return null;
        }
    }
}
