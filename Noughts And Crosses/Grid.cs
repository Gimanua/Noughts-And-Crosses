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
        public static readonly int Width;
        public static readonly int HalfWidth;
        public static readonly int Height;
        public static readonly int HalfHeight;
        private Mark Mark;
        
        public Grid(Rectangle bounds, Texture2D texture, LogicalPosition position) : base(bounds, texture, position)
        {

        }

    }
}
