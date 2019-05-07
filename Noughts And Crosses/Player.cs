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
                    new Meditate(this, HandleActionSelected),
                    new Trap(HandleActionSelected),
                    new Wait(HandleActionSelected),
                    new Explosion(this, HandleActionSelected),
                    new Destroyer(this, HandleActionSelected),
                    new Armageddon(this, HandleActionSelected)
                };
            }
        }

        public delegate void HandleMarkPlaced(MarkType mark, LogicalPosition position);
        public event HandleMarkPlaced MarkPlaced;

        //public delegate void HandlePlayerTurnEnd(Player player);
        //public event HandlePlayerTurnEnd PlayerTurnEnd;

        private GameMode Mode { get; }
        public MarkType Mark { get; }
        public List<Action> Actions { get; }
        //Tillfälligt
        public Action PreviousAction { get; private set; }
        private Action SelectedAction { get; set; }
        public static Dictionary<LogicalPosition, Grid> Grids { private get; set; }
        public uint Mana { get; set; } = 20;
        
        public void Update(Point mousePosition, Point staticMousePosition, GameTime gameTime, bool clicking)
        {
            LogicalPosition logicalPosition = LogicalPosition.GetLogicalPosition(mousePosition);

            //Quickfix
            if (!clicking)
                return;

            //Om man klickar på en ruta när man har en selected action.
            if (SelectedAction != null)
            {
                if (SelectedAction is IPlaceAble)
                {
                    IPlaceAble placeAbleAction = SelectedAction as IPlaceAble;
                    placeAbleAction.Place(logicalPosition);
                }
                SelectedAction.Activate();
                SelectedAction = null;
                //Avsluta ens tur
            }
            else if (Grids.TryGetValue(logicalPosition, out Grid grid) && grid.Mark == null)
            {
                grid.Mark = new Mark(logicalPosition, Mark);
                MarkPlaced(Mark, logicalPosition);
                //Avsluta ens tur
            }

            //Om man klickar på en spell/action, välj denna
            foreach (Action action in Actions)
            {
                action.Update(staticMousePosition);
            }
            
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            if(Mode == GameMode.Special)
            {
                foreach (Action action in Actions)
                {
                    action.Draw(spriteBatch, spriteFont);
                }
                
            }
            //Rita ut alla actions man kan välja
            //SpriteFont, skriv ut mana
        }

        public void DrawStatic(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            foreach (Action action in Actions)
            {
                action.DrawIcon(spriteBatch, spriteFont);
            }
            spriteBatch.DrawString(spriteFont, $"Current mana: {Mana.ToString()}", new Vector2(10, 10), Color.BlueViolet);
            spriteBatch.DrawString(spriteFont, $"Current player: {Mark.ToString()}", new Vector2(10, 30), Color.Red);
            spriteBatch.DrawString(spriteFont, $"Selected action: { SelectedAction?.ToString() }", new Vector2(10, 50), Color.Bisque);
        }

        public void HandleActionSelected(Action selectedAction)
        {
            //Kolla så att man valt en ny action
            if (!selectedAction.Equals(SelectedAction))
                SelectedAction = selectedAction;
            else
                SelectedAction = null;
        }
        
    }
}
