namespace Noughts_And_Crosses
{
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using System.Collections.Generic;
    using System.Diagnostics;
    using static Noughts_And_Crosses.GameObject;
    using static Noughts_And_Crosses.Mark;

    sealed class PlayField
    {
        public PlayField(GameMode gameMode)
        {
            Mode = gameMode;
            GenerateGrids();
        }

        public enum GameMode
        {
            Normal,
            Special
        }

        public Dictionary<LogicalPosition, Grid> Grids { get; private set; }
        public GameMode Mode { get; }
        private MarkType CurrentMark { get; set; } = MarkType.Cross;

        public void Update(MouseState mouseState)
        {
            if(mouseState.LeftButton == ButtonState.Pressed && !Game1.AlreadyPressing)
            {
                LogicalPosition logicalPosition = LogicalPosition.GetLogicalPosition(mouseState.Position);
                if (Grids.TryGetValue(logicalPosition, out Grid grid) && grid.Mark == null)
                {
                    grid.Mark = new Mark(logicalPosition, CurrentMark);
                    if (CheckForWin(logicalPosition, CurrentMark))
                    {
                        Debug.WriteLine($"{CurrentMark} won!");
                        GenerateGrids();
                        return;
                    }
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

        private void GenerateGrids()
        {
            Grids = new Dictionary<LogicalPosition, Grid>();
            for (int x = -10; x <= 10; x++)
            {
                for (int y = -10; y <= 10; y++)
                {
                    LogicalPosition position = new LogicalPosition(x, y);
                    Grids.Add(position, new Grid(position));
                }
            }
        }

        private bool CheckForWin(LogicalPosition placePosition, MarkType mark)
        {
            if (CheckDirection(placePosition, mark, 1, 1) || CheckDirection(placePosition, mark, 1, -1) || CheckDirection(placePosition, mark, 1, 0) || CheckDirection(placePosition, mark, 0, 1))
                return true;
            return false;
        }

        private bool CheckDirection(LogicalPosition placePosition, MarkType mark, int xIncrement, int yIncrement)
        {
            int highestMarksInARow = 0;
            for(int i = 0, marksInARow = 0,
                x = xIncrement > 0 ? placePosition.X - 4 : (xIncrement < 0 ? placePosition.X + 4 : placePosition.X), 
                y = yIncrement > 0 ? placePosition.Y - 4 : (yIncrement < 0 ? placePosition.Y + 4 : placePosition.Y); 
                i < 9; i++, x += xIncrement, y += yIncrement)
            {
                if (MarkMatch(x, y, mark) && ++marksInARow > highestMarksInARow)
                    highestMarksInARow = marksInARow;
                else
                    marksInARow = 0;
            }

            if(highestMarksInARow >= 5)
                return true;
            return false;
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
