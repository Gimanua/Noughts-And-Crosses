using Noughts_And_Crosses.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Noughts_And_Crosses.GameObject;

namespace Noughts_And_Crosses.Actions.Spells
{
    sealed class Explosion : Spell
    {
        public Explosion(Player caster, byte manaCost, LogicalPosition position) : base(caster, manaCost)
        {
            Position = position;
        }

        private LogicalPosition Position { get; }
        private Dictionary<LogicalPosition, Grid> Grids { get; set; }

        public override void Do()
        {
            HashSet<LogicalPosition> visited = new HashSet<LogicalPosition>();
            Queue<LogicalPosition> pending = new Queue<LogicalPosition>();
            pending.Enqueue(Position);

            
            while(Caster.Mana > 0 && pending.Count > 0)
            {
                LogicalPosition current = pending.Dequeue();
                Grids[current].Mark = null;

                List<LogicalPosition> neighbors = GetNeighbors(current);
                foreach(LogicalPosition neighbor in neighbors)
                {
                    pending.Enqueue(neighbor);
                }
            }
        }
        
        private List<LogicalPosition> GetNeighbors(LogicalPosition from)
        {
            List<LogicalPosition> NeighBors = new List<LogicalPosition>();
            LogicalPosition[] sides = { new LogicalPosition(from.X - 1, from.Y), new LogicalPosition(from.X, from.Y - 1) , new LogicalPosition(from.X + 1, from.Y), new LogicalPosition(from.X, from.Y + 1) };
            foreach(LogicalPosition position in sides)
            {
                if (Grids.ContainsKey(position))
                    NeighBors.Add(position);
            }
            return NeighBors;
        }
    }
}
