using System;

namespace Snake
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            SnakeGame snake = new SnakeGame();
            snake.run();
        }
    }
}
