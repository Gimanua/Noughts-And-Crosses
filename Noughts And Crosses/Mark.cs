namespace Noughts_And_Crosses
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    sealed class Mark : GameObject
    {
        public Mark(LogicalPosition position, MarkType type) : base(position, type)
        {
            Type = type;
        }

        static Mark()
        {
            Game1.Textures.Add(MarkType.Cross, Game1.Content.Load<Texture2D>("Marks/cross"));
            Game1.Textures.Add(MarkType.Nought, Game1.Content.Load<Texture2D>("Marks/nought"));
            SideLength = (int)(Grid.SideLength * 0.8);
        }

        public enum MarkType
        {
            Cross,
            Nought
        }

        public MarkType Type { get; }
        private static int SideLength { get; set; }
        //(Grid.SideLength - SideLength) / 2 ger offset vi vill ha inne i rutan. Grid.MiddleLeft är X-värdet på Grid(0,0)'s vänstra kant
        private static int MiddleLeft { get; set; } = Grid.MiddleLeft + ((Grid.SideLength - SideLength) / 2);
        private static int MiddleTop { get; set; } = Grid.MiddleTop + ((Grid.SideLength - SideLength) / 2);
        
        protected override Tuple<Texture2D, Rectangle> LoadGameObject(LogicalPosition position, params object[] extra)
        {
            MarkType type = (MarkType)extra[0];
            return new Tuple<Texture2D, Rectangle>(Game1.Textures[type], new Rectangle(MiddleLeft + position.X * Grid.SideLength, MiddleTop + position.Y * Grid.SideLength,SideLength, SideLength));
        }
    }
}
