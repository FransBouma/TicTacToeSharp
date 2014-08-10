using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
	/// <summary>
	/// Class which implements MiniMax algorithms.
	/// </summary>
	public class MiniMaxer
	{
		/// <summary>
		/// Simple MiniMax algorithm which re-calculates all possible moves without a persistent tree, nor A|B pruning nor
		/// Node duplication checks.
		/// </summary>
		/// <param name="activeBoard">The board as it is in the current state</param>
		/// <param name="depth">The depth of the recursion, used to put weight to the result: the deeper into the recursion, the
		/// less high / low the max/min values are. Start with 0</param>
		/// <param name="ourTurn">if set to <c>true</c> it's our turn to place a piece on the board, otherwise it's the other players
		/// turn</param>
		/// <returns></returns>
		public SlotScore BruteForceMiniMax(Board activeBoard, int depth, bool ourTurn)
		{
			bool gameEnded = false;
			int scoreCurrentState = CalculateMiniMaxScore(activeBoard, depth, out gameEnded);
			if(gameEnded)
			{
				// ended
				return new SlotScore() { Score = scoreCurrentState };
			}
			var scoresPerOpenSlot = new List<SlotScore>();
			var newDepth = depth+1;
			// first calculate the scores for the open places still on the board. 
			var pieceToUse = ourTurn ? this.PlayPiece : (this.PlayPiece == PieceType.X ? PieceType.O : PieceType.X);
			foreach(var indexOpenPlaces in activeBoard.GetOpenSlots())
			{
				var activeBoardClone = activeBoard.Clone();
				activeBoardClone.PlacePiece(pieceToUse, indexOpenPlaces);
				var openSlotScore = BruteForceMiniMax(activeBoardClone, newDepth, !ourTurn);
				scoresPerOpenSlot.Add(new SlotScore() { Score = openSlotScore.Score, Slot=indexOpenPlaces });
			}
			// then, depending on whether it's our turn (pick the move with the highest score) or the other player's turn 
			// (pick the move with the lowest score), determine the move which is the best. 
			int scoreToSelect = ourTurn ? scoresPerOpenSlot.Max(s=>s.Score) : scoresPerOpenSlot.Min(s=>s.Score);
			return scoresPerOpenSlot.First(s => s.Score == scoreToSelect);
		}


		/// <summary>
		/// Calculates the minimax score of the board specified, using the depth as weight for the result.
		/// if we win, it will return a positive number, otherwise a negative number. Depth influences the max / min value to make
		/// sure the player plays on, even when it understands it's of no use (all moves lead to loss)
		/// </summary>
		/// <param name="activeBoard">The active board.</param>
		/// <param name="depth">The depth of the recursion, used to put weight to the result: the deeper into the recursion, the
		/// less high / low the max/min values are.</param>
		/// <param name="gameEnded">if set to <c>true</c> [game ended].</param>
		/// <returns>
		/// 0 for tie/in-progress games, 10-depth for win, depth-10 for loss.
		/// </returns>
		private int CalculateMiniMaxScore(Board activeBoard, int depth, out bool gameEnded)
		{
			var boardState = activeBoard.DetermineGameState();
			int toReturn = 0;
			gameEnded = false;
			switch(boardState)
			{
				case GameStateType.OWins:
					gameEnded = true;
					toReturn = this.PlayPiece == PieceType.O ? (10 - depth) : (depth - 10);
					break;
				case GameStateType.XWins:
					gameEnded = true;
					toReturn = this.PlayPiece == PieceType.X ? (10 - depth) : (depth - 10);
					break;
				case GameStateType.InProgress:
					toReturn = 0;
					break;
				case GameStateType.Tie:
					// tie or in progress means there's no maximum gain nor minimal loss to calculate, return simply 0
					gameEnded = true;
					toReturn = 0;
					break;
			}
			return toReturn;
		}


		#region Properties
		public PieceType PlayPiece { get; set; }
		#endregion
	}
}
