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
        //public abstract void Activate();
        private Rectangle Hitbox { get; set; }
        private Texture2D Texture { get; set; }
        private Color Color { get; set; }

        public Action((Rectangle hitbox, Texture2D texture, Color color) constructionInformation)
        {
            Hitbox = constructionInformation.hitbox;
            Texture = constructionInformation.texture;
            Color = constructionInformation.color;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Hitbox, Color);
        }
    }
}
