namespace Noughts_And_Crosses
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Noughts_And_Crosses.Actions;
    using Noughts_And_Crosses.GameObjects;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static Noughts_And_Crosses.GameObject;

    abstract class Action
    {
        public abstract void Activate();
        protected Rectangle Hitbox { get; set; }
        protected Texture2D Texture { get; set; }
        private Color Color { get; set; }
        private static Dictionary<LogicalPosition, Player> trappedGrids { get; set; }

        public delegate void ActionPerformedEventHandler(Action performedAction);
        public event ActionPerformedEventHandler ActionPerformed;
        

        public Action((Rectangle hitbox, Texture2D texture, Color color) constructionInformation, ActionPerformedEventHandler actionPerformedEventHandler, Player performer)
        {
            Performer = performer;
            ActionPerformed += actionPerformedEventHandler;
            Hitbox = constructionInformation.hitbox;
            Texture = constructionInformation.texture;
            Color = constructionInformation.color;
        }

        public bool Selected { get; set; }
        protected static Dictionary<LogicalPosition, Grid> Grids { get; set; }
        protected static Dictionary<LogicalPosition, Player> TrappedGrids { get { return trappedGrids; } }
        protected Player Performer { get; }

        public static void Initialize(Dictionary<LogicalPosition, Grid> grids, Dictionary<LogicalPosition, Player> trappedGrids)
        {
            Grids = grids;
            Action.trappedGrids = trappedGrids;
        }

        public void Update(Point staticMousePosition, Point mouseOffset)
        {
            if (Selected)
            {
                LogicalPosition logicalPosition = LogicalPosition.GetLogicalPosition(staticMousePosition + mouseOffset);
                if (Hitbox.Contains(staticMousePosition))
                {
                    Selected = false;
                }
                else if(Grids.TryGetValue(logicalPosition, out Grid grid))
                {
                    if(this is IPlaceAble)
                    {
                        IPlaceAble placeAbleAction = this as IPlaceAble;
                        placeAbleAction.Place(logicalPosition);
                    }
                    ActionPerformed(this);
                    Activate();
                    Selected = false;
                }
            }
            else
            {
                if (Hitbox.Contains(staticMousePosition))
                {
                    Selected = true;
                    if(!(this is IPlaceAble))
                    {
                        if(this is Spell)
                        {
                            if (Performer.Mana < (this as Spell).ManaCost)
                                return;
                        }
                        //Utför omedelbart
                        ActionPerformed(this);
                        Activate();
                        Selected = false;
                    }
                }
            }
        }
        /*
        public bool Update(Point mousePosition)
        {
            if (Hitbox.Contains(mousePosition))
            {
                ActionSelected(this);
                return true;
            }
            return false;
        }*/

        public virtual void DrawIcon(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            spriteBatch.Draw(Texture, Hitbox, Color);
        }

        public virtual void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            return;
        }
    }
}
