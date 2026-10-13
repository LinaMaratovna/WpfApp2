using System.IO;
namespace LibMas
{
    /// <summary>
    /// Класс базовых операций с двумерным массивом (матрицей).
    /// </summary>
    public class Mas
    {
        /// <summary>
        /// Заполнение матрицы случайными числами.
        /// </summary>
        /// <param name="rows">Количество строк</param>
        /// <param name="cols">Количество столбцов</param>
        /// <param name="min">Минимальное значение</param>
        /// <param name="max">Максимальное значение</param>
        /// <returns>Заполненная матрица</returns>
        public static int[,] FillRandom(int rows, int cols, int min, int max)
        {
            if (rows <= 0 || cols <= 0)
                throw new ArgumentException("Размеры должны быть положительными");

            int[,] matrix = new int[rows, cols];
            Random rnd = new Random();
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    matrix[i, j] = rnd.Next(min, max + 1);
            return matrix;
        }
        /// <summary>
        /// Сохранение матрицы в текстовый файл.
        /// Формат: первая строка — размеры, далее — элементы.
        /// </summary>
        public static void SaveToFile(int[,] matrix, string path)
        {
            if (matrix == null) throw new ArgumentNullException(nameof(matrix));

            using (StreamWriter sw = new StreamWriter(path))
            {
                int rows = matrix.GetLength(0);
                int cols = matrix.GetLength(1);
                sw.WriteLine($"{rows} {cols}");
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                        sw.Write(matrix[i, j] + (j < cols - 1 ? " " : ""));
                    sw.WriteLine();
                }
            }
        }
        /// <summary>
        /// Чтение матрицы из текстового файла.
        /// </summary>
        public static int[,] OpenFromFile(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string[] sizeLine = sr.ReadLine().Split(' ');
                int rows = int.Parse(sizeLine[0]);
                int cols = int.Parse(sizeLine[1]);
                int[,] matrix = new int[rows, cols];

                for (int i = 0; i < rows; i++)
                {
                    string[] line = sr.ReadLine().Split(new[] { ' ' },
                        StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < cols; j++)
                        matrix[i, j] = int.Parse(line[j]);
                }
                return matrix;
            }
        }
        /// <summary>
        /// Очистка матрицы (обнуление всех элементов).
        /// </summary>
        public static void Clear(int[,] matrix)
        {
            if (matrix == null) return;
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    matrix[i, j] = 0;
        }
        /// <summary>
        /// Преобразование матрицы в строку для вывода.
        /// </summary>
        public static string Print(int[,] matrix)
        {
            if (matrix == null) return "";
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            string result = "";
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                    result += matrix[i, j].ToString().PadLeft(6);
                result += Environment.NewLine;
            }
            return result;//лфыовлофывдо
        }
    }
}