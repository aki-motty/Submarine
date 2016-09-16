using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016ShootingBase.Charactor
{
    sealed class Bullet : asd.TextureObject2D
    {
        public Bullet(asd.Vector2DF pos/*, float angle*/)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("C:\\Users\\AYoshimasa\\gitgit\\2016STGBase\\Charactor\\shot.png");
            Scale = new asd.Vector2DF(Size.X / Texture.Size.X, Size.Y / Texture.Size.Y);
            CenterPosition = Texture.Size.To2DF() / 2;
            Position = pos;
            //Angle = angle + 90;
        }
        protected override void OnUpdate()
        {
            var playerpos = Scene.Game.playerpos;
            var position = Position;
            var vec = playerpos - position;
            vec = vec.Normal;
            //position.X += speed;//* (float)Math.Cos((Angle - 90) * 2 * Math.PI / 360);
            //position.Y += speed;//* (float)Math.Sin((Angle - 90) * 2 * Math.PI / 360);
            position += vec * speed * 0.6f;
            Position = position;

            if (Position.Y > Resource.Window.Size.Y)
                Dispose();
        }

        private const float speed = 6.0f;
        private asd.Vector2DF Size { get; } = new asd.Vector2DF(16.0f, 32.0f);
    }
}
