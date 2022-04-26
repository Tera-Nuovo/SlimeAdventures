using System;
namespace slimeadventurres
{
    public class Enemy
    {
        public int CurrentLife { get; set; }
        public int Speed { get; set; }
        public int Attack { get; set; }
        public int Distance { get; set; }
        public int UnMoveCounter { get; set; } = 0;

        public Enemy(int currentLife, int attack, int speed)
        {
            CurrentLife = currentLife;
            Attack = attack;
            Speed = speed;
            Distance = NewDistance();
        }

        private int NewDistance()
        {
            Random random = new Random();

            return random.Next(1,21);
        }

        private void Move()
        {
            if (Distance > 0 && UnMoveCounter < 1)
            {
                Distance -= Speed;
                return;
            }

            if(UnMoveCounter > 0)
            {
                UnMoveCounter--;
            }

            if (Distance < 0)
            {
                Distance = 0;
            }
        }

        public void UpdateLife(int damage)
        {
            CurrentLife -= damage;

        }

        private void AttackPlayer()
        {
            if (Distance < 1)
            {
                Player.UpdateLife(Attack);
            }
        }

        public void EnemyAction()
        {
            if (SlimeManager.IceCounter == 0)
            {
                Move();
            }
            AttackPlayer();
        }

    }
}
