using System;
using System.Threading;

namespace KnightTour
{
    class Menu
    {
        private static int N = 7;

        private static int getSize()
        {
            Console.Clear();
            Console.Write("Enter board size NxN (N > 5 & N <= 10) : ");
            int num;
            try
            {
                num = Convert.ToInt32(Console.ReadLine());
                if (num < 6 || num > 10)
                {
                    Console.WriteLine("You have entered a number that is less than 6 or higher than 10");
                    Thread.Sleep(1000);
                    return getSize();
                }
                else
                    return num;
            }
            catch (NotFiniteNumberException ex)
            {
                Console.WriteLine("You entered invalid symbol!");
                Thread.Sleep(1000);
                return getSize();
            }
        }

        internal static int[,] GetFieldOptions()
        {
            int[,] field;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            N = getSize();
            return field = new int[N, N];

        }

        internal static void Show(int[,] field)
        {
            bool end = true;
            int num = 1;

            while (end)
            {
                Console.Clear();

                Console.WriteLine("\n\n");
                for (int row = 0; row < field.GetLength(0); row++)
                {
                    Console.Write("\t");
                    for (int col = 0; col < field.GetLength(1); col++)
                    {
                        if (field[row, col] <= num)
                        {
                            if (field[row, col] == 1 || field[row, col] == field.Length)
                                Console.ForegroundColor = ConsoleColor.Red;
                            else if (field[row, col] < 10)
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                            else if (field[row, col] < 20)
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                            else if (field[row, col] < 30)
                                Console.ForegroundColor = ConsoleColor.Yellow;
                            else if (field[row, col] < 40)
                                Console.ForegroundColor = ConsoleColor.Cyan;
                            else if (field[row, col] < 50)
                                Console.ForegroundColor = ConsoleColor.Magenta;
                            else if (field[row, col] < 60)
                                Console.ForegroundColor = ConsoleColor.Blue;
                            else if (field[row, col] < 70)
                                Console.ForegroundColor = ConsoleColor.White;
                            else if (field[row, col] < 80)
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                            else if (field[row, col] < 90)
                                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            else if (field[row, col] < 100)
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                            else
                                Console.ForegroundColor = ConsoleColor.Black;

                            if (field[row, col] < 10)
                                Console.Write("  " + field[row, col] + " ");
                            else if (field[row, col] < 100)
                                Console.Write(" " + field[row, col] + " ");
                            else
                                Console.Write(field[row, col] + " ");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("  0 ");
                        }
                        if (num == field.Length + 1)
                            end = false;

                    }
                    Console.WriteLine("\n");
                }
                num++;
                Thread.Sleep(500);
            }
            Console.ReadKey(true);
        }

    }
}
