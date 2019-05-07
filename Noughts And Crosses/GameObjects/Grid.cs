namespace Noughts_And_Crosses.GameObjects
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    sealed class Grid : GameObject
    {
        public Mark Mark { get; set; }
        public Player TrappedByPlayer { get; set; }
        
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
            //spriteBatch.DrawString(Game1.SpriteFont, $"X:{Position.X}", new Vector2(Bounds.X + 2, Bounds.Y + 4), Color.DarkBlue);
            //spriteBatch.DrawString(Game1.SpriteFont, $"Y:{Position.Y}", new Vector2(Bounds.X + 2, Bounds.Y + 20), Color.DarkBlue);
        }

        private static (LogicalPosition position, Texture2D texture, Rectangle bounds) GetTuple(LogicalPosition position)
        {
            return (position: position, texture: Game1.Textures[TextureType.Standard], bounds: new Rectangle(MiddleLeft + position.X * SideLength, MiddleTop + position.Y * SideLength, SideLength, SideLength));
        }
    }
}
