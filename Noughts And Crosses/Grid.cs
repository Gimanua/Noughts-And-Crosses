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
        
        public Grid(Rectangle bounds, Texture2D texture, LogicalPosition position) : base(bounds, texture, position)
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

        public static Grid CreateGrid(LogicalPosition position)
        {
            return new Grid()
        }
    }
}
