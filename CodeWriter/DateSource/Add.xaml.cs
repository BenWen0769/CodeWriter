using System.Windows;
using CodeWriter.Utils;
using Panuon.UI.Silver;

namespace CodeWriter.DateSource
{
    /// <summary>
    /// Add.xaml 的互動邏輯
    /// </summary>
    public partial class Add : Window
    {
        public Add()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(name.Text))
            {
                MessageBoxX.Show("請輸入名稱", "提示", Application.Current.MainWindow);
                return;
            }
            if (string.IsNullOrEmpty(sqlConn.Text))
            {
                MessageBoxX.Show("請輸入連接字符串", "提示", Application.Current.MainWindow);
                return;
            }
            string sql = $"INSERT INTO datasource VALUES('{name.Text}','{sqlConn.Text}')";
            SqliteHelper.ExcuteSql(sql);
            DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
