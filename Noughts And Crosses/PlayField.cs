﻿namespace Noughts_And_Crosses
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Noughts_And_Crosses.GameObjects;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System;
    using static Noughts_And_Crosses.GameObject;
    using static Noughts_And_Crosses.GameObjects.Mark;

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

        private enum Side
        {
            Left,
            Up,
            Right,
            Down
        }
        
        public GameMode Mode { get; }
        private Dictionary<LogicalPosition, Grid> Grids { get; set; }
        private MarkType CurrentMark { get; set; } = Game1.Random.Next(0,2) == 0 ? MarkType.Cross : MarkType.Nought;
        private LogicalPosition TopLeft { get; set; }
        private LogicalPosition BottomRight { get; set; }

        public Grid this[LogicalPosition logicalPosition] { get { return Grids[logicalPosition]; } }

        public void Update(MouseState mouseState, Vector2 cameraLocation)
        {
            if(mouseState.LeftButton == ButtonState.Pressed && !Game1.AlreadyPressing)
            {
                LogicalPosition logicalPosition = LogicalPosition.GetLogicalPosition(mouseState.Position + cameraLocation.ToPoint());
                if (Grids.TryGetValue(logicalPosition, out Grid grid) && grid.Mark == null)
                {
                    grid.Mark = new Mark(logicalPosition, CurrentMark);
                    if (CheckForWin(logicalPosition, CurrentMark))
                    {
                        Debug.WriteLine($"{CurrentMark} won!");
                        GenerateGrids();
                        return;
                    }
                    
                    if (logicalPosition.X - 5 <= TopLeft.X)
                        for (int i = 0, countTo = 5 - Math.Abs(logicalPosition.X - TopLeft.X); i < countTo; i++) { AddGrids(Side.Left); }
                    else if (logicalPosition.X + 5 >= BottomRight.X)
                        for (int i = 0, countTo = 5 - Math.Abs(logicalPosition.X - TopLeft.X); i < countTo; i++) { AddGrids(Side.Right); }

                    if (logicalPosition.Y - 5 <= TopLeft.Y)
                        for (int i = 0, countTo = 5 - Math.Abs(logicalPosition.Y - TopLeft.Y); i < countTo; i++) { AddGrids(Side.Up); }
                    else if (logicalPosition.Y + 5 >= BottomRight.Y)
                        for (int i = 0, countTo = 5 - Math.Abs(logicalPosition.Y - TopLeft.Y); i < countTo; i++) { AddGrids(Side.Down); }

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

        private void AddGrids(Side side)
        {
            int countTo = 0, x = 0, y = 0, xI = 0, yI = 0;
            switch (side)
            {
                case Side.Down:
                    countTo = Math.Abs(TopLeft.X) + BottomRight.X + 1;
                    x = TopLeft.X;
                    y = BottomRight.Y + 1;
                    xI = 1;
                    BottomRight = new LogicalPosition(BottomRight.X, BottomRight.Y + 1);
                    break;
                case Side.Left:
                    countTo = Math.Abs(TopLeft.Y) + BottomRight.Y + 1;
                    x = TopLeft.X - 1;
                    y = TopLeft.Y;
                    yI = 1;
                    TopLeft = new LogicalPosition(TopLeft.X - 1, TopLeft.Y);
                    break;
                case Side.Right:
                    countTo = Math.Abs(TopLeft.Y) + BottomRight.Y + 1;
                    x = BottomRight.X + 1;
                    y = TopLeft.Y;
                    yI = 1;
                    TopLeft = new LogicalPosition(TopLeft.X - 1, TopLeft.Y);
                    break;
                case Side.Up:
                    countTo = Math.Abs(TopLeft.X) + BottomRight.X + 1;
                    x = TopLeft.X;
                    y = TopLeft.Y - 1;
                    xI = 1;
                    BottomRight = new LogicalPosition(BottomRight.X, BottomRight.Y - 1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(side));
            }
            for(int count = 0; count < countTo; count++, x += xI, y += yI)
            {
                LogicalPosition position = new LogicalPosition(x, y);
                Grids.Add(position, new Grid(position));
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
            TopLeft = new LogicalPosition(-10, -10);
            BottomRight = new LogicalPosition(10, 10);
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
