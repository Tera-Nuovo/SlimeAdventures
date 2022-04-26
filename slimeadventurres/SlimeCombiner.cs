using System;
using System.Collections.Generic;

namespace slimeadventurres
{
    public static class SlimeCombiner
    {
        public static void CombineSlime(int a, int b)
        {
            //sorting to get the lowest ID to minimise possible combinations
            List<Slime> slimeToCombine = new();
            slimeToCombine.Add(Backpack.Slimes[a-1]);
            slimeToCombine.Add(Backpack.Slimes[b-1]);

            slimeToCombine.Sort((x, y) => x.ID.CompareTo(y.ID));

            if (slimeToCombine[0].ID == 1)
            {
                FireCombiner(slimeToCombine[1], a, b);
            }
            else if (slimeToCombine[0].ID == 2)
            {
                WaterCombiner(slimeToCombine[1], a, b);
            }
            else if (slimeToCombine[0].ID == 3)
            {
                EarthCombiner(slimeToCombine[1], a, b);
            } else if (slimeToCombine[0].ID == 4)
            {
                AirCombiner(slimeToCombine[1], a, b);
            } else
            {
                ComplexCombiner(slimeToCombine[0], slimeToCombine[1], a, b);
            }
        }

        private static void FireCombiner(Slime slime, int a, int b)
        {
            if (slime.SlimeType == "Fire Slime")
            {
                RemoveOldAndAddNew(a,b, new ThunderSlime());
            } else if (slime.SlimeType == "Water Slime")
            {
                RemoveOldAndAddNew(a, b, new ExplodingSlime());
            } else if (slime.SlimeType == "Earth Slime")
            {
                RemoveOldAndAddNew(a, b, new MagmaSlime());

            } else if (slime.SlimeType == "Air Slime")
            {
                RemoveOldAndAddNew(a, b, new GasSlime());
            }
        }

        private static void WaterCombiner(Slime slime, int a, int b)
        {

            if (slime.SlimeType == "Water Slime")
            {
                RemoveOldAndAddNew(a, b, new IceSlime());
            }
            else if (slime.SlimeType == "Earth Slime")
            {
                RemoveOldAndAddNew(a, b, new MudSlime());
            }
            else if (slime.SlimeType == "Air Slime")
            {
                RemoveOldAndAddNew(a, b, new BubbleSlime());
            }
        }

        private static void EarthCombiner(Slime slime, int a, int b)
        {

            if (slime.SlimeType == "Earth Slime")
            {
                RemoveOldAndAddNew(a, b, new RockSlime());

            }
            else if (slime.SlimeType == "Air Slime")
            {
                RemoveOldAndAddNew(a, b, new TornadoSlime());
            }
        }

        private static void AirCombiner(Slime slime, int a, int b)
        {
            if (slime.SlimeType == "Air Slime")
            {
                RemoveOldAndAddNew(a, b, new BigAirSlime());
            }
        }

        private static void ComplexCombiner(Slime slimeA, Slime slimeB, int a, int b)
        {

            if (slimeA.SlimeType == "Gas Slime" && slimeB.SlimeType == "Gas Slime")
            {
                RemoveOldAndAddNew(a, b, new PoisonSlime());
            } else if (slimeA.SlimeType == "Exploding Slime" && slimeB.SlimeType == "Bubble Slime")
            {
                RemoveOldAndAddNew(a, b, new AngleSlime());
            } else if (slimeA.SlimeType == "Magma Slime" && slimeB.SlimeType == "Ice Slime")
            {
                RemoveOldAndAddNew(a, b, new CrystalSlime());

            } else if (slimeA.SlimeType == "Crystal Slime" && slimeB.SlimeType == "Angle Slime")
            {
                RemoveOldAndAddNew(a, b, new GodSlime());

            } else if (slimeA.SlimeType == "God Slime" && slimeB.SlimeType == "God Slime")
            {
                RemoveOldAndAddNew(a, b, new UselessSlime());
            } else if (slimeA.SlimeType == "Thunder Slime" && slimeB.SlimeType == "Tornado Slime")
            {
                RemoveOldAndAddNew(a, b, new RandomSlime());

            }
            else
            {
                Console.WriteLine("SlimeCombine unsucsessfull. Try another combination!");
            }
        }

        private static void RemoveOldAndAddNew(int a, int b, Slime slime)
        {
            Backpack.Slimes.RemoveAt(a - 1);
            Backpack.Slimes.RemoveAt(b - 2);

            Backpack.PutInBackpack(slime);
            return;
        }
    }
}
