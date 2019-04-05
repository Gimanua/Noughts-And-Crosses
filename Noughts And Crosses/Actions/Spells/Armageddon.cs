using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noughts_And_Crosses.Actions.Spells
{
    sealed class Armageddon : Spell
    {
        public Armageddon(Player caster, byte manacost) : base(caster, manacost)
        {

        }

        public override void Do()
        {
            throw new NotImplementedException();
        }
    }
}
