using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noughts_And_Crosses.Actions
{
    sealed class Trap : Action
    {
        public Trap(ActionSelectedEventHandler actionSelectedEventHandler) : base(GetConstructionInformation(), actionSelectedEventHandler)
        {

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

        private static (Rectangle bounds, Texture2D texture, Color color) GetConstructionInformation()
        {
            int width = Game1.Viewport.Width;
            int height = Game1.Viewport.Height;
            return (bounds: new Rectangle((int)(width * 0.20), (int)(height * 0.85), (int)(width * 0.1), (int)(height * 0.1)), texture: Game1.Textures[TextureType.Standard], color: new Color(255, 255, 255, 255));
        }

        public override void Activate()
        {
            throw new NotImplementedException();
        }
    }
}
