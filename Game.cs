using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
	public class Game
	{
		#region Members
		private Board _activeBoard;
		private Player _player1, _player2;
		#endregion

		/// <summary>
		/// Starts a new game and enters the game loop. Exists when game is over.
		/// </summary>
		public void Start()
		{
			_activeBoard = new Board();
			CreatePlayers();
			Console.WriteLine("Game starts!\n{0} plays with {1}, {2} plays with {3}.\n{0} begins.", _player1.Description, 
												_player1.PlayPiece, _player2.Description, _player2.PlayPiece);
			GameLoop();
			Console.WriteLine(">>> Done!");
		}


		/// <summary>
		/// The game loop. controls the game play between the two players and exits when game is over.
		/// </summary>
		private void GameLoop()
		{
			for(int i = 1; i <= GameConstants.MaxNumberOfSlots; i++)
			{
				Console.WriteLine("\n>>> Move {0}. Board state:", i);
				_activeBoard.Display();
				MakePlayerMove(i, _player1);
				_activeBoard.Display();
				bool gameEnded = DisplayGameResult();
				if(gameEnded)
				{
					break;
				}
				MakePlayerMove(i, _player2);
				gameEnded = DisplayGameResult();
				if(gameEnded)
				{
					break;
				}
			}
			Console.WriteLine(">>> Final board state:");
			_activeBoard.Display();
		}


		private void MakePlayerMove(int moveNo, Player toPlay)
		{
			var slotPlayed = toPlay.PlacePieceOnBoard(moveNo, _activeBoard);
			Console.WriteLine("\n>>> {0} played {1} on slot {2}", toPlay.Description, toPlay.PlayPiece, slotPlayed);
		}


		/// <summary>
		/// Displays the game result, if anyone wins or if it's a tie
		/// </summary>
		/// <returns>true if the game ended, otherwise false.</returns>
		private bool DisplayGameResult()
		{
			bool gameEnded = false;
			var gameResult = _activeBoard.DetermineGameState();
			switch(gameResult)
			{
				case GameStateType.OWins:
					Console.WriteLine(">>> O wins!");
					gameEnded = true;
					break;
				case GameStateType.XWins:
					Console.WriteLine(">>> X wins!");
					gameEnded = true;
					break;
				case GameStateType.Tie:
					Console.WriteLine(">>> It's a tie!");
					gameEnded = true;
					break;
				case GameStateType.InProgress:
					// do nothin'
					break;
			}
			return gameEnded;
		}


		private void CreatePlayers()
		{
			var rand = new Random((int)DateTime.Now.Ticks);
			var player1PieceType = (PieceType)(1 + (rand.Next(100) % 2));
			var player2PieceType = player1PieceType == PieceType.O ? PieceType.X : PieceType.O;
			if((rand.Next(100) % 2) == 0)
			{
				// computer begins.
				_player1 = new ComputerPlayer(player1PieceType);
				_player2 = new HumanPlayer(player2PieceType);
			}
			else
			{
				// human begins
				_player1 = new HumanPlayer(player1PieceType);
				_player2 = new ComputerPlayer(player2PieceType);
			}
		}
	}
}
