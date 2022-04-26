using System;
using System.Text.RegularExpressions;

namespace slimeadventurres
{
    public abstract class Slime
    {
        public abstract string SlimeType { get; set; }
        public abstract string Description { get; set; }
        public abstract int ID { get; set; }
        public abstract string Noise { get; set; }

        public void Use()
        {
            if (EnemyManager.Enemies.Count > 0)
            {
                SayNoise(Noise);

                Ability();

                Backpack.Slimes.Remove(this);
            }
            else
            {
                Console.WriteLine("No enemies to target!");
                Console.WriteLine("You lost some " + this.SlimeType + "!");
                Backpack.Slimes.Remove(this);
            }
        }

        public abstract void Ability();

        public void PrintDescription()
        {
            Console.Write(Description + "\n");
        }

        public void SayNoise(string noise)
        {
            Console.WriteLine("***********");
            Console.WriteLine(noise);
            Console.WriteLine("***********");
        }

    }

    public class FireSlime : Slime
    {
        public override string SlimeType { get; set; } = "Fire Slime";
        public override string Description { get; set; } = "I am a Fire Slime";
        public override int ID { get; set; } = 1;
        public override string Noise { get; set; } = "FIRE!";

        public override void Ability()
        {
            EnemyManager.Enemies[0].UpdateLife(10);
        }
    }

    public class WaterSlime : Slime
    {
        public override string SlimeType { get; set; } = "Water Slime";
        public override string Description { get; set; } = "I am a Water Slime";
        public override int ID { get; set; } = 2;
        public override string Noise { get; set; } = "WATER!";

        public override void Ability()
        {
            foreach (Enemy e in EnemyManager.Enemies)
            {
                e.UnMoveCounter = 2;
            }
        }
    }

    public class AirSlime : Slime
    {
        public override string SlimeType { get; set; } = "Air Slime";
        public override string Description { get; set; } = "I am an Air Slime";
        public override int ID { get; set; } = 3;
        public override string Noise { get; set; } = "AIR!";

        public override void Ability()
        {
            EnemyManager.Enemies[0].Distance += 5;
        }
    }

    public class EarthSlime : Slime
    {
        public override string SlimeType { get; set; } = "Earth Slime";
        public override string Description { get; set; } = "I am an Earth Slime";
        public override int ID { get; set; } = 4;
        public override string Noise { get; set; } = "EARTH!";


        public override void Ability()
        {
            foreach (Enemy e in EnemyManager.Enemies)
            {
                if (e.Distance < 6)
                {
                    e.UpdateLife(1);
                }
            }
        }
    }

    public class ThunderSlime : Slime
    {
        public override string SlimeType { get; set; } = "Thunder Slime";
        public override string Description { get; set; } = "I am an Thunder Slime";
        public override int ID { get; set; } = 5;
        public override string Noise { get; set; } = "THUNDER!";

        public override void Ability()
        {
            int index = -1;
            int HighestAttack = 0;

            foreach (Enemy e in EnemyManager.Enemies)
            {
                if (e.Attack > HighestAttack)
                {
                    HighestAttack = e.Attack;
                    index = EnemyManager.Enemies.IndexOf(e);
                }
            }
            if (index != -1)
            {
                EnemyManager.Enemies[index].UpdateLife(10);
            }
        }
    }

    public class ExplodingSlime : Slime
    {
        public override string SlimeType { get; set; } = "Exploding Slime";
        public override string Description { get; set; } = "I am an Exploding Slime";
        public override int ID { get; set; } = 6;
        public override string Noise { get; set; } = "EXPLODING!";
        private static Random random = new Random();

        public int ExplotionCounter = random.Next(2, 7);

        public void ExplotionCountDown()
        {
            ExplotionCounter -= 1;
        }

        public override void Ability()
        {
            if (EnemyManager.Enemies.Count > 0)
            {
                foreach (Enemy e in EnemyManager.Enemies)
                {
                    if (e.Distance < 6)
                    {
                        e.UpdateLife(2);
                    }
                }
            }
            Player.UpdateLife(4);
        }

    }

    public class MagmaSlime : Slime
    {
        public override string SlimeType { get; set; } = "Magma Slime";
        public override string Description { get; set; } = "I am a Magma Slime";
        public override int ID { get; set; } = 7;
        public override string Noise { get; set; } = "MAGMA!";
        public static int SelfDamage { get; } = 1;

        public override void Ability()
        {
            Regex num = new Regex("^\\d+$");

            Console.WriteLine("Write the number of the enemy you want to kill!");
            string input = Console.ReadLine();
            int number = -1;

            bool isParsable = Int32.TryParse(input, out number);

            if (number - 1 > -1 && number - 1 <= EnemyManager.Enemies.Count - 1)
                    EnemyManager.Enemies[number - 1].UpdateLife(EnemyManager.Enemies[number - 1].CurrentLife);
             else
             {
                 Console.WriteLine("`´`´`´`´`´`´`´`´`´`´`´`´`´`´");
                 Console.WriteLine("Could not be parsed.");
                 Console.WriteLine("`´`´`´`´`´`´`´`´`´`´`´`´`´`´");
             }

        }
    }

    public class GasSlime : Slime
    {
        public override string SlimeType { get; set; } = "Gas Slime";
        public override string Description { get; set; } = "I am a Gas Slime";
        public override int ID { get; set; } = 8;
        public override string Noise { get; set; } = "GAS!";

        public override void Ability()
        {
            // Make gas into poison and make the actual gas functionallity
            SlimeManager.GasCounter += 10;
            Gasing();
        }

        public static void Gasing()
        {
            foreach (Enemy e in EnemyManager.Enemies)
            {
                e.CurrentLife -= 1;
            }
            SlimeManager.GasCounter--;
        }
    }

    public class MudSlime : Slime
        {
            public override string SlimeType { get; set; } = "Mud Slime";
            public override string Description { get; set; } = "I am a Mud Slime";
            public override int ID { get; set; } = 9;
        public override string Noise { get; set; } = "MUD";

            public override void Ability()
            {
                Player.LifeCapacity += 1;
            }
        }

    public class BubbleSlime : Slime
    {
        public override string SlimeType { get; set; } = "Bubble Slime";
        public override string Description { get; set; } = "I am a Bubble Slime";
        public override int ID { get; set; } = 10;
        public override string Noise { get; set; } = "BUBBLE!";

        public override void Ability()
        {
            Player.CurrentLife = Player.LifeCapacity;
        }
    }

    public class IceSlime : Slime
    {
        public override string SlimeType { get; set; } = "Ice Slime";
        public override string Description { get; set; } = "I am an Ice Slime";
        public override int ID { get; set; } = 11;
        public override string Noise { get; set; } = "ICE!";

        public override void Ability()
        {
            SlimeManager.IceCounter += 2;
        }
    }

    public class TornadoSlime : Slime
    {
        public override string SlimeType { get; set; } = "Tornado Slime";
        public override string Description { get; set; } = "I am a Tornado Slime";
        public override int ID { get; set; } = 12;
        public override string Noise { get; set; } = "TORNADO!";

        public override void Ability()
        {
            Random random = new Random();

            foreach (Enemy e in EnemyManager.Enemies)
            {
                e.Distance = random.Next(0,21);
            }
            EnemyManager.SortAfterNearest();
        }
    }

        public class RockSlime : Slime
        {
            public override string SlimeType { get; set; } = "Rock Slime";
            public override string Description { get; set; } = "I am a Rock Slime";
            public override int ID { get; set; } = 13;
            public override string Noise { get; set; } = "ROCK!";

            public override void Ability()
            {
            foreach (Enemy e in EnemyManager.Enemies)
            {
                if (e.Distance < 5)
                {
                    e.UpdateLife(3);
                }
            }
        }
        }

        public class BigAirSlime : Slime
        {
            public override string SlimeType { get; set; } = "Big Air Slime";
            public override string Description { get; set; } = "I am a Big Air Slime";
            public override int ID { get; set; } = 14;
            public override string Noise { get; set; } = "BIG AIR!";

            public override void Ability()
            {

            foreach (Enemy e in EnemyManager.Enemies)
            {
                e.Distance += 3;
            }
        }
        }

        public class PoisonSlime : Slime
        {
            public override string SlimeType { get; set; } = "Poison Slime";
            public override string Description { get; set; } = "I am a Poison Slime";
            public override int ID { get; set; } = 15;
            public override string Noise { get; set; } = "EXPLODING!";

            public override void Ability()
            {
                Console.WriteLine("Poison!");
            }
        }

        public class CrystalSlime : Slime
        {
            public override string SlimeType { get; set; } = "Crystal Slime";
            public override string Description { get; set; } = "I am a Crystal Slime";
            public override int ID { get; set; } = 16;
            public override string Noise { get; set; } = "EXPLODING!";

            public override void Ability()
            {
                Console.WriteLine("Crystal!");
            }
        }

        public class GodSlime : Slime
        {
            public override string SlimeType { get; set; } = "God Slime";
            public override string Description { get; set; } = "I am a God Slime";
            public override int ID { get; set; } = 17;
            public override string Noise { get; set; } = "EXPLODING!";

            public override void Ability()
            {
                Console.WriteLine("God!");
            }
        }

        public class UselessSlime : Slime
        {
            public override string SlimeType { get; set; } = "Useless Slime";
            public override string Description { get; set; } = "I am a Useless Slime";
            public override int ID { get; set; } = 18;
            public override string Noise { get; set; } = "EXPLODING!";

            public override void Ability()
            {
                Console.WriteLine("Useless!");
            }
        }

        public class RandomSlime : Slime
        {
            public override string SlimeType { get; set; } = "Random Slime";
            public override string Description { get; set; } = "I am a Random Slime";
            public override int ID { get; set; } = 19;
            public override string Noise { get; set; } = "EXPLODING!";

            public override void Ability()
            {
                Console.WriteLine("Random!");
            }
        }

        public class AngleSlime : Slime
        {
            public override string SlimeType { get; set; } = "Angle Slime";
            public override string Description { get; set; } = "I am an Angle Slime";
            public override int ID { get; set; } = 20;
            public override string Noise { get; set; } = "EXPLODING!";

            public override void Ability()
            {
                Console.WriteLine("Angle!");
            }
        }
}  