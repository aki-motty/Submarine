using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016ShootingBase.Charactor
{
    sealed class Enemy : asd.TextureObject2D
    {
        public Enemy(asd.Layer2D layer,asd.Layer2D backlayer)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("images\\sensuikan2.png");
            Scale = new asd.Vector2DF(Size.X / Texture.Size.X, Size.Y / Texture.Size.Y);
            CenterPosition = Texture.Size.To2DF() / 2;
            Position = new asd.Vector2DF(targetPosition.X, -Size.Y);

            gameLayer = layer;
            backGroundLayer = backlayer;
            
        }

        protected override void OnUpdate()
        {
            var position = Position;
            position.Y = position.Y * 0.99f + targetPosition.Y * 0.01f;
            position.X = targetPosition.X + 200 * (float)Math.Cos(ang);
            position.Y = targetPosition.Y + 30 * (float)Math.Sin(2*ang);
            Position = position;
            if (count % 1 == 0)  ang += 0.02f * (11 - hp);
            if (count % 100 == 0)
            {
                var bulletPos = Position;
                gameLayer.AddObject(new Charactor.Bullet(bulletPos, 0));
                if (hp <= 6)
                {
                    gameLayer.AddObject(new Charactor.Bullet(bulletPos, 20));
                    gameLayer.AddObject(new Charactor.Bullet(bulletPos, -20));
                }
                if (hp <= 3)
                {
                    gameLayer.AddObject(new Charactor.Bullet(bulletPos, 40));
                    gameLayer.AddObject(new Charactor.Bullet(bulletPos, -40));
                }
            }
            if (count%250 == 0)
            {
                
                var radarPos = new asd.Vector2DF(random.Next(100,551),random.Next(200,381));
                backGroundLayer.AddObject(new Charactor.Radar(radarPos));
                asd.Engine.Sound.Play(radarSE);
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
            asd.Engine.Sound.Play(explosion);
            var explosionpos = Position;
            gameLayer.AddObject(new Charactor.Explosion(explosionpos));
            if (hp < 0)
                Dispose();
        }
        public void ShotHoming()
        {
            var bulletPos = Position;
            gameLayer.AddObject(new Charactor.HomingBullet(bulletPos));
        }

        private asd.Vector2DF Size { get; } = new asd.Vector2DF(128.0f, 128.0f);
        private readonly asd.Vector2DF targetPosition = new asd.Vector2DF(300, 60);
        private asd.Layer2D gameLayer;
        private asd.Layer2D backGroundLayer;
        private asd.SoundSource explosion = asd.Engine.Sound.CreateSoundSource("sounds\\se_maoudamashii_explosion04.wav", true);
        private asd.SoundSource radarSE = asd.Engine.Sound.CreateSoundSource("sounds\\meka_mi_radar01.wav", true);
        private Random random = new System.Random();
        private int count = 0;
        private int hp = 10;
        private float ang = 0.0f;
        
    }
}
