using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noughts_And_Crosses
{
    sealed class Grid : GameObject
    {
        private Mark Mark;
        
        public Grid(LogicalPosition position) : base(position)
        {

        }

        static Grid()
        {
            Game1.Textures.Add(TextureType.Standard, Game1.Content.Load<Texture2D>("grid"));
            SideLength = Game1.WindowMiddle.X / 10;
        }

        public enum TextureType
        {
            Standard
        }

        public static int SideLength { get; private set; }
        public static int MiddleLeft { get; private set; } = Game1.WindowMiddle.X - HalfSideLength;
        public static int MiddleTop { get; private set; } = Game1.WindowMiddle.Y - HalfSideLength;
        private static int HalfSideLength { get; set; } = SideLength / 2;
        
        public void PlaceMark(Mark mark)
        {
            Mark = mark;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Mark?.Draw(spriteBatch);
        }

        protected override Tuple<Texture2D, Rectangle> LoadGameObject(LogicalPosition position, params object[] extra)
        {
            return new Tuple<Texture2D, Rectangle>(Game1.Textures[TextureType.Standard], new Rectangle(MiddleLeft + position.X * SideLength, MiddleTop + position.Y * SideLength, SideLength, SideLength));
        }
    }
}
