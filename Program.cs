using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016ShootingBase
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            asd.Engine.Initialize(
                Resource.Window.TitleName,
                Resource.Window.Size.X, Resource.Window.Size.Y,
                new asd.EngineOption());

            asd.Engine.File.AddRootPackageWithPassword("Resource.pack", "amcr");
            asd.Engine.ChangeScene(new Scene.Title());
            while (asd.Engine.DoEvents())
            {
                asd.Engine.Update();
            }
            asd.Engine.Terminate();
        }
    }
}
