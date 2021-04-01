using System;
using System.Windows.Input;

namespace Snake.View
{
    public enum MenuOption
    {
        NewGame,
        HowToPlay,
        ExitGame,
        Menu
    }
    class MenuView
    {
        private const int StartingCursorPosition = 3;
        private bool pressed = false;
        private int _cursorPosition;
        public void initializeMenu()
        {
            _cursorPosition = StartingCursorPosition;
            Console.WriteLine("******************");
            Console.WriteLine("*** Snake Game ***");
            Console.WriteLine("******************");
            Console.WriteLine(" * - New Game");
            Console.WriteLine("   - How to play");
            Console.WriteLine("   - Exit Game");
        }

        public int getKeyboardInput()
        {
            if (Keyboard.IsKeyDown(Key.Up))
            {
                if (_cursorPosition > 3 && !pressed)
                {
                    Console.SetCursorPosition(1, _cursorPosition);
                    Console.Write(' ');
                    _cursorPosition--;
                    Console.SetCursorPosition(1, _cursorPosition);
                    Console.Write('*');
                    pressed = true;
                }
            }

            if (Keyboard.IsKeyDown(Key.Down) && !pressed)
            {
                if (_cursorPosition < 5)
                {
                    Console.SetCursorPosition(1, _cursorPosition);
                    Console.Write(' ');
                    _cursorPosition++;
                    Console.SetCursorPosition(1, _cursorPosition);
                    Console.Write('*');
                    pressed = true;
                }
            }

            if (Keyboard.IsKeyUp(Key.Up) && Keyboard.IsKeyUp(Key.Down) && Keyboard.IsKeyUp(Key.Enter))
            {
                pressed = false;
            }

            if (Keyboard.IsKeyDown(Key.Enter) && !pressed)
            {
                return _cursorPosition - StartingCursorPosition;
            }

            return (int)MenuOption.Menu;
        }
    }
}
