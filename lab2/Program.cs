using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        static void mainMenu(List<Shape> shapes)
        {
            printHelp();
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
                            createShapeMenu(shapes);
                            break;
                        }
                    case 'l':
                        {
                            printShapes(shapes);
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

        private static void createShapeMenu(List<Shape> shapes)
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
                            createCircle(shapes);
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

        private static void printShapes(List<Shape> shapes)
        {
            Console.WriteLine();
            foreach (Shape item in shapes)
            {
                Console.WriteLine(item.ToString());
            }
        }

        private static void createCircle(List<Shape> shapes)
        {
            Point center;
            Console.WriteLine("Input coordinates of circle's center in a format x y");
            string line = Console.ReadLine();
            Regex regForPoint = new Regex(@"(\d*\.?\d+)\s+(\d*\.?\d+)");
            MatchCollection matches = regForPoint.Matches(line);
            if (matches.Count == 1)
            {
                GroupCollection group1 = matches[0].Groups;
                double x = Convert.ToDouble(group1[1].ToString());
                double y = Convert.ToDouble(group1[2].ToString());
                center = new Point(x, y);
                Console.WriteLine("Input a radius");
                double radius = Convert.ToDouble(Console.ReadLine().ToString());
                shapes.Add(new Circle(center, radius));
            }
            else
            {
                Console.WriteLine("Couldn't create a circle because of a wrong input");
            }
        }

        static void Main(string[] args)
        {
            List<Shape> shapes = new List<Shape>();
            mainMenu(shapes);
        }
    }
}
