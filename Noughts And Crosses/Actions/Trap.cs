using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Noughts_And_Crosses.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Noughts_And_Crosses.GameObject;

namespace Noughts_And_Crosses.Actions
{
    sealed class Trap : Action, IPlaceAble
    {

        public Trap(ActionPerformedEventHandler actionSelectedEventHandler, Player placer) : base(GetConstructionInformation(), actionSelectedEventHandler, placer)
        {
            Placer = placer;
        }

        /*
        public override void Do()
        {
            throw new NotImplementedException();
        }
        */

        static Trap()
        {
            Game1.Textures.Add(TextureType.Standard, Game1.Content.Load<Texture2D>("trap"));
        }

        public enum TextureType
        {
            Standard
        }
        
        public Player Placer { get; }

        private static (Rectangle bounds, Texture2D texture, Color color) GetConstructionInformation()
        {
            int width = Game1.Viewport.Width;
            int height = Game1.Viewport.Height;
            return (bounds: new Rectangle((int)(width * 0.20), (int)(height * 0.85), (int)(width * 0.1), (int)(height * 0.1)), texture: Game1.Textures[TextureType.Standard], color: new Color(255, 255, 255, 255));
        }

        public override void Activate()
        {
            return;
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            foreach(var kvp in TrappedGrids)
            {
                if (kvp.Value != Placer)
                    continue;
                Rectangle gridRect = Grids[kvp.Key].Bounds;
                spriteBatch.Draw(Texture, new Rectangle(gridRect.Left + 2, gridRect.Top + 2, gridRect.Width - 4, gridRect.Height - 4), Color.White);
            }
        }

        public void Place(LogicalPosition position)
        {
            if (Grids.ContainsKey(position))
                TrappedGrids.Add(position, Placer);//TODO Fixa detta ASAP
        }
    }
}
