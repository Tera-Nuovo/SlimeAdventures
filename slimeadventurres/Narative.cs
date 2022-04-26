using System;
namespace slimeadventurres
{
    public static class Narative
    {
        public static void PreStory()
        {

            Console.WriteLine("Hello there, I am professor Nietsnei. \n I am head of the institute of slime. \n What's your name?");
            GameManager.PlayerName = GameManager.GetName();
            Console.WriteLine("Great to meet you " + GameManager.PlayerName + "!");
            Console.ReadKey();
            Console.WriteLine("I am so happy that you want to be part of this experiment!");
            Console.ReadKey();
            Console.WriteLine("Now let's just get right to it. Follow me please.");
            Console.ReadKey();
            Console.WriteLine("Are you familiar with slime?");
        }

        public static void SlimeExplanation()
        {
            Console.WriteLine("Explanation");
        }


    }
}
