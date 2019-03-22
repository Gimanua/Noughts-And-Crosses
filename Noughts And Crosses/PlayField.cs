using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noughts_And_Crosses
{
    class PlayField
    {
        Dictionary<GameObject.LogicalPosition, Grid> Grids;


        public enum GameMode
        {
            Normal,
            Special
        }
    }
}
