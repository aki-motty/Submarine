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
            Texture = asd.Engine.Graphics.CreateTexture2D("C:\\Users\\AYoshimasa\\gitgit\\2016STGBase\\images\\sensuikan.png");
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
            this.Position = position;

            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Z) == asd.KeyState.Push)
            {
                gameLayer.AddObject(new Charactor.Shot(Position));
                asd.Engine.Sound.Play(shotSE);
            }
        }

        public bool IsHit(Bullet bullet)
        {
            return (bullet.Position - Position).Length < 16;
        }
        public void damage()
        {
            hp--;
            if (hp < 0)
                Dispose();
        }

        private asd.Vector2DF Size { get; } = new asd.Vector2DF(64.0f, 64.0f);
        private const float speed = 6;
        private int hp = 3;
        private asd.SoundSource shotSE = asd.Engine.Sound.CreateSoundSource("C:\\Users\\AYoshimasa\\gitgit\\2016STGBase\\sounds\\se_maoudamashii_battle18.wav", true);
        private asd.Layer2D gameLayer;
    }
}
