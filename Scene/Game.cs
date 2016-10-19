using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016ShootingBase.Scene
{
    sealed class Game : asd.Scene
    {
        public Game()
        {
            var backgroundLayer = new asd.Layer2D();
            var background = new asd.TextureObject2D();
            background.Texture = asd.Engine.Graphics.CreateTexture2D("C:\\Users\\AYoshimasa\\gitgit\\2016STGBase\\images\\ocean.png");
            backgroundLayer.AddObject(background);
            AddLayer(backgroundLayer);

            player = new Charactor.Player(gameLayer);
            gameLayer.AddObject(player);

            enemy = new Charactor.Enemy(gameLayer);
            gameLayer.AddObject(enemy);

            AddLayer(gameLayer);

            bgm.IsLoopingMode = true;
            id_bgm = asd.Engine.Sound.Play(bgm);
        }

        protected override void OnUpdated()
        {
            playerpos = player.Position;
            if (!enemy.IsAlive)
                asd.Engine.ChangeScene(new Scene.Clear());
            if (!player.IsAlive)
                asd.Engine.ChangeScene(new Scene.GameOver());
            CollisionPlayerAndBullet();
            CollisionEnemyAndShot();
        }

        private void CollisionPlayerAndBullet()
        {
            Func<bool> isHit = () =>
            {
                foreach (var obj in gameLayer.Objects)
                {
                    if (!(obj is Charactor.Bullet))
                        continue;
                    if (player.IsHit(obj as Charactor.Bullet))
                        return true;
                }
                return false;
            };

            if (isHit())
            {
                player.damage();
                gameLayer.Objects.ToList().RemoveAll(o =>
                {
                    if (!(o is Charactor.Bullet))
                        return false;
                    o.Dispose();
                    return true;
                });
            }

        }

        private void CollisionEnemyAndShot()
        {
            gameLayer.Objects.ToList().RemoveAll(o =>
            {
                if (!(o is Charactor.Shot))
                    return false;
                if (!enemy.IsHit(o as Charactor.Shot))
                    return false;
                o.Dispose();
                enemy.damage();
                return true;
            });
        }
        private Charactor.Player player;
        private Charactor.Enemy enemy;
        public static asd.Vector2DF playerpos ;
        private asd.Layer2D gameLayer = new asd.Layer2D();
        public static int id_bgm = 0;
        private asd.SoundSource bgm = asd.Engine.Sound.CreateSoundSource("C:\\Users\\AYoshimasa\\gitgit\\2016STGBase\\sounds\\SoulfulPrincess.wav", false);
    }
}
