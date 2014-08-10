namespace TicTacToe
{
	/// <summary>
	/// Simple class which contains the score per slot, used in minimax algorithms. 
	/// </summary>
	public class SlotScore
	{
		public int Score { get; set; }
		public int Slot { get; set; }
	}
}
