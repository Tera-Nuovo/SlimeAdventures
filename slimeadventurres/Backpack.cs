using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace slimeadventurres
{
    public static class Backpack
    {
        public static int Capacity { get; set; } = 5;

        public static List<Slime> Slimes = new List<Slime>();

        public static void OpenBackpack()
        {
            Console.WriteLine("==========================");
            Console.WriteLine("Your bag contains:");
            PrintSlime();
            Console.WriteLine("==========================");
            PrintOptions();
            PlayerAction(Console.ReadLine());
        }

        public static void CombineSlime()
        {
            Console.WriteLine("..................................");
            Console.WriteLine("Write the number of the first slime you want to combine.");
            Console.WriteLine("..................................");
            String str1 = Console.ReadLine();
            int num1 = parseString(str1);

            Console.WriteLine("..................................");
            Console.WriteLine("Write the number of the second slime you want to combine.");
            Console.WriteLine("..................................");
            String str2 = Console.ReadLine();
            int num2 = parseString(str2);
            if(num1 != -1 && num2 != -1)
            {
                SlimeCombiner.CombineSlime(num1, num2);
            } else
            {
                Console.WriteLine("-_-_-_-_-_-_-_-_-_-_-_-_-_-");
                Console.WriteLine("Combine was not possible.");
                Console.WriteLine("-_-_-_-_-_-_-_-_-_-_-_-_-_-");
            }

        }

        public static void UseSlime(Slime slimeToUse)
        {
            slimeToUse.Use();
        }

        public static void DropSlime(Slime slimeToRemove)
        {
            Slimes.Remove(slimeToRemove);
        }

        public static void PutInBackpack(Slime slimeToAdd)
        {
            if (!BagIsFull())
            {
                Console.WriteLine("()()()()()()()()()()()()()()");
                Console.WriteLine("You picked up " + slimeToAdd.SlimeType + "!");
                Console.WriteLine("()()()()()()()()()()()()()()");
                AddSlime(slimeToAdd);
            } else
            {
                Console.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^^^");
                Console.WriteLine("Ups! Your bag is full!");
                Console.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^^^");
            }

        }

        public static void AddSlime(Slime slimeToAdd)
        {
            Slimes.Add(slimeToAdd);
        }

        public static bool BagIsFull()
        {
            if (Slimes.Count >= Capacity )
            {
                return true;
            } else return false;
        }

        private static void PrintSlime()
        {
            int count = 1;
            foreach(Slime s in Slimes)
            {
                Console.WriteLine("");
                Console.WriteLine(count + ": " + s.SlimeType);
                Console.WriteLine("");
                count++;
            }
        }

        private static void PrintOptions()
        {
            Console.WriteLine("|-|-|-|-|-|-|-|-|-|-|-|-|-|-|");
            Console.WriteLine("You have the following actions:");
            Console.WriteLine("Press 'C' to combine two slimes.");
            Console.WriteLine("Press a slime number to use the slime.");
            Console.WriteLine("Press 'I' to see information on what each slime does.");
            Console.WriteLine("|-|-|-|-|-|-|-|-|-|-|-|-|-|-|");

        }

        private static void PlayerAction(string action)
        {
            Regex combine = new Regex("c");
            Regex num = new Regex("^\\d+$");
            Regex info = new Regex("i");
            Regex r = new Regex("r");

            if (r.IsMatch(action))
            {
                return;
            }

            if (combine.IsMatch(action))
            {
                CombineSlime();
                GameManager.turnFinnished = true;
            }

            if (num.IsMatch(action))
            {
                int number;

                bool isParsable = Int32.TryParse(action, out number);

                if (isParsable)
                {
                    UseSlime(Slimes[number - 1]);
                    GameManager.turnFinnished = true;
                }
                else
                    Console.WriteLine("Could not be parsed.");
            }

            if (info.IsMatch(action))
            {
                Console.WriteLine("###########################");
                Console.WriteLine("Press the number of the slime you want to know more about.");
                Console.WriteLine("###########################");
                GiveInfo(Console.ReadLine());
            }

        }

        private static void GiveInfo(string slimeNum)
        {
            Regex num = new Regex("^\\d+$");

            if (num.IsMatch(slimeNum))
            {
                int number;

                bool isParsable = Int32.TryParse(slimeNum, out number);

                if (isParsable)
                     Slimes[number - 1].PrintDescription();
                else
                Console.WriteLine("`´`´`´`´`´`´`´`´`´`´`´`´`´`´");
                Console.WriteLine("Could not be parsed.");
                Console.WriteLine("`´`´`´`´`´`´`´`´`´`´`´`´`´`´");
            }
        }

        private static int parseString(string str)
        {
            int number;

            bool isParsable = Int32.TryParse(str, out number);

            if (isParsable)
                return number;
            else
            {
                Console.WriteLine("Could not be parsed.");
                return -1;
            }

                
        }

        public static void UpdateSlimeCounters()
        {
            if (SlimeManager.GasCounter > 0)
            {
                GasSlime.Gasing();
            }
            if (SlimeManager.IceCounter > 0)
            {
                SlimeManager.IceCounter--;
            }
            for (int i = Slimes.Count -1; i > -1; i--)
            {

                if (Slimes[i].GetType() == typeof(ExplodingSlime))
                {

                    UpdateExplodingCounter((ExplodingSlime)Slimes[i], i);
                } else if (Slimes[i].GetType() == typeof(MagmaSlime))
                {

                    Player.UpdateLife(MagmaSlime.SelfDamage);
                }
            }
            EnemyManager.CheckIfDead();
        }

        private static void UpdateExplodingCounter(ExplodingSlime e, int index)
        {
            if (e.ExplotionCounter > 0)
            {
                e.ExplotionCountDown();
            }
            else
            {

                e.SayNoise(e.Noise);

                e.Ability();

                Backpack.Slimes.RemoveAt(index);
                Console.WriteLine("EXPLOOOOOOOOoooooOOOOOOOOOOOOOOOOOOOOOOOOOOODEING!");
            }
        }
    }
}
