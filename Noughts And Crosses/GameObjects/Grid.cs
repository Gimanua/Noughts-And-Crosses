namespace Noughts_And_Crosses.GameObjects
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    sealed class Grid : GameObject
    {
        public Mark Mark { get; set; }
        
        public Grid(LogicalPosition position) : base(GetTuple(position))
        {

        }

        static Grid()
        {
            Game1.Textures.Add(TextureType.Standard, Game1.Content.Load<Texture2D>("grid"));
            SideLength = Game1.WindowMiddle.X / 10;
            MiddleLeft = Game1.WindowMiddle.X - HalfSideLength;
            MiddleTop = Game1.WindowMiddle.Y - HalfSideLength;
            HalfSideLength = SideLength / 2;
        }

        public enum TextureType
        {
            Standard
        }

        public static int SideLength { get; private set; }
        public static int MiddleLeft { get; private set; }
        public static int MiddleTop { get; private set; }
        private static int HalfSideLength { get; set; }
        

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Mark?.Draw(spriteBatch);
        }

        private static Tuple<LogicalPosition, Texture2D, Rectangle> GetTuple(LogicalPosition position)
        {
            return new Tuple<LogicalPosition, Texture2D, Rectangle>(position, Game1.Textures[TextureType.Standard], new Rectangle(MiddleLeft + position.X * SideLength, MiddleTop + position.Y * SideLength, SideLength, SideLength));
        }
    }
}
