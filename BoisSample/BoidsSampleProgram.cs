using System;
using System.Collections.Generic;
using System.Text;

namespace Sxta.Rti1516.BoidSample
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                BoidsSample app = new BoidsSample(null, "");
                app.Go();
            }
            catch
            {
                // Check if it's an Ogre Exception
                if (Mogre.OgreException.IsThrown)
                    Example.ShowOgreException();
                else
                    throw;
            }
        }
    }
}