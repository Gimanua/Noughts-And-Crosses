using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Noughts_And_Crosses.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Noughts_And_Crosses.GameObject;

namespace Noughts_And_Crosses.Actions.Spells
{
    sealed class Explosion : Spell, IPlaceAble
    {
        private new const sbyte ManaCost = 0;

        public Explosion(Player caster, ActionPerformedEventHandler actionSelectedEventHandler) : base(caster, ManaCost, GetConstructionInformation(), actionSelectedEventHandler, caster)
        {
            Placer = caster;
        }

        static Explosion()
        {
            Game1.Textures.Add(TextureType.Standard, Game1.Content.Load<Texture2D>("explosion"));
        }

        public enum TextureType
        {
            Standard
        }

        private TimeSpan ExplosionStart { get; set; } = TimeSpan.Zero;
        private List<LogicalPosition> ExplosionPositions { get; set; } = new List<LogicalPosition>();
        private Color MyColor { get; set; } = new Color(255, 255, 255, 255);
        private LogicalPosition Position { get; set; }
        public Player Placer { get; }
        
        public void Update(GameTime gameTime)
        {
            if (ExplosionStart != TimeSpan.Zero)
            {
                double elapsedSeconds = (gameTime.TotalGameTime - ExplosionStart).TotalSeconds;

                if (elapsedSeconds <= 2.15)
                {
                    Performer.WaitingForAnimation = true;
                }
                else
                {
                    ExplosionPositions.Clear();
                    ExplosionStart = TimeSpan.Zero;
                    Performer.WaitingForAnimation = false;
                }
            }
            else
                Performer.WaitingForAnimation = false;
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            foreach(LogicalPosition position in ExplosionPositions)
            {
                spriteBatch.Draw(Game1.Textures[TextureType.Standard], new Rectangle(Grid.MiddleLeft + position.X * Grid.SideLength, Grid.MiddleTop + position.Y * Grid.SideLength, Grid.SideLength, Grid.SideLength), MyColor);
            }
        }
        
        public override void Activate()
        {
            Performer.WaitingForAnimation = true;
            ExplosionStart = Game1.GameTime.TotalGameTime;
            HashSet<LogicalPosition> visited = new HashSet<LogicalPosition>();
            Queue<LogicalPosition> pending = new Queue<LogicalPosition>();
            pending.Enqueue(Position);
            
            while(Caster.Mana > 0 && pending.Count > 0)
            {
                LogicalPosition current = pending.Dequeue();
                Grids[current].Mark = null;
                visited.Add(current);
                ExplosionPositions.Add(current);

                List<LogicalPosition> neighbors = GetNeighbors(current, visited);
                for(int i = neighbors.Count - 1; i >= 0; i--)
                {
                    int randomPick = Game1.Random.Next(neighbors.Count);
                    pending.Enqueue(neighbors[randomPick]);
                    neighbors.RemoveAt(randomPick);
                }

                Caster.Mana -= 1;
            }
        }
        
        private List<LogicalPosition> GetNeighbors(LogicalPosition from, HashSet<LogicalPosition> visited)
        {
            List<LogicalPosition> NeighBors = new List<LogicalPosition>();
            LogicalPosition[] sides = { new LogicalPosition(from.X - 1, from.Y), new LogicalPosition(from.X, from.Y - 1) , new LogicalPosition(from.X + 1, from.Y), new LogicalPosition(from.X, from.Y + 1) };
            foreach(LogicalPosition position in sides)
            {
                if (Grids.ContainsKey(position) && !visited.Contains(position))
                    NeighBors.Add(position);
            }
            return NeighBors;
        }

        public void Place(LogicalPosition position)
        {
            Position = position;
        }

        private static (Rectangle bounds, Texture2D texture, Color color) GetConstructionInformation()
        {
            int width = Game1.Viewport.Width;
            int height = Game1.Viewport.Height;
            return (bounds: new Rectangle((int)(width * 0.50), (int)(height * 0.85), (int)(width * 0.1), (int)(height * 0.1)), texture: Game1.Textures[TextureType.Standard], color: new Color(255, 255, 255, 255));
        }
    }
}
