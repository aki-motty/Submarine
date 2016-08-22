using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016ShootingBase.Scene
{
    class Clear : asd.Scene
    {
        public Clear()
        {
            var layer = new asd.Layer2D();

            var background = new asd.TextureObject2D();
            background.Texture = asd.Engine.Graphics.CreateTexture2D("clear.png");
            layer.AddObject(background);

            AddLayer(layer);
        }

        protected override void OnUpdated()
        {
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Z) == asd.KeyState.Push)
                asd.Engine.ChangeScene(new Scene.Title());
        }
    }
}
