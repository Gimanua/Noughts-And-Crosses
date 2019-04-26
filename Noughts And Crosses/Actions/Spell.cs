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
        public Spell(Player caster, byte manaCost, (Rectangle bounds, Texture2D texture, Color color) constructionInformation) : base(constructionInformation)
        {
            Caster = caster;
            ManaCost = manaCost;
        }

        protected Player Caster { get; }
        protected byte ManaCost { get; }
    }
}
