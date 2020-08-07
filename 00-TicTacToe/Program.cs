using System;

namespace TicTacToe
{
    class MainClass
    {
        const string PLAYER_ONE_SYMBOL = "X";
        const string PLAYER_TWO_SYMBOL = "O";
        public static void Main(string[] args)
        {
            string gameOverMessage = string.Empty, errMessage = String.Empty;
            bool is_playerOne = true;
            string[,] boardValue = new string[,] { { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", "9" } };
            while (gameOverMessage.Equals(string.Empty))
            {
                viewBoard(boardValue);
                Console.WriteLine(errMessage);
                errMessage = playGame(is_playerOne, boardValue, out gameOverMessage);
                if (errMessage.Equals(string.Empty)) is_playerOne = !is_playerOne;
            }
            viewBoard(boardValue);
            Console.WriteLine("\n\t"+gameOverMessage + "\n\tThank you for playing!");
            Console.ReadKey();
        }
        public static void viewBoard(string[,] value)
        {
            Console.Clear();
            for (int i=0; i < 3; i++)
            {
                Console.WriteLine("\t     |     |\n\t  {0}  |  {1}  |  {2}\n{3}",
                    value[i,0],
                    value[i,1],
                    value[i,2],
                    (i < 2) ? "\t_____|_____|_____" : "\t     |     |");
            }
        }
        public static string playGame(bool is_playerOne, string[,] value, out string gameOverMessage)
        {
            Console.WriteLine("Current Player is Player: {0} -- {1} \nPlease select the field for your next move: ", (is_playerOne) ? 1 : 2, (is_playerOne) ? PLAYER_ONE_SYMBOL : PLAYER_TWO_SYMBOL);
            int move;
            bool input = int.TryParse(Console.ReadLine(), out move);
            gameOverMessage = string.Empty;
            if (input && move > 0 && move < 10)
            {
                int row = (move - 1) / 3, col = (move - 1) % 3;
                if (int.TryParse(value[row, col], out move))
                {
                    value[row, col] = (is_playerOne) ? PLAYER_ONE_SYMBOL : PLAYER_TWO_SYMBOL;
                    gameOverMessage = checkStatus(value);
                    return string.Empty;
                }
                else return "\tInvalid Move, Please try again";
            }
            else return "\tWrong Input, Please try again";
        }
        public static string checkStatus(string[,] value)
        {
            int[] magicSquare = { 8, 1, 6, 3, 5, 7, 4, 9, 2 };
            int counter = 0, leftover = 0, temp = 0;
            string[] board = new string[9];
            foreach (string val in value)
            {
                board[counter++] = val;
                if (int.TryParse(val, out temp)) leftover++;
            }
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    for (int k = 0; k < 9; k++)
                        if (i != j && i != k && j != k)
                        {
                            if (board[i].Equals(PLAYER_ONE_SYMBOL) && board[j].Equals(PLAYER_ONE_SYMBOL) && board[k].Equals(PLAYER_ONE_SYMBOL))
                                if (magicSquare[i] + magicSquare[j] + magicSquare[k] == 15)
                                    return "Game Over - Player One Won!";
                            if (board[i].Equals(PLAYER_TWO_SYMBOL) && board[j].Equals(PLAYER_TWO_SYMBOL) && board[k].Equals(PLAYER_TWO_SYMBOL))
                                if (magicSquare[i] + magicSquare[j] + magicSquare[k] == 15)
                                    return "Game Over - Player Two Won!";
                        }
            if (leftover < 1) return "Game Over - No Moves Left!";
            else return string.Empty;
        }
    }
}
