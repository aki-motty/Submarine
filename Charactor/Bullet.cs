using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016ShootingBase.Charactor
{
    sealed class Bullet : asd.TextureObject2D
    {
        public Bullet(asd.Vector2DF pos , int angle)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("images\\gyorai.png");
            Scale = new asd.Vector2DF(Size.X / Texture.Size.X, Size.Y / Texture.Size.Y);
            CenterPosition = Texture.Size.To2DF() / 2;
            Position = pos;
            aNgle = angle + 90;
            Angle += 180 + angle;
        }
        protected override void OnUpdate()
        {
            var vec = new asd.Vector2DF((float)Math.Cos(aNgle * Math.PI / 180), (float)Math.Sin(aNgle * Math.PI / 180));
            vec = vec.Normal;
            Position += vec * speed;
            acceleration += 0.005f;
            speed += acceleration;

            if (Position.Y > Resource.Window.Size.Y)
                Dispose();
        }

        private float speed = 0;
        private float acceleration = 0;
        private int aNgle = 0;
        private asd.Vector2DF Size { get; } = new asd.Vector2DF(-8.0f, 48.0f);
    }
}
