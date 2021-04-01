using System;

namespace Snake.Model
{
    public class BoardEventArgs : EventArgs
    {
        public int _value { get; set; }
        public int[] _YXCoordinate { get; set; }
    }
    public class Board
    {
        private enum BoardConfig
        {
            DefaultBoardLength = 40,
            DefaultBoardHeight = 30,
            BoardEdgeOffset = 2
        }

        public enum SquareInfo
        {
            Empty,
            Snake,
            Food
        }

        private int[][] _grid;

        public event EventHandler<BoardEventArgs> _gridEdited;
        public Board() : this((int)BoardConfig.DefaultBoardHeight, (int)BoardConfig.DefaultBoardLength)
        {
        }

        public Board(int boardHeight, int boardLength)
        {
            initializeBoard(boardHeight, boardLength);
            spawnFood();
        }

        public void switchSquare(int[] x1, int[] x2)
        {
            int foo = _grid[x2[0]][x2[1]];
            setAtSquare(_grid[x1[0]][x1[1]],x2[0], x2[1]);
            setAtSquare(foo, x1[0], x1[1]);
        }

        protected virtual void onGridEdited(int value, int[] YXCoordinate)
        {
            if (_gridEdited != null)
            {
                _gridEdited(this,new BoardEventArgs{_value = value, _YXCoordinate = YXCoordinate});
            }
        }

        private void initializeBoard(int height, int length)
        {
            _grid = new int[height + (int)BoardConfig.BoardEdgeOffset][];
            for (int i = 0; i < _grid.Length; i++)
            {
                _grid[i] = new int[length + (int)BoardConfig.BoardEdgeOffset];
            }
        }

        public void spawnFood()
        {
            Random randomGenerator = new Random();
            int yCoordinate;
            int xCoordinate;
            do
            {
                yCoordinate = randomGenerator.Next(1, _grid.Length-1);
                xCoordinate = randomGenerator.Next(1, _grid[0].Length-1);
            } while (_grid[yCoordinate][xCoordinate] != (int)SquareInfo.Empty);

            setAtSquare((int)SquareInfo.Food, yCoordinate, xCoordinate);
        }

        public int[][] getGrid()
        {
            return _grid;
        }

        public void setAtSquare(int value, int y, int x)
        {
            _grid[y][x] = value;
            onGridEdited(value,new int[]{y,x});
        }
    }
}