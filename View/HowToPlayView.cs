using System;
using System.Windows.Input;

namespace Snake.View
{
    public class HowToPlayView
    {
        public void initializeHowToPlay()
        {
            Console.WriteLine("*******************");
            Console.WriteLine("*** How to play ***");
            Console.WriteLine("*******************");
            Console.WriteLine("Goal: stay alive as long as possible and beat your last score");
            Console.WriteLine("1- Use arrows to change directions");
            Console.WriteLine("2- To make points, you need to eat to food");
            Console.WriteLine();
            Console.WriteLine("Enter to go back");
        }

        public int getKeyboardInput()
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                return (int)MenuOption.Menu;
            }

            return (int)MenuOption.HowToPlay;
        }
    }
}