using System;
using System.Collections.Generic;

namespace slimeadventurres
{
    public static class Player
    {
        // Properties
        public static int LifeCapacity { get; set; } = 20;
        public static int CurrentLife { get; set; } = 20;
        public static int Attack { get; set; } = 3;
        public static bool Dead { get; set; } = false;

        //MAYBE LATER
        //public static int Defence { get; set; }
        //public static int AttackTypes { get; set; }
        //public static int Speed { get; set; }

        //MAYBE LATER
        //private static int defence = 10;
        //private static int attackTypes = 0;


        public static void AttackEnemy()
        {
            if (EnemyManager.Enemies.Count > 0)
            {
                EnemyManager.Enemies[0].UpdateLife(Attack);
            }
        }

        public static void SeeBackpack()
        {

        }

        public static void UpdateLife(int damage)
        {
            CurrentLife -= damage;
            CheckIfDead();
        }

        private static void CheckIfDead()
        {
            if (CurrentLife < 1)
            {
                Die();
            }
        }

        private static void Die()
        {
            Dead = true;
            Console.WriteLine("-------------------");
            Console.WriteLine("YOU ARE DEAD!");
            Console.WriteLine("-------------------");

        }
    }
}
