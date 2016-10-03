using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016ShootingBase.Charactor
{
    sealed class Enemy : asd.TextureObject2D
    {
        public Enemy(asd.Layer2D layer)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("stand2_back05_man.png");
            Scale = new asd.Vector2DF(Size.X / Texture.Size.X, Size.Y / Texture.Size.Y);
            CenterPosition = Texture.Size.To2DF() / 2;
            Position = new asd.Vector2DF(targetPosition.X, -Size.Y);

            gameLayer = layer;
        }

        protected override void OnUpdate()
        {
            var position = Position;
            position.Y = position.Y * 0.99f + targetPosition.Y * 0.01f;
            position.X = targetPosition.X + 200 * (float)Math.Cos(ang);
            position.Y = targetPosition.Y + 30 * (float)Math.Sin(2*ang);
            Position = position;
            if (count % 100 == 0) reverser *= -1;
            if (count % 5 == 0)  ang += 0.1f;

            if (count%80 == 0)
            {
                for (int i=0; i<12; i++)
                {
                    /*var angle = 2 * Math.PI * (i / 12.0f);
                    var bulletPos = Position + 36.0f * new asd.Vector2DF((float)Math.Cos(angle), (float)Math.Sin(angle));

                    angle *= 360.0f / (2 * Math.PI); // rad to degree

                    if ((count / 30) % 2 == 0)
                    {
                        angle += 90;
                        bulletPos.X += 64.0f;
                    }
                    else
                    {
                        angle -= 90;
                        bulletPos.X -= 64.0f;
                    }
                    */
                    var bulletPos = Position;
                    gameLayer.AddObject(new Charactor.Bullet(bulletPos/*, (float)angle*/));
                }
            }
            
            count = (count + 1) % 1200;
        }

        public bool IsHit(Charactor.Shot shot)
        {
            return (shot.Position - Position).Length < 16;
        }

        public void damage()
        {
            hp--;
            if (hp < 4)
                Texture = asd.Engine.Graphics.CreateTexture2D("stand2_back05_man.png");
            if (hp < 0)
                Dispose();
        }

        private asd.Vector2DF Size { get; } = new asd.Vector2DF(128.0f, 128.0f);
        private readonly asd.Vector2DF targetPosition = new asd.Vector2DF(300, 60);
        private asd.Layer2D gameLayer;

        private int count = 0;
        private int hp = 10;
        private int reverser = -1;
        private float ang = 0.0f;
        private float speed = 5.0f;
        
    }
}
