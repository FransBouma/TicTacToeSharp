using System;

namespace TicTacToe
{
	public enum PieceType
	{
		Empty=0,
		O,
		X
	}

	public enum GameStateType
	{
		InProgress,
		OWins, 
		XWins,
		Tie
	}


	public static class GameConstants
	{
		public const int BoardSideSize = 3; // 3x3 board
		public const int MaxNumberOfSlots = BoardSideSize * BoardSideSize;
	}
}
