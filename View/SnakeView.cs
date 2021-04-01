using System;
using System.Threading;
using Snake.Model;

namespace Snake.View
{
    class SnakeView
    {
        private readonly string[] GameOverMSG = 
        {
            "                                               \n",
            "_______________________________________________\n",
            "  ___   __   _  _ ____   ___  __  __ ____ ____ \n",
            " / __) /  \\ ( \\/ |  __) /   \\/  )(  (  __|  _ \\\n",
            "( (_ \\/  ^ \\/    )  _) (  O  )  \\/ /)   _))   /\n",
            " \\___/\\_/\\_/\\_)(_(____) \\__ / \\___/ (____|__\\_)\n",
            "_______________________________________________\n"
        };
    
        private Board _board;
        private int _score;
        public SnakeView(Board board)
        {
            _board = board;
        }

        public void initializeBoardView()
        {
            string rowString = "";
            for (int i = 0; i < _board.getGrid().Length; i++)
            {
                for (int j = 0; j< _board.getGrid()[0].Length; j++)
                {
                    if (i == 0 || i == _board.getGrid().Length-1 || j == 0 || j == _board.getGrid()[0].Length - 1)
                    {
                        rowString += 'X';
                    }
                    else
                    {
                        int square = _board.getGrid()[i][j];
                        switch (square)
                        {
                            case (int) Board.SquareInfo.Empty:
                                rowString += ' ';
                                break;
                            case (int) Board.SquareInfo.Snake:
                                rowString += 'O';
                                break;
                            case (int) Board.SquareInfo.Food:
                                rowString += '■';
                                break;
                        }
                    }

                    if (j == _board.getGrid()[0].Length - 1)
                    {
                        rowString += '\n';
                    }
                    else
                    {
                        rowString += ' ';
                    }
                }
            }
            Console.WriteLine("*** Snake Game ***");
            Console.WriteLine("Score : " + _score);
            Console.WriteLine("\n");
            Console.WriteLine(rowString);
        }

        private void updateBoard(int value, int[] YXCoordinate)
        {
            int[] translatedYXCoordinate = translateYXCoordinateToConsole(YXCoordinate);
            Console.SetCursorPosition(translatedYXCoordinate[1], translatedYXCoordinate[0]);

            switch (value)
            {
                case (int)Board.SquareInfo.Empty:
                    Console.Write(' ');
                    break;
                case (int)Board.SquareInfo.Snake:
                    Console.Write('O');
                    break;
                case (int)Board.SquareInfo.Food:
                    Console.Write('■');
                    _score += 10;
                    Console.SetCursorPosition(8,1);
                    Console.Write(_score);

                    break;
            }
        }
        public void onGridEdited(object source, BoardEventArgs args)
        {
            updateBoard(args._value, args._YXCoordinate);
        }

        public void onGameOver(object source, EventArgs args)
        {
            gameOverAnimation();
        }

        private int[] translateYXCoordinateToConsole(int[] YXCoordinate)
        {
            return new int[]{YXCoordinate[0] + 4,YXCoordinate[1]*2};
        }

        private void gameOverAnimation()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 5; i < ((_board.getGrid().Length - GameOverMSG.Length) / 2)+5; i++)
            {
                int j = 0;
                foreach (string line in GameOverMSG)
                {
                    Console.SetCursorPosition( ((_board.getGrid()[0].Length*2) + 2 - (GameOverMSG[0].Length))/2, i+j);
                    Console.Write(line);
                    j++;
                    Thread.Sleep(5);
                }
            }
            Thread.Sleep(750);
            Console.SetCursorPosition(_board.getGrid()[0].Length, ((_board.getGrid().Length - GameOverMSG.Length) / 2) + 5 + GameOverMSG.Length);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(":(");
        }
    }
}
