using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noughts_And_Crosses.Actions
{
    abstract class Spell : Action
    {
        public Spell(Player caster, byte manaCost)
        {
            Caster = caster;
            ManaCost = manaCost;
        }

        protected Player Caster { get; }
        protected byte ManaCost { get; }
    }
}
