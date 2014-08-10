using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
	public class Board
	{
		#region Members
		private PieceType[] _slots;
		#endregion

		public Board()
		{
			_slots = new PieceType[GameConstants.MaxNumberOfSlots];
			for(int i = 0; i < _slots.Length; i++)
			{
				_slots[i] = PieceType.Empty;
			}
		}


		public GameStateType DetermineGameState()
		{
			// we have to test a limited number of slots whether they're part of a winning line: 0-3 . There can be just 1 piece type
			// on a line for the line to be marked as a winning line. 
			for(int i=0;i<4;i++)
			{
				bool isWinningLine = DetermineWinningLine(i);
				if(isWinningLine)
				{
					return DetermineGameStateType(i, isWinningLine);
				}
			}
			return DetermineGameStateType(-1, false);
		}

		
		public void PlacePiece(PieceType toPlace, int slot)
		{
			if(!IsValidMove(slot))
			{
				throw new InvalidOperationException(string.Format("Can't place piece at slot '{0}'", slot));
			}
			_slots[slot] = toPlace;
		}


		public bool IsValidMove(int slot)
		{
			return ((slot >= 0) && (slot < GameConstants.MaxNumberOfSlots) && (_slots[slot] == PieceType.Empty));
		}


		public Board Clone()
		{
			var toReturn = new Board();
			Array.Copy(_slots, toReturn._slots, _slots.Length);
			return toReturn;
		}

		
		public List<int> GetOpenSlots()
		{
			var toReturn = new List<int>();
			for(int i = 0; i < _slots.Length; i++)
			{
				if(_slots[i] == PieceType.Empty)
				{
					toReturn.Add(i);
				}
			}
			return toReturn;
		}


		public void Display()
		{
			Console.WriteLine("Contents:  \t\t\tSlots:");
			Console.WriteLine(" {0} | {1} | {2}\t\t\t 0 | 1 | 2", PieceTypeToString(_slots[0]), PieceTypeToString(_slots[1]), PieceTypeToString(_slots[2]));
			Console.WriteLine("---+---+---\t\t\t---+---+---");
			Console.WriteLine(" {0} | {1} | {2}\t\t\t 3 | 4 | 5", PieceTypeToString(_slots[3]), PieceTypeToString(_slots[4]), PieceTypeToString(_slots[5]));
			Console.WriteLine("---+---+---\t\t\t---+---+---");
			Console.WriteLine(" {0} | {1} | {2}\t\t\t 6 | 7 | 8", PieceTypeToString(_slots[6]), PieceTypeToString(_slots[7]), PieceTypeToString(_slots[8]));
		}
		

		private string PieceTypeToString(PieceType toConvert)
		{
			return toConvert == PieceType.Empty ? " " : toConvert.ToString();
		}


		private GameStateType DetermineGameStateType(int slot, bool isWin)
		{
			if(isWin)
			{
				return _slots[slot] == PieceType.O ? GameStateType.OWins : GameStateType.XWins;
			}
			// tie or in progress. If there are still slots left, we can't decide yet if it is a tie so report 'in progress'
			return NumberOfOpenSlots > 0 ? GameStateType.InProgress : GameStateType.Tie;
		}


		private bool DetermineWinningLine(int startSlot)
		{
			switch(startSlot)
			{
				case 0:
					// check vertical
					if(DetermineIfLineUsingOnePiece(_slots[0], _slots[3], _slots[6]))
					{
						return true;
					}
					// check horizontal
					if(DetermineIfLineUsingOnePiece(_slots[0], _slots[1], _slots[2]))
					{
						return true;
					}
					// check diagonal left->right
					return DetermineIfLineUsingOnePiece(_slots[0], _slots[4], _slots[8]);
				case 1:
					// check vertical
					return DetermineIfLineUsingOnePiece(_slots[1], _slots[4], _slots[7]);
				case 2:
					// check vertical
					if(DetermineIfLineUsingOnePiece(_slots[2], _slots[5], _slots[8]))
					{
						return true;
					}
					// check diagonal right->left
					return DetermineIfLineUsingOnePiece(_slots[2], _slots[4], _slots[6]);
				case 3:
					// check horizontal.
					return DetermineIfLineUsingOnePiece(_slots[3], _slots[4], _slots[5]);
				default: 
					// no need
					return false;
			}
		}


		private bool DetermineIfLineUsingOnePiece(PieceType piece0, PieceType piece1, PieceType piece2)
		{
			return (piece0 != PieceType.Empty) && (piece0 == piece1) && (piece1 == piece2);
		}


		#region Properties
		public int NumberOfOpenSlots
		{
			get { return _slots.Count(x => x == PieceType.Empty); }
		}
		#endregion

	}
}
