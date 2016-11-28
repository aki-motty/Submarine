using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016ShootingBase.Charactor
{
    sealed class Radar : asd.TextureObject2D
    {
        public Radar(asd.Vector2DF pos)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("images\\radar.png");
            Scale = new asd.Vector2DF(Size.X / Texture.Size.X, Size.Y / Texture.Size.Y);
            CenterPosition = Texture.Size.To2DF() / 2;
            Position = pos;
        }

        protected override void OnUpdate()
        {
            var playerPos = Scene.Game.playerpos;
            var position = Position;
            var vec = playerPos - position;
            vec = vec.Normal;
            Position = position; 
            var angle = this.Angle;
            angle += 3;
            this.Angle = angle;
            if (count % 120 == 0)
            {
                Dispose();
            }
            count = (count + 1) % 1200;
        }

        private asd.Vector2DF Size { get; } = new asd.Vector2DF(200.0f, 200.0f);
        private int count = 1;
    }
}
