using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noughts_And_Crosses
{
    sealed class Mark : GameObject
    {
        private static readonly int Width;
        private static readonly int Height;
        public readonly MarkType Type;

        static Mark()
        {
            Game1.Textures.Add(MarkType.Cross, Game1.Content.Load<Texture2D>("Marks/cross"));
            Game1.Textures.Add(MarkType.Nought, Game1.Content.Load<Texture2D>("Marks/nought"));
        }

        private Mark(Rectangle bounds, Texture2D texture, LogicalPosition position, MarkType type) : base(bounds, texture, position)
        {
            Type = type;
        }

        public static Mark CreateMark(LogicalPosition position, MarkType type)
        {
            return new Mark(new Rectangle(
                Game1.WindowMiddle.X - Grid.HalfWidth + position.X * Grid.Width + (Grid.Width - Width) / 2,
                Game1.WindowMiddle.Y - Grid.HalfHeight + position.Y * Grid.Height + (Grid.Height - Height) / 2,
                Width, Height), Game1.Textures[type], position, type);
        }

        public enum MarkType
        {
            Cross,
            Nought
        }
        
    }
}
