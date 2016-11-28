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
            var background = new asd.TextureObject2D();
            background.Texture = asd.Engine.Graphics.CreateTexture2D("images\\ocean.png");
            backgroundLayer.AddObject(background);
            AddLayer(backgroundLayer);
            AddLayer(gameLayer);
            player = new Charactor.Player(gameLayer);
            playersLayer.AddObject(player);

            enemy = new Charactor.Enemy(gameLayer, backgroundLayer);
            gameLayer.AddObject(enemy);

            AddLayer(playersLayer);

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
            CollisionPlayerAndHomingBullet();
            CollisionEnemyAndShot();
            CollisionPlayerAndRadar();
        }

        private void CollisionPlayerAndHomingBullet()
        {
            Func<bool> isHitHomingBullet = () =>
            {
                foreach (var obj in gameLayer.Objects)
                {
                    if (!(obj is Charactor.HomingBullet))
                        continue;
                    if (player.IsHitHomingBullet(obj as Charactor.HomingBullet))
                        return true;
                }
                return false;
            };

            if (isHitHomingBullet())
            {
                player.damage();
                gameLayer.Objects.ToList().RemoveAll(o =>
                {
                    if (!(o is Charactor.Bullet) && !(o is Charactor.HomingBullet))
                        return false;
                    o.Dispose();
                    player.Texture = asd.Engine.Graphics.CreateTexture2D("images\\sensuikan.png");
                    return true;
                });
            }

        }
        private void CollisionPlayerAndRadar()
        {
            Func<bool> isHitRadar = () =>
            {
                foreach (var obj in backgroundLayer.Objects)
                {
                    if (!(obj is Charactor.Radar))
                        continue;
                    if (player.IsHitRadar(obj as Charactor.Radar))
                        return true;
                }
                return false;
            };

            if (isHitRadar())
            {
                asd.Engine.Sound.Play(rockSE);
                enemy.ShotHoming();
            }
        }
        private void CollisionPlayerAndBullet()
        {
            Func<bool> isHitBullet = () =>
            {
                foreach (var obj in gameLayer.Objects)
                {
                    if (!(obj is Charactor.Bullet))
                        continue;
                    if (player.IsHitBullet(obj as Charactor.Bullet))
                        return true;
                }
                return false;
            };

            if (isHitBullet())
            {
                player.damage();
                gameLayer.Objects.ToList().RemoveAll(o =>
                {
                    if (!(o is Charactor.Bullet) && !(o is Charactor.HomingBullet))
                        return false;
                    o.Dispose();
                    player.Texture = asd.Engine.Graphics.CreateTexture2D("images\\sensuikan.png");
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
        private asd.Layer2D playersLayer = new asd.Layer2D();
        private asd.Layer2D backgroundLayer = new asd.Layer2D();
        public static int id_bgm = 0;
        private asd.SoundSource bgm = asd.Engine.Sound.CreateSoundSource("sounds\\SoulfulPrincess.wav", false);
        private asd.SoundSource rockSE = asd.Engine.Sound.CreateSoundSource("sounds\\select06.wav", true);

    }
}
