namespace Noughts_And_Crosses
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
    using Noughts_And_Crosses.Actions.Spells;

    sealed class PlayField
    {
        public PlayField(GameMode gameMode)
        {
            Mode = gameMode;
            GenerateGrids();
            CrossPlayer = new Player(MarkType.Cross, gameMode, HandleMarkPlaced);
            NoughtPlayer = new Player(MarkType.Nought, gameMode, HandleMarkPlaced);
            CurrentPlayer = Game1.Random.Next(0, 2) == 0 ? CrossPlayer : NoughtPlayer;
            Player.Grids = Grids;

            if (gameMode == GameMode.Special)
            {
                Explosion.Grids = Grids;
            }
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
        public Dictionary<LogicalPosition, Grid> Grids { get; private set; }
        //private MarkType CurrentMark { get; set; } = Game1.Random.Next(0,2) == 0 ? MarkType.Cross : MarkType.Nought;
        private LogicalPosition TopLeft { get; set; }
        private LogicalPosition BottomRight { get; set; }
        private Player CrossPlayer { get; set; }
        private Player NoughtPlayer { get; set; }
        private Player CurrentPlayer { get; set; }
        
        public Grid this[LogicalPosition logicalPosition] { get { return Grids[logicalPosition]; } }

        public void HandleMarkPlaced(MarkType mark, LogicalPosition position)
        {
            if (CheckForWin(position, mark))
            {
                //Detta är ju en lite sisådär lösning
                Debug.WriteLine($"{mark} won!");
                GenerateGrids();
                Player.Grids = Grids;
                if (Mode == GameMode.Special)
                    Explosion.Grids = Grids;
                //return;
            }

            if (position.X - 5 <= TopLeft.X)
                for (int i = 0, countTo = 5 - Math.Abs(position.X - TopLeft.X); i < countTo; i++) { AddGrids(Side.Left); }
            else if (position.X + 5 >= BottomRight.X)
                for (int i = 0, countTo = 5 - Math.Abs(position.X - BottomRight.X); i < countTo; i++) { AddGrids(Side.Right); }

            if (position.Y - 5 <= TopLeft.Y)
                for (int i = 0, countTo = 5 - Math.Abs(position.Y - TopLeft.Y); i < countTo; i++) { AddGrids(Side.Up); }
            else if (position.Y + 5 >= BottomRight.Y)
                for (int i = 0, countTo = 5 - Math.Abs(position.Y - BottomRight.Y); i < countTo; i++) { AddGrids(Side.Down); }

            if (CurrentPlayer.Equals(CrossPlayer))
                CurrentPlayer = NoughtPlayer;
            else
                CurrentPlayer = CrossPlayer;
        }

        public void Update(MouseState mouseState, Vector2 cameraLocation, GameTime gameTime)
        {
            Point combinedMouseCamera = mouseState.Position + cameraLocation.ToPoint();
            CurrentPlayer.Update(combinedMouseCamera, gameTime, mouseState.LeftButton == ButtonState.Pressed && !Game1.AlreadyPressing);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Grid grid in Grids.Values)
            {
                grid.Draw(spriteBatch);
            }
            CurrentPlayer.Draw(spriteBatch);
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
                    BottomRight = new LogicalPosition(BottomRight.X + 1, BottomRight.Y);
                    break;
                case Side.Up:
                    countTo = Math.Abs(TopLeft.X) + BottomRight.X + 1;
                    x = TopLeft.X;
                    y = TopLeft.Y - 1;
                    xI = 1;
                    TopLeft = new LogicalPosition(TopLeft.X, TopLeft.Y - 1);
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
