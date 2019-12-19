using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightTour
{
    class Start
    {
        private const int MAX_NUM_OF_MOVES = 8;
        private static int[,] field;
        private static int[,] optionsMoves = { { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 },
                                                { 1, 2 }, { 1, -2 }, { -1, 2 }, { -1, -2 } };
        private static int currentX;
        private static int currentY;
        private static int count;
        private static Random rand = new Random();
        static int Num = 1000;

        internal static void Run()
        {
            field = Menu.GetFieldOptions();
            count = 1;

            int startY = rand.Next(field.GetLength(0));
            int startX = rand.Next(field.GetLength(1));
            currentX = startX;
            currentY = startY;

            while (true)
            {
                field[currentY, currentX] = count;

                if (count == field.Length)
                {
                    ChangeByEulerMethod(count);
                    break;
                }
                int i = GetRightMove();

                if (i == 9)
                {
                    FillEmptyCells();
                    ChangeByEulerMethod(count, false);
                    break;
                }

                currentY += optionsMoves[i, 0];
                currentX += optionsMoves[i, 1];
                count++;
            }

            Menu.Show(field);

        }

        #region Euler Method

        static void ChangeByEulerMethod(int endNum, bool isEnded = true)
        {
            int num = 0;
            if (!isEnded)
            {
                for (int k = Num; k >= 1000; k--)
                {
                    num = GetTheRightPoint(k, endNum);
                    ChangeLastPaths(num, endNum);
                    ChangeNum(k, ++endNum);
                }
                if (num == -1)
                    return;
                ChangeLastPaths(num, endNum);
            }

            num = GetTheRightPoint(1, endNum);
            if (num == -1)
                return;

            ChangeLastPaths(num, endNum);

        }

        static void ChangeNum(int num, int modifiedNum)
        {
            for (int i = 0; i < field.GetLength(0); i++)
                for (int j = 0; j < field.GetLength(1); j++)
                    if (field[i, j] == num)
                        field[i, j] = modifiedNum;
        }

        static void FillEmptyCells()
        {
            for (int i = 0; i < field.GetLength(0); i++)
                for (int j = 0; j < field.GetLength(1); j++)
                    if (field[i, j] == 0)
                        field[i, j] = Num++;

            Num--;
        }

        static int GetTheRightPoint(int startPoint, int finalPoint)
        {
            List<int> aroundTheStartPoint = GetTheNearestPoints(startPoint);
            List<int> aroundTheFinalPoint = GetTheNearestPoints(finalPoint);
            aroundTheFinalPoint.Sort();
            aroundTheStartPoint.Sort();
            int num = FindingASuitablePoint(aroundTheStartPoint, aroundTheFinalPoint);

            if (finalPoint == field.Length && num == finalPoint)
                return -1;

            return num;
        }

        private static List<int> GetTheNearestPoints(int num)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < field.GetLength(0); i++)
                for (int j = 0; j < field.GetLength(1); j++)
                    if (field[i, j] == num)
                        for (int k = 0; k < optionsMoves.GetLength(0); k++)
                            if (isNotNull((i + optionsMoves[k, 0]), (j + optionsMoves[k, 1])))
                                list.Add(field[i + optionsMoves[k, 0], j + optionsMoves[k, 1]]);

            return list;
        }

        private static int FindingASuitablePoint(List<int> firstNeighbors, List<int> secondNeighbors)
        {
            for (int i = firstNeighbors.Count - 1; i >= 0; i--)
                for (int j = secondNeighbors.Count - 1; j >= 0; j--)
                {
                    if (firstNeighbors[i] == secondNeighbors[j]
                        || firstNeighbors[i] == (secondNeighbors[j] + 1)
                        || firstNeighbors[i] == (secondNeighbors[j] - 1))
                    {
                        return firstNeighbors[i];
                    }
                }
            return 0;
        }

        private static void ChangeLastPaths(int num, int maxNum)
        {
            for (int i = 0; i < field.GetLength(0); i++)
                for (int j = 0; j < field.GetLength(1); j++)
                    if (field[i, j] >= num && field[i, j] < Num)
                    {
                        int k = field[i, j] - num;
                        field[i, j] = maxNum - k;

                    }
        }

        #endregion

        #region The Warnsdorf’s rule
        //Method for find 
        static int GetRightMove()
        {
            int[] arr = new int[MAX_NUM_OF_MOVES];

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = CountOfNextVariation(currentY + optionsMoves[i, 0], currentX + optionsMoves[i, 1]);
            }

            int min = MAX_NUM_OF_MOVES + 1;
            for (int j = 0; j < arr.Length; j++)
            {
                if (arr[j] != 0 && arr[j] < min)
                    min = arr[j];
            }

            if (min == MAX_NUM_OF_MOVES + 1)
                min = 0;

            List<int> rightMoves = new List<int>();

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == min)
                    rightMoves.Add(i);
            }
            if (rightMoves.Count > 0)
                return rightMoves[0];
            else
                return MAX_NUM_OF_MOVES + 1;
        }

        //Method for counting the number of possible paths
        static int CountOfNextVariation(int y, int x)
        {
            int counter = 0;

            if (isNotNullorNotFilled(y, x))
            {
                for (int i = 0; i < optionsMoves.GetLength(0); i++)
                {
                    if (isNotNullorNotFilled((y + optionsMoves[i, 0]), (x + optionsMoves[i, 1])))
                        counter++;
                }
                return counter;
            }

            return MAX_NUM_OF_MOVES + 1;
        }

        static bool isNotNullorNotFilled(int y, int x)
        {

            if (x >= 0 && y >= 0 && y < field.GetLength(0) && x < field.GetLength(1) && field[y, x] == 0)
                return true;

            return false;
        }

        static bool isNotNull(int y, int x)
        {

            if (x >= 0 && y >= 0 && y < field.GetLength(0) && x < field.GetLength(1))
                return true;

            return false;
        }

        #endregion

    }
}
