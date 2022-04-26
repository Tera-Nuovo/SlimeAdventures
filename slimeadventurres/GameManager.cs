using System;
using System.Collections.Generic;

namespace slimeadventurres
{
    public static class GameManager
    {
        public static string PlayerName { get; set; }
        public static bool KeepPlaying { get; set; } = true;
        private static bool FirstTurn { get; set; } = true;
        public static bool turnFinnished = false;

        public static void StartGame()
        {
            StartScreen();
            if (!WantToSkip())
            {
                Narative.PreStory();

                if (!WantToSkip())
                {
                    Narative.SlimeExplanation();
                }
            }
            Console.Clear();
        }

        public static bool MainGameLoop()
        {
            EnemyTurn();
            PlayerTurn();
            
            return true;
        }

        public static string GetName()
        {
            return Console.ReadLine();
        }

        public static bool WantToSkip()
        {
            if(Console.ReadLine() == "yes")
            {
                return true;
            } else
            {
                return false;
            }
        }

        private static void StartScreen()
        {
            Console.WriteLine("WELCOME TO SLIME ADVENTURES!");
            Console.WriteLine("----------------------------");
            Console.WriteLine("\n");
            Console.WriteLine("Press the enter key to get started!");
            Console.WriteLine("Press the 'Q' key to quit the game at any time!");
            Console.WriteLine("/_\\/_\\/_\\/_\\/_\\/_\\/_\\/_\\/_\\");
        }

        private static void PlayerTurn()
        {
            Backpack.UpdateSlimeCounters();
            while (turnFinnished == false)
            {
                PrintCurrentState();
                PrintOptions();
                PlayerAction(Console.ReadLine());
            }
            turnFinnished = false;
        }

        private static void PrintCurrentState()
        {
            Console.WriteLine("************************");
            Console.WriteLine("Life: " + Player.CurrentLife + " / " + Player.LifeCapacity);
            Console.WriteLine("Attack: " + Player.Attack);
            Console.WriteLine("************************");
            EnemyManager.PrintEnemies();
        }

        private static void PrintOptions()
        {
            Console.WriteLine("<><><><><><><><><><><><>");
            Console.WriteLine("You have the following options:");
            Console.WriteLine("Press 'A' to attack the nearest enemy!");
            Console.WriteLine("Press 'B' to open your backpack!");
            Console.WriteLine("<><><><><><><><><><><><>");
        }

        private static void PlayerAction(string action)
        {
            if (action == "a")
            {
                Player.AttackEnemy();
                turnFinnished = true;
            }
            if (action == "b")
            {
                Backpack.OpenBackpack();
            }
            if (action == "q")
            {
                turnFinnished = true;
                KeepPlaying = false;
            } else
            {
                turnFinnished = true;
            }
            EnemyManager.CheckIfDead();
        }

        private static void EnemyTurn()
        {
            Random random = new();

            if (random.Next(1, 4) == 1)
            {
                EnemyManager.EnemySpawner();

            } else if (FirstTurn == true)
            {
                EnemyManager.EnemySpawner();
                FirstTurn = false;
            }

            foreach (Enemy e in EnemyManager.Enemies)
            {
                e.EnemyAction();
            }
        }
    }
}
