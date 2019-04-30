using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noughts_And_Crosses.Actions
{
    abstract class Spell : Action
    {
        public Spell(Player caster, byte manaCost, (Rectangle bounds, Texture2D texture, Color color) constructionInformation, ActionSelectedEventHandler actionSelectedEventHandler) : base(constructionInformation, actionSelectedEventHandler)
        {
            Caster = caster;
            ManaCost = manaCost;
        }

        protected Player Caster { get; }
        protected byte ManaCost { get; }

        public override void DrawIcon(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            base.DrawIcon(spriteBatch, spriteFont);
            string manaCost = ManaCost == 0 ? "All mana" : ManaCost.ToString();
            Vector2 measuredString = spriteFont.MeasureString(manaCost);
            spriteBatch.DrawString(spriteFont, manaCost, new Vector2(Hitbox.X, Hitbox.Bottom + measuredString.Y / 4), Color.BlueViolet);
        }
    }
}
