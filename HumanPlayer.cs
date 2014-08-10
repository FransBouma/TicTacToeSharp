using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
	/// <summary>
	/// The player which represents the human player. Implements console I/O to determine the next move.
	/// </summary>
	public class HumanPlayer : Player
	{
		public HumanPlayer(PieceType playPiece) : base(playPiece)
		{
		}


		/// <summary>
		/// Determines the slot to place the next piece on. Called multiple times if returned value results in
		/// an invalid move
		/// </summary>
		/// <param name="moveNo">The move no.</param>
		/// <param name="activeBoard">The active board.</param>
		/// <returns></returns>
		protected override int DetermineSlotForNextPiece(int moveNo, Board activeBoard)
		{
			Console.WriteLine("\nType the slot number of the slot to place a '{0}'. 9 for random.", this.PlayPiece);
			int toReturn = -1;
			while(true)
			{
				var keyRead = Console.ReadKey();
				switch(keyRead.Key)
				{
					case ConsoleKey.D0:
						toReturn = 0;
						break;
					case ConsoleKey.D1:
						toReturn = 1;
						break;
					case ConsoleKey.D2:
						toReturn = 2;
						break;
					case ConsoleKey.D3:
						toReturn = 3;
						break;
					case ConsoleKey.D4:
						toReturn = 4;
						break;
					case ConsoleKey.D5:
						toReturn = 5;
						break;
					case ConsoleKey.D6:
						toReturn = 6;
						break;
					case ConsoleKey.D7:
						toReturn = 7;
						break;
					case ConsoleKey.D8:
						toReturn = 8;
						break;
					case ConsoleKey.D9:
						toReturn = GetRandomValidSlot(activeBoard);
						break;
				}
				if(toReturn >= 0)
				{
					break;
				}
			}
			return toReturn;
		}


		private int GetRandomValidSlot(Board activeBoard)
		{
			var rand = new Random((int)DateTime.Now.Ticks);
			while(true)
			{ 
				var slot = rand.Next(9);
				if(activeBoard.IsValidMove(slot))
				{
					return slot;
				}
			}
		}


		#region Properties
		public override string Description
		{
			get { return "Human player"; }
		}
		#endregion
	}
}
