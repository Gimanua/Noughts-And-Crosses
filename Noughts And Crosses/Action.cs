namespace Noughts_And_Crosses
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
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
        private static Dictionary<LogicalPosition, Grid> grids { get; set; }

        public delegate void ActionSelectedEventHandler(Action selectedAction);
        public event ActionSelectedEventHandler ActionSelected;

        public Action((Rectangle hitbox, Texture2D texture, Color color) constructionInformation, ActionSelectedEventHandler actionSelectedEventHandler)
        {
            ActionSelected += actionSelectedEventHandler;
            Hitbox = constructionInformation.hitbox;
            Texture = constructionInformation.texture;
            Color = constructionInformation.color;
        }

        protected static ReadOnlyDictionary<LogicalPosition, Grid> Grids { get => new ReadOnlyDictionary<LogicalPosition, Grid>(grids); }

        public static void Initialize(Dictionary<LogicalPosition, Grid> grids)
        {
            Action.grids = grids;
        }

        public void Update(Point mousePosition)
        {
            if (Hitbox.Contains(mousePosition))
                ActionSelected(this);
        }

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
