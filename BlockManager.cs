using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class BlockManager
    {
        public class BlockMap 
        {
            [XmlElement("Row")]
            public List<string> Row;
            public Vector2 BlockSpacing;
        }
        [XmlElement("BlockMap")]
        public BlockMap BlockMapped;
        [XmlIgnore]
        public List<Block> Blocks;
        private ContentManager _content;
        private XmlManager<BlockMap> xmlBlocks;
        private int _bgWidth, _bgHeight;
        private Level _level;


        public void Initialize(ContentManager content, int bgWidth, int bgHeight, Level level)
        {
            _content = content;
            _bgHeight = bgHeight;
            _bgWidth = bgWidth;
            _level = level;

            Blocks = new List<Block>();
            xmlBlocks = new XmlManager<BlockMap>();

            BlockMapped = xmlBlocks.Load("Load/BlockMap/BlockLevel"+ level.Value + ".xml",BlockMapped);

            int rownumber = 0;
            foreach (string row in BlockMapped.Row) 
            {
                rownumber+= (int)BlockMapped.BlockSpacing.Y;

                string[] ss = row.Split(']');
                int columnnumber = 0;

                foreach (string chr in ss) 
                {
                    columnnumber+= (int)BlockMapped.BlockSpacing.X;

                    string chrCopy = chr;

                    if (chrCopy != string.Empty)
                        chrCopy = chrCopy.TrimStart('[');

                    int value;

                    if (chrCopy == string.Empty)
                        value = 0;
                    else
                        value = Convert.ToInt16(chrCopy);

                    if (value != 0) 
                    {
                        Block blok = new Block();

                        blok.Initialize(content, "Blocks/BlockLevel" + value, new Vector2(columnnumber, rownumber));

                        Blocks.Add(blok);
                    }
                    
                }

            }
        }

        public void UnloadContent() 
        {
            this.Blocks.Clear();
        }

        public void Update(GameTime gameTime, Ball bal) 
        {
            foreach (Block blok in Blocks.ToList()) 
            {
                if (blok.CheckCollision(bal))
                    Blocks.Remove(blok);                
            }
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            foreach(Block blok in Blocks)
            {
                blok.Draw(spriteBatch);
            }
        }
    }
}
