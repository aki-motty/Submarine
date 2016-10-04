using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016ShootingBase.Charactor
{
    sealed class Bullet : asd.TextureObject2D
    {
        public Bullet(asd.Vector2DF pos)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("C:\\Users\\AYoshimasa\\gitgit\\2016STGBase\\images\\gyorai.png");
            Scale = new asd.Vector2DF(Size.X / Texture.Size.X, Size.Y / Texture.Size.Y);
            CenterPosition = Texture.Size.To2DF() / 2;
            Position = pos;
        }
        protected override void OnUpdate()
        {
            var playerpos = Scene.Game.playerpos;
            var position = Position;
            var rotation = Angle;
            var vec = playerpos - position;
            vec = vec.Normal;
            var degree = vec.Degree + 90.0f;
            position += vec * speed * 0.4f;
            rotation = degree;
            Angle = rotation;
            Position = position;

            if (Position.Y > Resource.Window.Size.Y)
                Dispose();
        }

        private const float speed = 6.0f;
        private asd.Vector2DF Size { get; } = new asd.Vector2DF(8.0f, 48.0f);
    }
}
