namespace Noughts_And_Crosses
{
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using System.Collections.Generic;

    sealed class PlayField
    {
        private Mark.MarkType CurrentMark { get; set; } = Mark.MarkType.Cross;

        public PlayField(GameMode gameMode)
        {
            Mode = gameMode;
            for(int x = -10; x <= 10; x++)
            {
                for(int y = -10; y <= 10; y++)
                {
                    GameObject.LogicalPosition position = new GameObject.LogicalPosition(x, y);
                    Grids.Add(position, new Grid(position));
                }
            }
        }

        public Dictionary<GameObject.LogicalPosition, Grid> Grids { get; } = new Dictionary<GameObject.LogicalPosition, Grid>();
        public GameMode Mode { get; }

        public enum GameMode
        {
            Normal,
            Special
        }

        public void Update(MouseState mouseState)
        {
            if(mouseState.LeftButton == ButtonState.Pressed)
            {
                GameObject.LogicalPosition logicalPosition = GameObject.LogicalPosition.GetLogicalPosition(mouseState.Position);
                if (Grids.TryGetValue(logicalPosition, out Grid grid))
                    grid.PlaceMark(new Mark(logicalPosition, CurrentMark));
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Grid grid in Grids.Values)
            {
                grid.Draw(spriteBatch);
            }
        }
    }
}
