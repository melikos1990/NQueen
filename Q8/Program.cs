using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace cs_8queen
{
    class Program
    {
        static List<List<int>> result = new List<List<int>>();

        //validation queen rule
        static bool safeCheck(int[,] board, int row, int col, int N)
        {
            int i, j;

            for (i = 0; i < col; i++)
                if (board[row, i] == 1)
                    return false;

            for (i = row, j = col; i >= 0 && j >= 0; i--, j--)
                if (board[i, j] == 1)
                    return false;

            for (i = row, j = col; j >= 0 && i < N; i++, j--)
                if (board[i, j] == 1)
                    return false;

            return true;
        }

        //recursiveto solve N Queen problem
        static bool solveNQ(int[,] board, int col, int N)
        {
            if (col == N)
            {
                List<int> v = new List<int>();
                for (int i = 0; i < N; i++)
                    for (int j = 0; j < N; j++)
                    {
                        if (board[i, j] == 1)
                            v.Add(j + 1);
                    }
                result.Add(v);
                return true;
            }


            bool res = false;
            for (int i = 0; i < N; i++)
            {
                if (safeCheck(board, i, col, N))
                {
                    board[i, col] = 1;

                    res = solveNQ(board, col + 1, N) || res;

                    board[i, col] = 0;
                }
            }

            return res;
        }

        /* Check Queen Positions.*/
        static List<List<int>> solveNQ(int n)
        {
            result.Clear();
            int[,] board = new int[n, n];

            solveNQ(board, 0, n);
            return result;
        }

        public static void Main()
        {
            Console.WriteLine("Queen Count:");
            int n = Convert.ToInt32(Console.ReadLine());

            List<List<int>> res = solveNQ(n);
            new Program().Print(res);
        }

        //Print
        public void Print(List<List<int>> queenLocations, char emptySpot = '.', char queenSpot = 'Q')
        {
            int size = queenLocations.First().Count();

            if (size == 0) Console.WriteLine("no solution");

            char[]? emptyLine = Enumerable.Range(0, size).Select(x => emptySpot).ToArray();

            List<string>? lines = queenLocations.Select(f =>
            {
                string newLine = "";
                foreach (var x in f)
                {
                    newLine += new string(emptyLine, 0, x)
                    + queenSpot
                    + new string(emptyLine, 0, (size - x - 1) < 0 ? 0 : (size - x - 1)) + "\r\n";
                }

                return newLine;
            }).ToList();

            Console.WriteLine(string.Join(Environment.NewLine, lines));
        }
    }
}
