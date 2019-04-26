namespace Noughts_And_Crosses
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Noughts_And_Crosses.Actions;
    using Noughts_And_Crosses.Actions.Spells;
    using Noughts_And_Crosses.GameObjects;
    using System.Collections.Generic;
    using static Noughts_And_Crosses.GameObject;
    using static Noughts_And_Crosses.GameObjects.Mark;
    using static Noughts_And_Crosses.PlayField;

    sealed class Player
    {
        public Player(MarkType mark, GameMode mode, HandleMarkPlaced handleMarkPlaced)
        {
            Mode = mode;
            Mark = mark;
            MarkPlaced += handleMarkPlaced;
            
            if(mode == GameMode.Special)
            {
                Actions = new List<Action>
                {
                    new Meditate(),
                    new Trap(),
                    new Wait(),
                    new Explosion(this),
                    new Destroyer(this),
                    new Armageddon(this)
                };
            }
        }

        public delegate void HandleMarkPlaced(MarkType mark, LogicalPosition position);
        public event HandleMarkPlaced MarkPlaced;

        public void Update(Point mousePosition, GameTime gameTime, bool clicking)
        {
            SelectedAction?.Update(gameTime);
            LogicalPosition logicalPosition = LogicalPosition.GetLogicalPosition(mousePosition);

            //Quickfix
            if (!clicking)
                return;

            //Om man klickar på en spell/action, utför eller välj denna
            if (false)
            {

            }
            //Om man klickar på en ruta med en Action
            else if (false && (SelectedAction = Game1.Random.Next(0,2) == 0 ? new Explosion(this) : null) != null)
            {
                //Test
                Explosion explosion = SelectedAction as Explosion;
                explosion.Place(logicalPosition);
                explosion.Activate(gameTime);
            }
            //Om man klickar på en ruta vanligt
            else
            {
                
                if(Grids.TryGetValue(logicalPosition, out Grid grid) && grid.Mark == null)
                {
                    grid.Mark = new Mark(logicalPosition, Mark);
                    MarkPlaced(Mark, logicalPosition);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(Mode == GameMode.Special)
            {
                foreach (Action action in Actions)
                {
                    action.Draw(spriteBatch);
                }
            }
            //Rita ut alla actions man kan välja
            //SpriteFont, skriv ut mana
        }

        private GameMode Mode { get; }
        public MarkType Mark { get; }
        public List<Action> Actions { get; }
        //Tillfälligt
        private Explosion SelectedAction { get; set; }
        public static Dictionary<LogicalPosition, Grid> Grids { private get; set; }
        public uint Mana { get; set; } = 20;
    }
}
