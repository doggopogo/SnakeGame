using Snake.Model;
using System;
using System.Windows.Input;


namespace Snake.Controller
{
    class SnakeController
    {
        enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }
        public event EventHandler _gameOver;

        private Board _board;
        private Node _headNode;
        private Node _tailNode;
        private int _nodeToAdd = 4;
        private int _currentDirection = -1;
        private Boolean isGameOver = false;

        public SnakeController(Board board)
        {
            _board = board;
        }

        protected virtual void onGameOver()
        {
            if (_gameOver != null)
            {
                _gameOver(this, EventArgs.Empty);
            }
        }

        public void afterOneLoop()
        {
            if (_currentDirection != -1 && !isGameOver)
            {
                int[] previousTailNodeCoordinate = { _tailNode.getYXCoordinate()[0], _tailNode.getYXCoordinate()[1] };
                switch (_currentDirection)
                {
                    case (int)Direction.Up:
                        _headNode.moveY(-1);
                        break;
                    case (int)Direction.Down:
                        _headNode.moveY(1);
                        break;
                    case (int)Direction.Left:
                        _headNode.moveX(-1);
                        break;
                    case (int)Direction.Right:
                        _headNode.moveX(1);
                        break;
                }

                int headNodeY = _headNode.getYXCoordinate()[0];
                int headNodeX = _headNode.getYXCoordinate()[1];

                if (_board.getGrid()[headNodeY][headNodeX] == (int)Board.SquareInfo.Food)
                {
                    foodEaten();
                    _board.setAtSquare((int)Board.SquareInfo.Empty, headNodeY, headNodeX);
                }
                else if (_board.getGrid()[headNodeY][headNodeX] == (int)Board.SquareInfo.Snake)
                {
                    gameOver();
                }
                else if (headNodeY == 0 || headNodeY == _board.getGrid().Length - 1 || headNodeX == 0 || headNodeX == _board.getGrid()[0].Length - 1)
                {
                    gameOver();
                }
                if (_nodeToAdd > 0)
                {
                    addNode(previousTailNodeCoordinate[0], previousTailNodeCoordinate[1]);
                    _board.setAtSquare((int)Board.SquareInfo.Snake, headNodeY, headNodeX);
                }

                if (!isGameOver)
                {
                    _board.switchSquare(_headNode.getYXCoordinate(), previousTailNodeCoordinate);
                }
            }
        }

        public void getKeyboardInput()
        {
            if (!isGameOver)
            {
                if (Keyboard.IsKeyDown(Key.Up))
                {
                    if (_currentDirection != (int)Direction.Down)
                    {
                        setCurrentDirection((int)Direction.Up);
                    }
                }

                else if (Keyboard.IsKeyDown(Key.Down))
                {
                    if (_currentDirection != (int)Direction.Up)
                    {
                        setCurrentDirection((int)Direction.Down);
                    }
                }

                else if (Keyboard.IsKeyDown(Key.Left))
                {
                    if (_currentDirection != (int)Direction.Right)
                    {
                        setCurrentDirection((int)Direction.Left);
                    }
                }

                else if (Keyboard.IsKeyDown(Key.Right))
                {
                    if (_currentDirection != (int)Direction.Left)
                    {
                        setCurrentDirection((int)Direction.Right);
                    }
                }
            }
        }

        private void setCurrentDirection(int newDirection)
        {
            _currentDirection = newDirection;
        }

        public void initializeFirstNode()
        {
            int yCoordinate = (_board.getGrid().Length + 1) / 2;
            int xCoordinate = (_board.getGrid()[0].Length + 1) / 2;
            _board.setAtSquare((int)Board.SquareInfo.Snake, yCoordinate, xCoordinate);
            _headNode = new Node(yCoordinate, xCoordinate);
            _tailNode = _headNode;
        }

        private void foodEaten()
        {
            _nodeToAdd++;
            _board.spawnFood();
        }

        private void addNode(int y, int x)
        {
            _tailNode.setNextNode(y, x);
            _tailNode = _tailNode.getNextNode();
            _board.setAtSquare((int)Board.SquareInfo.Snake, y, x);
            _nodeToAdd--;
        }

        private void gameOver()
        {
            _currentDirection = -1;
            isGameOver = true;
            onGameOver();
        }
    }
}
