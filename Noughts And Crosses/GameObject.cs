namespace Noughts_And_Crosses
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Noughts_And_Crosses.GameObjects;
    using System;

    abstract class GameObject
    {
        protected Rectangle Bounds { get; set; }
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

            public static LogicalPosition GetLogicalPosition(Point mousePoint, Point cameraPoint)
            {
                Point combined = mousePoint + cameraPoint;
                //Kan inte med säkerhet säga varför -1 behövs på y.
                int x = combined.X / Grid.SideLength - Grid.MiddleLeft / Grid.SideLength;
                int y = combined.Y / Grid.SideLength - Grid.MiddleTop / Grid.SideLength;
                return new LogicalPosition(x, y);
                //MiddleLeft + position.X * SideLength, MiddleTop + position.Y * SideLength, SideLength, SideLength
            }
        }
    }
}
