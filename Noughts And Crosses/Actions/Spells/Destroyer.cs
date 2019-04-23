using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noughts_And_Crosses.Actions.Spells
{
    sealed class Destroyer : Spell
    {
        private new const byte ManaCost = 7;

        public Destroyer(Player caster) : base(caster, ManaCost)
        {

        }

        /*
        public override void Do()
        {
            throw new NotImplementedException();
        }
        */
    }
}
