using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016ShootingBase.Charactor
{
    sealed class Player : asd.TextureObject2D
    {
        public Player(asd.Layer2D layer)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("images\\sensuikan.png");
            Scale = new asd.Vector2DF(Size.X / Texture.Size.X, Size.Y / Texture.Size.Y);
            CenterPosition = Texture.Size.To2DF() / 2;
            Position = Resource.Window.Size.To2DF() / 2;

            gameLayer = layer;
        }

        protected override void OnUpdate()
        {
            var position = this.Position;
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Up) == asd.KeyState.Hold)
                position.Y -= speed;
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Down) == asd.KeyState.Hold)
                position.Y += speed;
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Left) == asd.KeyState.Hold)
                position.X -= speed;
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Right) == asd.KeyState.Hold)
                position.X += speed;

            position.X = asd.MathHelper.Clamp(position.X, asd.Engine.WindowSize.X - this.Texture.Size.X / 20.0f, this.Texture.Size.X / 20.0f);
            position.Y = asd.MathHelper.Clamp(position.Y, asd.Engine.WindowSize.Y - this.Texture.Size.Y / 20.0f, this.Texture.Size.Y / 20.0f);

            this.Position = position;

            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Z) == asd.KeyState.Push)
            {
                gameLayer.AddObject(new Charactor.Shot(Position));
                asd.Engine.Sound.Play(shotSE);
            }
            
            

        }

        public bool IsHitBullet(Bullet bullet)
        {
            return (bullet.Position - Position).Length < 16;
        }
        public bool IsHitHomingBullet(HomingBullet bullet)
        {
            return (bullet.Position - Position).Length < 16;
        }
        public bool IsHitRadar(Radar radar)
        {
            var vec = Position - radar.Position;
            vec = new asd.Vector2DF(vec.X,-vec.Y);

            if (-180 + 3  <= vec.Degree && vec.Degree <= 90 - 3)
            {
                if (90 - radar.Angle - 2 < vec.Degree && vec.Degree < 90 - radar.Angle + 2)
                {
                    if (vec.Length < 100)
                    {
                        Texture = asd.Engine.Graphics.CreateTexture2D("images\\sensuikan_rockon.png");
                        CenterPosition = Texture.Size.To2DF() / 2;
                        return true;
                    }
                }
            }
            else if (90 + 3 < vec.Degree && vec.Degree < 180 - 3)
            {
                if (450 - radar.Angle - 2 < vec.Degree && vec.Degree < 450 - radar.Angle + 2)
                {
                    if (vec.Length < 120)
                    {
                        Texture = asd.Engine.Graphics.CreateTexture2D("images\\sensuikan_rockon.png");
                        CenterPosition = Texture.Size.To2DF() / 2;
                        return true;
                    }
                }
            }
            
            return false;
        }
        public void damage()
        {
            hp--;
            if (hp < 0)
                Dispose();
        }

        private asd.Vector2DF Size { get; } = new asd.Vector2DF(64.0f, 64.0f);
        private const float speed = 6;
        private int hp = 2;
        private asd.SoundSource shotSE = asd.Engine.Sound.CreateSoundSource("sounds\\se_maoudamashii_battle18.wav", true);
        private asd.Layer2D gameLayer;
    }
}
