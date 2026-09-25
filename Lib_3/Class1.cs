namespace Lib_3
{
    /// <summary>
    /// Класс вычислительных модулей для работы с матрицей.
    /// </summary>
    public class Matrix
    {
        /// <summary>
        /// Вычисляет сумму элементов каждой строки матрицы.
        /// </summary>
        /// <param name="matrix">Исходная матрица M × N</param>
        /// <returns>Массив sums длиной M, где sums[i] — сумма i-й строки</returns>
        /// <exception cref="ArgumentNullException">Если matrix == null</exception>
        /// <exception cref="ArgumentException">Если матрица пустая</exception>
        public static int[] RowSums(int[,] matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            if (rows == 0 || cols == 0)
                throw new ArgumentException("Матрица не должна быть пустой");

            int[] sums = new int[rows];

            for (int i = 0; i < rows; i++)
            {
                int sum = 0;
                for (int j = 0; j < cols; j++)
                    sum += matrix[i, j];
                sums[i] = sum;
            }
            return sums;
        }
    }
}