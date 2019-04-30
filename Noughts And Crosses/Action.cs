namespace Noughts_And_Crosses
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    abstract class Action
    {
        public abstract void Activate();
        protected Rectangle Hitbox { get; set; }
        private Texture2D Texture { get; set; }
        private Color Color { get; set; }

        public delegate void ActionSelectedEventHandler(Action selectedAction);
        public event ActionSelectedEventHandler ActionSelected;

        public Action((Rectangle hitbox, Texture2D texture, Color color) constructionInformation, ActionSelectedEventHandler actionSelectedEventHandler)
        {
            ActionSelected += actionSelectedEventHandler;
            Hitbox = constructionInformation.hitbox;
            Texture = constructionInformation.texture;
            Color = constructionInformation.color;
        }

        public void Update(Point mousePosition)
        {
            if (Hitbox.Contains(mousePosition))
                Activate();
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
