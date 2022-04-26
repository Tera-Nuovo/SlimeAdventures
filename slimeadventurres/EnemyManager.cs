using System;
using System.Collections.Generic;

namespace slimeadventurres
{
    public static class EnemyManager
    {
        public static List<Enemy> Enemies = new List<Enemy>();
        private static int EnemiesSpawned = 0;

        public static void EnemySpawner()
        {
            Enemies.Add(StrengthControler());
            SortAfterNearest();
            EnemiesSpawned++;
        }

        private static Enemy StrengthControler()
        {
            if(EnemiesSpawned < 10)
            {
                return new Enemy(1,1,1);
            }
            if (EnemiesSpawned < 20)
            {
                return new Enemy(2, 2, 2);
            }
            if (EnemiesSpawned < 30)
            {
                return new Enemy(3, 3, 3);
            }
            if (EnemiesSpawned < 40)
            {
                return new Enemy(4, 4, 4);
            } else
            {
                return new Enemy(5, 5, 5);
            }
        }

        public static void SortAfterNearest()
        {
            Enemies.Sort((x,y) => x.Distance.CompareTo(y.Distance));
        }

        public static void CheckIfDead()
        {
            for (int i = Enemies.Count-1; i > -1; i-- )
            {
                if (Enemies[i].CurrentLife <= 0)
                {
                    Enemies.Remove(Enemies[i]);
                    Console.WriteLine("------------------------");
                    Console.WriteLine("You killed an enemy!");
                    Console.WriteLine("------------------------");
                    Backpack.PutInBackpack(SlimeManager.SpawnSlime());
                    
                }
            }
        }

        public static void PrintEnemies()
        {
            foreach(Enemy e in Enemies)
            {
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("Enemy " + (Enemies.IndexOf(e) + 1) );
                Console.WriteLine("Life: " + e.CurrentLife);
                Console.WriteLine("Distance: " + e.Distance );
                Console.WriteLine("Attack: " + e.Attack);
                Console.WriteLine("Speed: " + e.Speed);
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            }
        }
    }
}
