namespace Noughts_And_Crosses
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    abstract class GameObject
    {
        private Rectangle Bounds { get; set; }
        private readonly Texture2D Texture;
        private LogicalPosition Position { get; set; }

        public GameObject(Rectangle bounds, Texture2D texture, LogicalPosition position)
        {
            Bounds = bounds;
            Texture = texture;
            Position = position;
        }

        public abstract void Update();

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Bounds, Color.White);
        }

        public struct LogicalPosition
        {
            public short X { get; set; }
            public short Y { get; set; }

            public LogicalPosition(short x, short y)
            {
                X = x;
                Y = y;
            }
        }
    }
}
