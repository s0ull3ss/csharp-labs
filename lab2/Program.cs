using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    class Program
    {
        private static void printHelp()
        {
            Console.WriteLine();
            Console.WriteLine("c - create a shape");
            Console.WriteLine("l - print a list of shapes with their attributes");
            Console.WriteLine("h - help");
            Console.WriteLine("e - exit of program");
        }

        static void mainMenu()
        {
            ConsoleKeyInfo key;
            char keyChar;

            while (true)
            {
                key = Console.ReadKey();
                keyChar = key.KeyChar;

                switch (keyChar)
                {
                    case 'c':
                        {
                            createShapeMenu();
                            break;
                        }
                    case 'l':
                        {
                            break;
                        }
                    case 'h':
                        {
                            printHelp();
                            break;
                        }
                    case 'e':
                        {
                            return;
                        }
                    default:
                        {
                            Console.WriteLine("Wrong input! h - help");
                            break;
                        }
                }
            }
        }

        private static void createShapeMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Choose a shape to create: E - Ellipse, С - Circle, P - polygon, press Q to cancel");
            ConsoleKeyInfo key;
            char keyChar;

            while (true)
            {
                key = Console.ReadKey();
                keyChar = key.KeyChar;

                switch (keyChar)
                {
                    case 'e':
                        {
                            return;
                        }
                    case 'c':
                        {
                            return;
                        }
                    case 'p':
                        {
                            return;
                        }
                    case 'q':
                        {
                            return;
                        }
                    default:
                        {
                            Console.WriteLine("Wrong input!");
                            break;
                        }
                }

            }
        }

        static void Main(string[] args)
        {
            mainMenu();
        }
    }
}
