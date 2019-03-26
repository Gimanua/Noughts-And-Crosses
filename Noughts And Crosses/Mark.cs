namespace Noughts_And_Crosses
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    sealed class Mark : GameObject
    {
        private Mark(Rectangle bounds, Texture2D texture, LogicalPosition position, MarkType type) : base(bounds, texture, position)
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
        private static int MiddleLeft { get; set; } = (Grid.SideLength - SideLength) / 2 + Grid.MiddleLeft;
        private static int MiddleTop { get; set; } = (Grid.SideLength - SideLength) / 2 + Grid.MiddleTop;
        
        public static Mark CreateMark(LogicalPosition position, MarkType type)
        {
            return new Mark(new Rectangle(
                MiddleLeft + position.X * Grid.SideLength,
                MiddleTop + position.Y * Grid.SideLength,
                SideLength, SideLength), Game1.Textures[type], position, type);
        }
    }
}
