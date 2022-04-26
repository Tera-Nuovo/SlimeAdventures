using System;
using System.Collections.Generic;

namespace slimeadventurres
{
    class Program
    {
        static void Main(string[] args)
        {
            //GameManager.StartGame();
            Console.Clear();
            while (GameManager.KeepPlaying)
            {
                if (!GameManager.MainGameLoop())
                {
                    GameManager.KeepPlaying = false;
                }

            }
        }

    }
}
