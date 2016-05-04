using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework.Graphics;
namespace Breakout
{
    public class MenuItem
    {
        [XmlElement("Name")]
        public string Name;
        [XmlElement("LinkID")]
        public string LinkID;
        [XmlElement("Type")]
        public string Type;
        [XmlElement("TexturePath")]
        public string TexturePath;
        [XmlIgnore]
        public Texture2D Texture;
        [XmlIgnore]
        public ParallaxingBackground PBG1, PBG2;
    }
}
