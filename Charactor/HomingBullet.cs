using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016ShootingBase.Charactor
{
    sealed class HomingBullet : asd.TextureObject2D
    {
        public HomingBullet(asd.Vector2DF pos)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("images\\gyorai.png");
            Scale = new asd.Vector2DF(Size.X / Texture.Size.X, Size.Y / Texture.Size.Y);
            CenterPosition = Texture.Size.To2DF() / 2;
            Position = pos;
        }
        protected override void OnUpdate()
        {
            var playerpos = Scene.Game.playerpos;
            var position = Position;
            var rotation = Angle;
            if (count % 10  == 0)
            {
                vec = playerpos - position;
                vec = vec.Normal;
            }
            
            var degree = (playerpos - position).Degree + 90.0f;
            position += vec * speed;
            rotation = degree;
            Angle = rotation;
            Position = position;

            if (Position.Y > Resource.Window.Size.Y)
                Dispose();
            count = (count + 1) % 1200;
        }
        private asd.Vector2DF vec = new asd.Vector2DF(0,0);
        private int count = 0;
        private const float speed = 4.0f;
        private asd.Vector2DF Size { get; } = new asd.Vector2DF(8.0f, 48.0f);
    }
}
