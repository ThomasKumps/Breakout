using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace Breakout
{
    [XmlRoot("Menu")]
    public class Menu
    {   [XmlElement("MenuItems")]
        public List<MenuItem> MenuItems;
        [XmlElement("Spacing")]
        public int Spacing;
    }
}
