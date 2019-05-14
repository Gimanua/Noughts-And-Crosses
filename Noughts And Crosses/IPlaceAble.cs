using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Noughts_And_Crosses.GameObject;

namespace Noughts_And_Crosses
{
    interface IPlaceAble
    {
        void Place(LogicalPosition position);
        Player Placer { get; }
    }
}
