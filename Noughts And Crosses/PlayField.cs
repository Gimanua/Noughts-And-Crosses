namespace Noughts_And_Crosses
{
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using System;
    using System.Collections.Generic;
    using static Noughts_And_Crosses.GameObject;
    using static Noughts_And_Crosses.Mark;

    sealed class PlayField
    {
        private MarkType CurrentMark { get; set; } = MarkType.Cross;

        public PlayField(GameMode gameMode)
        {
            Mode = gameMode;
            for(int x = -10; x <= 10; x++)
            {
                for(int y = -10; y <= 10; y++)
                {
                    LogicalPosition position = new LogicalPosition(x, y);
                    Grids.Add(position, new Grid(position));
                }
            }
        }

        public Dictionary<LogicalPosition, Grid> Grids { get; } = new Dictionary<LogicalPosition, Grid>();
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
                LogicalPosition logicalPosition = LogicalPosition.GetLogicalPosition(mouseState.Position);
                if (Grids.TryGetValue(logicalPosition, out Grid grid) && grid.Mark == null)
                {
                    grid.Mark = new Mark(logicalPosition, CurrentMark);
                    CheckForWin(logicalPosition, CurrentMark);
                    if (CurrentMark == MarkType.Cross)
                        CurrentMark = MarkType.Nought;
                    else
                        CurrentMark = MarkType.Cross;
                }
                    
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Grid grid in Grids.Values)
            {
                grid.Draw(spriteBatch);
            }
        }

        private bool CheckForWin(LogicalPosition placePosition, MarkType mark)
        {
            for(int xI = -1; xI <= 1; xI++)
            {
                for(int yI = -1; yI <= 1; yI++)
                {
                    if (xI == 0 && yI == 0)
                        continue;
                    if (CheckDirection(placePosition, mark, xI, yI))
                        return true;
                }
            }
            return false;
        }

        private bool CheckDirection(LogicalPosition placePosition, MarkType mark, int xIncrement, int yIncrement)
        {
            for(int i = 0, x = xIncrement < 0 ? placePosition.X - 1 : (xIncrement > 0 ? placePosition.X + 1 : 0), y = yIncrement < 0 ? placePosition.Y - 1 : (yIncrement > 0 ? placePosition.Y + 1 : 0); i < 5; i++, x += xIncrement, y += yIncrement)
            {
                if (!MarkMatch(x, y, mark))
                    return false;
            }
            return true;
        }

        private bool MarkMatch(int x, int y, MarkType mark)
        {
            LogicalPosition positionToCheck = new LogicalPosition(x, y);
            if (!Grids.TryGetValue(positionToCheck, out Grid grid) || grid.Mark?.Type != mark)
                return false;
            return true;
        }
    }
}
