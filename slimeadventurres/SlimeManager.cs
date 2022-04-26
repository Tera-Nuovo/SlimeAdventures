using System;
using System.Collections.Generic;

namespace slimeadventurres
{
    public static class SlimeManager
    {
        public static int GasCounter { get; set; } = 0;
        public static int IceCounter { get; set; } = 0;

        public static Slime SpawnSlime()
        {
            Random random = new Random();

            switch(random.Next(1, 5))
            {
                case 1:
                    return new FireSlime();
                case 2:
                    return new WaterSlime();
                case 3:
                    return new AirSlime();
                default:
                    return new EarthSlime();
            }

        }
    }
}
