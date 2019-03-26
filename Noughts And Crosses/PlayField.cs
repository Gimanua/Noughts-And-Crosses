using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noughts_And_Crosses
{
    class PlayField
    {
        public PlayField(GameMode gameMode)
        {
            Mode = gameMode;
            for(short x = -10; x <= 10; x++)
            {
                for(short y = -10; y <= 10; y++)
                {
                    Grids.Add(new GameObject.LogicalPosition(x, y), new Grid())
                }
            }
        }

        public Dictionary<GameObject.LogicalPosition, Grid> Grids { get; }
        public GameMode Mode { get; }

        public enum GameMode
        {
            Normal,
            Special
        }

        public void Update()
        {

        }

        public void Draw()
        {

        }

        
    }
}
