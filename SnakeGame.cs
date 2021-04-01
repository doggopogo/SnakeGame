using System;
using System.Threading;
using Snake.Controller;
using Snake.Model;
using Snake.View;

namespace Snake
{
    class SnakeGame
    {
        private const int TimerLoopLimit = 3;
        public void run()
        {
            Board board = new Board();
            SnakeController controller = new SnakeController(board);
            controller.initializeFirstNode();
            MenuView menuView = new MenuView();
            HowToPlayView howToPlayView = new HowToPlayView();
            SnakeView snakeView = new SnakeView(board);
            Console.SetWindowSize(board.getGrid()[0].Length*2, (int)(board.getGrid().Length*1.2d));
            Console.CursorVisible = false;
            menuView.initializeMenu();

            board._gridEdited += snakeView.onGridEdited;
            controller._gameOver += snakeView.onGameOver;

            int timer = 0;
            int cmd = (int)MenuOption.Menu;
            bool isGameRunning = false;
            bool isInHowToPlay = false;
            while (true)
            {
                switch (cmd)
                {
                    case (int)MenuOption.NewGame:
                        Console.Clear();
                        snakeView.initializeBoardView();
                        isGameRunning = true;
                        cmd = -1;
                        break;
                    case (int)MenuOption.HowToPlay:
                        if (!isInHowToPlay)
                        {
                            Console.Clear();
                            howToPlayView.initializeHowToPlay();
                            isInHowToPlay = true;
                        }
                        cmd = howToPlayView.getKeyboardInput();
                        break;
                    case (int)MenuOption.ExitGame:
                        Console.SetCursorPosition(0, 7);
                        Console.WriteLine("Good Bye!");
                        Thread.Sleep(1000);
                        Environment.Exit(0);
                        break;
                    case (int)MenuOption.Menu:
                        if (isInHowToPlay)
                        {
                            isInHowToPlay = false;
                            Console.Clear();
                            menuView.initializeMenu();
                        }
                        cmd = menuView.getKeyboardInput();
                        break;
                }

                Thread.Sleep(20);

                if (isGameRunning)
                {
                    controller.getKeyboardInput();
                    timer++;

                    if (timer == TimerLoopLimit)
                    {
                        controller.afterOneLoop();
                        timer = 0;
                    }
                }
            }
        }
    }
}
