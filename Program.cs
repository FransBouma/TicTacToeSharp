using System;

namespace TicTacToe
{
	// Simple Tic Tac Toe game, with human against computer. Board uses layout:
	// 
	//  0 | 1 | 2
	// ---+---+---
	//  3 | 4 | 5
	// ---+---+---
	//  6 | 7 | 8
	//
	// which are 'slot' indexes. A board is built from an array of slots (0-8). 
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Simple tic-tac-toe. Human against computer.");
			Play();
		}


		private static void Play()
		{
			while(true)
			{
				var game = new Game();
				game.Start();
				Console.WriteLine(">>> Press 0 to quit, any other key to play again");
				var keyRead = Console.ReadKey();
				if(keyRead.Key == ConsoleKey.D0)
				{
					return;
				}
			}
		}
	}
}
