using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016ShootingBase.Charactor
{
    sealed class Explosion : asd.TextureObject2D
    {
        public Explosion(asd.Vector2DF pos)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("images\\explosion.png");
            Scale = new asd.Vector2DF(Size.X / Texture.Size.X, Size.Y / Texture.Size.Y);
            CenterPosition = Texture.Size.To2DF() / 2;
            Position = pos;
        }

        protected override void OnUpdate()
        {
            var alpha = Color;
            if (alpha.A > 0)
            {
                alpha.A -= 5;
                Color = alpha;
            }else
            {
                Dispose();
            }
        }

        private asd.Vector2DF Size { get; } = new asd.Vector2DF(100.0f, 100.0f);
    }
}
