namespace Noughts_And_Crosses
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Noughts_And_Crosses.GameObjects;
    using System;

    abstract class GameObject
    {
        public Rectangle Bounds { get; set; }
        private readonly Texture2D Texture;
        protected LogicalPosition Position { get; set; }

        public GameObject((LogicalPosition position, Texture2D texture, Rectangle bounds) constructionInformation)
        {
            Position = constructionInformation.position;
            Texture = constructionInformation.texture;
            Bounds = constructionInformation.bounds;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Bounds, Color.White);
        }
        
        public struct LogicalPosition
        {
            public int X { get; set; }
            public int Y { get; set; }

            public LogicalPosition(int x, int y)
            {
                X = x;
                Y = y;
            }

            public static LogicalPosition GetLogicalPosition(Point combined)
            {
                double x = combined.X / (double)Grid.SideLength - Grid.MiddleLeft / (double)Grid.SideLength;
                double y = combined.Y / (double)Grid.SideLength - Grid.MiddleTop / (double)Grid.SideLength;
                return new LogicalPosition(x < 0 ? (int)x-1 : (int)x, y < 0 ? (int)y-1 : (int)y);
            }
        }
    }
}
