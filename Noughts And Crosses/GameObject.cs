namespace Noughts_And_Crosses
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    abstract class GameObject
    {
        private Rectangle Bounds { get; set; }
        private readonly Texture2D Texture;
        private LogicalPosition Position { get; set; }

        public GameObject(LogicalPosition position, params object[] extra)
        {
            Tuple<Texture2D, Rectangle> tuple = LoadGameObject(position, extra);
            Texture = tuple.Item1;
            Bounds = tuple.Item2;
            Position = position;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Bounds, Color.White);
        }

        protected abstract Tuple<Texture2D, Rectangle> LoadGameObject(LogicalPosition position, params object[] extra);
        
        public struct LogicalPosition
        {
            public int X { get; set; }
            public int Y { get; set; }

            public LogicalPosition(int x, int y)
            {
                X = x;
                Y = y;
            }

            public static LogicalPosition GetLogicalPosition(Point point)
            {
                return new LogicalPosition(point.X / Grid.SideLength - Grid.MiddleLeft / Grid.SideLength, point.Y / Grid.SideLength - Grid.MiddleTop / Grid.SideLength);
            }
        }
    }
}
