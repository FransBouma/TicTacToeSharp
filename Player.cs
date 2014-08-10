using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
	/// <summary>
	/// Abstract base class for a player.
	/// </summary>
	public abstract class Player
	{
		#region Members
		private PieceType _playPiece;
		#endregion

		public Player(PieceType playPiece)
		{
			if(playPiece == PieceType.Empty)
			{
				throw new ArgumentException(string.Format("playPiece value '{0}' isn't a valid value", playPiece));
			}
			_playPiece = playPiece;
		}


		public int PlacePieceOnBoard(int moveNo, Board activeBoard)
		{
			OnStartPlacePieceOnBoard(moveNo, activeBoard);
			bool piecePlaced = false;
			int slotForMove = -1;
			while(!piecePlaced)
			{
				slotForMove = DetermineSlotForNextPiece(moveNo, activeBoard);
				if(activeBoard.IsValidMove(slotForMove))
				{
					activeBoard.PlacePiece(_playPiece, slotForMove);
					piecePlaced = true;
				}
			}
			return slotForMove;
		}


		/// <summary>
		/// Called when the PlacePieceOnBoard method is called
		/// </summary>
		/// <param name="moveNo">The move no.</param>
		/// <param name="activeBoard">The active board.</param>
		protected virtual void OnStartPlacePieceOnBoard(int moveNo, Board activeBoard)
		{
			// Nop
		}

		/// <summary>
		/// Determines the slot to place the next piece on. Called multiple times if returned value results in
		/// an invalid move
		/// </summary>
		/// <param name="moveNo">The move no.</param>
		/// <param name="activeBoard">The active board.</param>
		/// <returns></returns>
		protected abstract int DetermineSlotForNextPiece(int moveNo, Board activeBoard);


		#region Properties
		public abstract string Description { get;}

		public PieceType PlayPiece
		{
			get { return _playPiece; }
		}
		#endregion
	}
}
