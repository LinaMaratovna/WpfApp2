using Lib_3;
using LibMas;
using Microsoft.Win32;
using System.Data;
using System.Windows;
namespace WpfApp2
{
   // <summary>
   // Главное окно приложения для работы с двумерным массивом (матрицей).
   
    public partial class MainWindow : Window
    {
        private int[,] mas;

        public MainWindow()
        {
            InitializeComponent();
        }

       // Заполнение матрицы случайными числами.
       
        private void btnFill_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtDlinaCount.Text, out int rows) && rows > 0 &&
                int.TryParse(txtShirinaCount.Text, out int columns) && columns > 0)
            {
                mas = Mas.FillRandom(rows, columns, 0, 10);
                UpdateGrid();
                MessageBox.Show("Матрица успешно заполнена случайными числами!", "Успех");
            }
            else
            {
                MessageBox.Show("Введите корректные положительные числа для длины и ширины матрицы!", "Ошибка");
            }
        }

       // Ручной ввод матрицы через строку.
       // Формат: строки разделяются точкой с запятой, элементы - пробелами или запятыми
       // Пример в txtManualDlinaArray: 1 2 3; 4 5 6; 7 8 9
       
        private void btnManualInput_Click(object sender, RoutedEventArgs e)
        {
            string line1 = txtManualDlinaArray.Text.Trim();
            string line2 = txtManualShirinaArray.Text.Trim();

            if (string.IsNullOrWhiteSpace(line1) || string.IsNullOrWhiteSpace(line2))
            {
                MessageBox.Show("Введите обе строки матрицы!");
                return;
            }

            try
            {
                string[] row1 = line1.Split(new char[] { ' ', ',' },
                    StringSplitOptions.RemoveEmptyEntries);
                string[] row2 = line2.Split(new char[] { ' ', ',' },
                    StringSplitOptions.RemoveEmptyEntries);

                if (row1.Length != row2.Length)
                {
                    MessageBox.Show("Строки должны быть одинаковой длины!");
                    return;
                }
                int cols = row1.Length;
                mas = new int[2, cols];

                for (int j = 0; j < cols; j++)
                {
                    if (!int.TryParse(row1[j], out mas[0, j]) ||
                        !int.TryParse(row2[j], out mas[1, j]))
                    {
                        MessageBox.Show("Все элементы должны быть числами!");
                        return;
                    }
                }
                txtDlinaCount.Text = "2";
                txtShirinaCount.Text = cols.ToString();
                UpdateGrid();
                MessageBox.Show("Матрица успешно введена!", "Успех");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }

       // Расчет суммы элементов матрицы.
       
        private void btnCalc_Click(object sender, RoutedEventArgs e)
        {
            if (mas != null && mas.Length > 0)
            {
                int[] rowSums = Matrix.RowSums(mas);
                int sum = rowSums.Sum();
                Rezult.Text = sum.ToString();
            }
            else
            {
                MessageBox.Show("Сначала заполните матрицу!", "Предупреждение");
            }
        }

       // Сохранение матрицы в файл.
       
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (mas == null || mas.Length == 0)
            {
                MessageBox.Show("Матрица пуста, нечего сохранять.", "Предупреждение");
                return;
            }
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.DefaultExt = "*.txt";
            saveFile.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            saveFile.Title = "Сохранение матрицы";
            saveFile.FileName = "matrix.txt";
            if (saveFile.ShowDialog() == true)
            {
                try
                {
                    Mas.SaveToFile(mas, saveFile.FileName);
                    MessageBox.Show($"Матрица успешно сохранена в файл:\n{saveFile.FileName}", "Успех");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка");
                }
            }
        }

       // Открытие матрицы из файла.
       
        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.DefaultExt = "*.txt";
            openFile.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            openFile.Title = "Открытие матрицы";
            if (openFile.ShowDialog() == true)
            {
                try
                {
                    mas = Mas.OpenFromFile(openFile.FileName);
                    txtDlinaCount.Text = mas.GetLength(0).ToString();
                    txtShirinaCount.Text = mas.GetLength(1).ToString();
                    UpdateGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка чтения файла: {ex.Message}", "Ошибка");
                }
            }
        }

       // Очистка всех полей и таблицы.
       
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtDlinaCount.Clear();
            txtShirinaCount.Clear();
            txtManualDlinaArray.Clear();
            txtManualShirinaArray.Clear();
            Rezult.Clear();
            dataGrid1.ItemsSource = null;
            dataGrid1.Columns.Clear();
            mas = null;
            txtDlinaCount.Focus();
        }

       // Закрытие окна.
       
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
                this.Close();
        }

       // Информация о программе.
       
        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Пракитическая работа 2: 3. Дана матрица размера M × N. Для каждой строки матрицы найти сумму ее элементов. Сделала: Мухутдинова Алина Маратовна");
        }

// Вспомогательный метод для обновления DataGrid.
       
        private void UpdateGrid()
        {
            if (mas == null) return;
            int rows = mas.GetLength(0);
            int cols = mas.GetLength(1);
            DataTable table = new DataTable();
            for (int j = 0; j < cols; j++)
                table.Columns.Add($"Столбец {j + 1}", typeof(int));

            for (int i = 0; i < rows; i++)
            {
                DataRow row = table.NewRow();
                for (int j = 0; j < cols; j++)
                    row[j] = mas[i, j];
                table.Rows.Add(row);
            }
            dataGrid1.ItemsSource = null;
            dataGrid1.Columns.Clear();
            dataGrid1.AutoGenerateColumns = true;
            dataGrid1.ItemsSource = table.DefaultView;///
        }
    }
}