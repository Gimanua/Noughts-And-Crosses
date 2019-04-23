using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noughts_And_Crosses.Actions.Spells
{
    sealed class Armageddon : Spell
    {
        private new const byte ManaCost = 15;

        public Armageddon(Player caster) : base(caster, ManaCost)
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
