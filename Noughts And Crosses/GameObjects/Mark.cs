namespace Noughts_And_Crosses.GameObjects
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    sealed class Mark : GameObject
    {
        public Mark(LogicalPosition position, MarkType type) : base(GetInformation(position, type))
        {
            Type = type;
        }

        static Mark()
        {
            Game1.Textures.Add(MarkType.Cross, Game1.Content.Load<Texture2D>("Marks/cross"));
            Game1.Textures.Add(MarkType.Nought, Game1.Content.Load<Texture2D>("Marks/nought"));
            SideLength = (int)(Grid.SideLength * 0.8);
            //(Grid.SideLength - SideLength) / 2 ger offset vi vill ha inne i rutan. Grid.MiddleLeft är X-värdet på Grid(0,0)'s vänstra kant
            MiddleLeft = Grid.MiddleLeft + (Grid.SideLength - SideLength) / 2;
            MiddleTop = Grid.MiddleTop + (Grid.SideLength - SideLength) / 2;
        }

        public enum MarkType
        {
            Cross,
            Nought
        }

        public MarkType Type { get; }
        private static int SideLength { get; set; }
        private static int MiddleLeft { get; set; }
        private static int MiddleTop { get; set; }
        
        private static (LogicalPosition position, Texture2D texture, Rectangle bounds) GetInformation(LogicalPosition position, MarkType type)
        {
            return (position: position, texture: Game1.Textures[type], bounds: new Rectangle(MiddleLeft + position.X * Grid.SideLength, MiddleTop + position.Y * Grid.SideLength, SideLength, SideLength));
        }
    }
}
