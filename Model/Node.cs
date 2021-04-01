namespace Snake.Model
{
    public class Node
    {
        private int[] _YXCoordinate;
        private Node _nextNode;

        public Node(int y, int x)
        {
            _YXCoordinate = new[] { y, x };
            _nextNode = null;
        }

        public Node(int y, int x, Node nextNode)
        {
            _YXCoordinate = new[] { y, x };
            _nextNode = nextNode;
        }

        public void moveY(int yOffset)
        {
            move(_YXCoordinate[0]+yOffset, _YXCoordinate[1]);
        }

        public void moveX(int xOffset)
        {
            move(_YXCoordinate[0], _YXCoordinate[1] + xOffset);
        }

        private void move(int y, int x)
        {
            if (_nextNode != null)
            {
                _nextNode.move(_YXCoordinate[0], _YXCoordinate[1]);
            }
            _YXCoordinate[0] = y;
            _YXCoordinate[1] = x;
        }

        // For use for tail node only
        public void setNextNode(int y, int x)
        {
            _nextNode = new Node(y, x);
        }

        public int[] getYXCoordinate()
        {
            return _YXCoordinate;
        }

        public void setYXCoordinate(int y, int x)
        {
            _YXCoordinate[0] = y;
            _YXCoordinate[1] = x;
        }



        public Node getNextNode()
        {
            return _nextNode;
        }

        public void setNextNode(Node nextNode)
        {
            _nextNode = nextNode;
        }
    }
}