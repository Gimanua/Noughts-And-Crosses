namespace Noughts_And_Crosses
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Noughts_And_Crosses.GameObjects;
    using System;

    abstract class GameObject
    {
        private Rectangle Bounds { get; set; }
        private readonly Texture2D Texture;
        private LogicalPosition Position { get; set; }

        public GameObject(Tuple<LogicalPosition, Texture2D, Rectangle> information)
        {
            Position = information.Item1;
            Texture = information.Item2;
            Bounds = information.Item3;
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

            public static LogicalPosition GetLogicalPosition(Point point)
            {
                //Kan inte med säkerhet säga varför -1 behövs på y.
                return new LogicalPosition(point.X / Grid.SideLength - Grid.MiddleLeft / Grid.SideLength, point.Y / Grid.SideLength - Grid.MiddleTop / Grid.SideLength - 1);
            }
        }
    }
}
