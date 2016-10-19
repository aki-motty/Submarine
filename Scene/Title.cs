﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016ShootingBase.Scene
{
    sealed class Title : asd.Scene
    {
        public Title()
        {
            var layer = new asd.Layer2D();

            var background = new asd.TextureObject2D();
            asd.Engine.Sound.Stop(Game.id_bgm);
            background.Texture = asd.Engine.Graphics.CreateTexture2D("C:\\Users\\AYoshimasa\\gitgit\\2016STGBase\\images\\Title.png");
            layer.AddObject(background);

            AddLayer(layer);
        }
         
        protected override void OnUpdated()
        {
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Z) == asd.KeyState.Push)
                asd.Engine.ChangeScene(new Scene.Game());
        }
    }
}
