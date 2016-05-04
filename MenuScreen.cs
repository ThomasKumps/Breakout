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
    public class MenuScreen
    {
        
        public Menu Menu;
        public XmlManager<Menu> xmlMenu;
        private int _width, _height, selected = 0;
        private SpriteFont font;
        private float keyHit = 0f;
        private ContentManager contentSide;
        public MenuScreen(int width, int height) 
        {
            this._height = height;
            this._width = width;
        }

        public void LoadContent(ContentManager content, string path) 
        {
            this.contentSide = content;

            xmlMenu = new XmlManager<Breakout.Menu>();
            Menu = xmlMenu.Load(path, Menu);
            font = content.Load<SpriteFont>("Buxton SketchSmaller");

            foreach (MenuItem item in Menu.MenuItems) 
            {
                if (item.TexturePath != string.Empty) 
                {
                    item.Texture = content.Load<Texture2D>("Background/" + item.TexturePath);

                    if (item.Type == "SceneGame") 
                    {
                        item.PBG1 = new ParallaxingBackground();
                        item.PBG2 = new ParallaxingBackground();

                        item.PBG1.Initialize(content, "ParallaxBackground/" + item.TexturePath + "BgLayer1", _width, _height, 1);
                        item.PBG2.Initialize(content, "ParallaxBackground/" + item.TexturePath + "BgLayer2", _width, _height, 2);
                    }
                }
                    
            }
        }


        public void Update(GameTime gameTime, AppState appState) 
        {
            keyHit += (float)gameTime.ElapsedGameTime.TotalSeconds;
            

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && keyHit > 0.5) 
            {
                if (appState.menuState.ItemSelected > 0)
                    appState.menuState.ItemSelected--;
                keyHit = 0f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && keyHit > 0.5) 
            {
                if (appState.menuState.ItemSelected < Menu.MenuItems.Count - 1)
                    appState.menuState.ItemSelected++;
                keyHit = 0f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && keyHit > 0.5) 
            {
                keyHit = 0f;
                switch (Menu.MenuItems[appState.menuState.ItemSelected].Type)
                {
                    case "StartGame":
                        appState.gameState.Level = appState.menuState.ItemSelected;
                        appState.gameState.GamePaused = false;
                        appState.menuState.NewGame = true;
                        appState.gameState.SceneGame = false;
                        break;
                    case "SceneGame":
                        appState.gameState.Level = appState.menuState.ItemSelected;
                        appState.gameState.GamePaused = false;
                        appState.menuState.NewGame = true;
                        appState.gameState.SceneGame = true;
                        break;
                    case "Menu":
                        this.LoadContent(contentSide, "Load/Menu/" + Menu.MenuItems[appState.menuState.ItemSelected].LinkID + ".xml");
                        break;
                    case "credits":
                        break;
                }              
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Back) && keyHit > 0.5) 
            {
                this.LoadContent(contentSide, "Load/Menu/MainMenu.xml");
                keyHit = 0f;
                appState.menuState.ItemSelected= 0;
            }


            if (Menu.MenuItems[appState.menuState.ItemSelected].Type == "SceneGame" && Menu.MenuItems[appState.menuState.ItemSelected].PBG1 != null) 
            {
                Menu.MenuItems[appState.menuState.ItemSelected].PBG1.Update(gameTime, 1);
                Menu.MenuItems[appState.menuState.ItemSelected].PBG2.Update(gameTime, 2);
            }

            selected = appState.menuState.ItemSelected;
                
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            int space = 0;

            if (Menu.MenuItems[selected].Texture != null) 
            {
                spriteBatch.Draw(Menu.MenuItems[selected].Texture, new Rectangle(0, 0, Menu.MenuItems[selected].Texture.Width, Menu.MenuItems[selected].Texture.Height), Color.White);

                if (Menu.MenuItems[selected].PBG1 != null) 
                {
                    Menu.MenuItems[selected].PBG1.Draw(spriteBatch);
                    Menu.MenuItems[selected].PBG2.Draw(spriteBatch);
                }
                    

            }
                

            foreach(MenuItem item in Menu.MenuItems)
            {
                if(item != Menu.MenuItems[selected])
                    spriteBatch.DrawString(font, item.Name, new Vector2(_width / 3 + _width / 10, _height / 4 + space), Color.Blue); 
                else
                    spriteBatch.DrawString(font, item.Name, new Vector2(_width / 3 + _width / 10, _height / 4 + space), Color.Red);
                space += Menu.Spacing;
            }
        }
    }
}
