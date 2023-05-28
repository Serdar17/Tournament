namespace Tournament.Calculation.Solver
{
    public class Solver
    {
        public double[] Solve(double[][] mainMatrix, double[] freeMembers)
        {
            LinearEquationsSystem.Fill(mainMatrix, freeMembers);
            return LinearEquationsSystem.Solve();
        }
    }

    public static class LinearEquationsSystem
    {
        private static int Width { get; set; }
        private static int Height { get; set; }
        private static double[,] _matrix;
        private const double Epsilon = 1e-5;

        public static double[] Solve()
        {
            SimplifyMatrix();
            return GetRoots();
            // return DoesSystemHasRoots()
            //     ? GetRoots()
            //     : throw new Exception("There are no solutions.");
        }

        public static void Fill(double[][] mainMatrix, double[] freeMembers)
        {
            Height = mainMatrix.GetLength(0);
            Width = mainMatrix[0].Length + 1;
            _matrix = new double[Height, Width];
            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j <= Width - 1; j++)
                {
                    _matrix[i, j] = j < Width - 1
                        ? mainMatrix[i][j]
                        : freeMembers[i];
                }
            }
        }

        public static void SimplifyMatrix()
        {
            var usedRows = new HashSet<int>();
            for (var j = 0; j < Width; j++)
            {
                for (var i = 0; i < Height; i++)
                {
                    if (Math.Abs(_matrix[i, j]) > Epsilon && !usedRows.Contains(i))
                    {
                        usedRows.Add(i);
                        MultiplyRow(i, 1.0 / _matrix[i, j]);
                        MakeZeroColumns(j, i);
                    }
                }
            }
        }

        private static double[] GetRoots()
        {
            var roots = new double[Width - 1];
            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Width - 1; j++)
                {
                    if (Math.Abs(_matrix[i, j] - 1) < Epsilon)
                        roots[j] = _matrix[i, Width - 1];
                }
            }
            return roots;
        }

        public static void MakeZeroColumns(int j, int currentElementRow)
        {
            for (var k = 0; k < Height; k++)
                if (k != currentElementRow)
                    MultiplyAndAddToAnotherRow(currentElementRow, k, j);
        }

        public static void MultiplyRow(int i, double multiplier)
        {
            for (var j = 0; j < Width; j++)
                _matrix[i, j] *= multiplier;
        }

        public static bool DoesSystemHasRoots()
        {
            for (var i = 0; i < Height; i++)
            {
                var zerosCount = 0;
                for (var j = 0; j < Width - 1; j++)
                    if (Math.Abs(_matrix[i, j]) < Epsilon)
                        zerosCount++;
                if (zerosCount == Width - 1 && Math.Abs(_matrix[i, Width - 1]) > Epsilon)
                    return false;
            }
            return true;
        }

        public static void MultiplyAndAddToAnotherRow(int currentRow, int toAddRow, int currentColumn)
        {
            var multiplier = _matrix[toAddRow, currentColumn] / _matrix[currentRow, currentColumn];
            for (var j = 0; j < Width; j++)
            {
                _matrix[toAddRow, j] -= _matrix[currentRow, j] * multiplier;
                if (Math.Abs(_matrix[toAddRow, j]) < Epsilon)
                    _matrix[toAddRow, j] = 0;
            }
        }
    }
}