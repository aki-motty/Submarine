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

            var font = asd.Engine.Graphics.CreateFont("Submarine_font.aff");
            var font2 = asd.Engine.Graphics.CreateFont("Submarine_font2.aff");

            var obj1 = new asd.TextObject2D();
            CreateText(obj1, font, 35, 100, "SUBMARINE");
            var obj2 = new asd.TextObject2D();
            CreateText(obj2, font2, 120, 280, "Press \"Z\" key");
 
            var background = new asd.TextureObject2D();
            asd.Engine.Sound.Stop(Game.id_bgm);
            background.Texture = asd.Engine.Graphics.CreateTexture2D("images\\ocean.png");
            layer.AddObject(background);
            layer.AddObject(obj1);
            layer.AddObject(obj2);
            AddLayer(layer);


        }
        public void CreateText(asd.TextObject2D obj, asd.Font font, float x, float y, String text)
        {
            var pos = new asd.Vector2DF(x, y);
            obj.Font = font;
            obj.Position = pos;
            obj.Text = text;
        }

        protected override void OnUpdated()
        {
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Z) == asd.KeyState.Push)
                asd.Engine.ChangeScene(new Scene.Game());
        }
    }
}
