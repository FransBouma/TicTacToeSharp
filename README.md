TicTacToeSharp
==============

Tic Tac Toe game implemented in C# with Minimax based AI. 
Minimax is based on: https://en.wikipedia.org/wiki/Minimax
The code currently implements brute-force Minimax, without AB pruning or
node de-duplication. It does implement result-weight based on recursion-depth 
so the computer AI picks the move with the deepest recursion depth to result in 
a loss, if the AI finds itself in a situation it can't win or it's always a tie. 
This to make it put on a fight. 

But it's not likely you'll run into that situation, as it's currently unbeatable 
(always win or tie) ;)

Who starts and who plays with what (O or X) is randomized.