using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
	/// <summary>
	/// The player which represents the computer player. Uses minimax algorithm to decide what to do next.
	/// </summary>
	public class ComputerPlayer : Player
	{
		public ComputerPlayer(PieceType playPiece) : base(playPiece)
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
			var mm = new MiniMaxer() { PlayPiece = this.PlayPiece };
			return mm.BruteForceMiniMax(activeBoard, 0, true).Slot;
		}


		#region Properties
		public override string Description
		{
			get { return "Computer player"; }
		}
		#endregion
	}
}
