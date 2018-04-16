﻿using System;
using System.Collections.Generic;


namespace SampleProgram
{
    public static class Program
    {
        public static void Main()
        {
            //var game = new Game();
            var game = new ComplexGame();

            game.Setup();
            game.Play(5);

            Console.WriteLine("Press any key ...");
            Console.ReadKey();
        }
	}
}
