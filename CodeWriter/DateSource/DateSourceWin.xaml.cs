using CodeWriter.DateSource;
using CodeWriter.Utils;
using Panuon.UI.Silver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CodeWriter
{
    /// <summary>
    /// DateSource.xaml 的互動邏輯
    /// </summary>
    public partial class DateSourceWin : Window
    {
        public DateSourceWin()
        {
            InitializeComponent();
            setListData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var addWin = new CodeWriter.DateSource.Add();
            addWin.Owner = this;
            addWin.ShowInTaskbar = false;
            bool? result = addWin.ShowDialog();
            if (result.HasValue && result.Value)
            {
                setListData();
            }
        }

        private void delConn(object sender, RoutedEventArgs e)
        {
            var parms = ((MenuItem)(sender)).CommandParameter;
            if (!string.IsNullOrEmpty(parms.ToString()))
            {
                SqliteHelper.ExcuteSql($"delete from datasource where name='{parms}'");
                setListData();
            }
        }

        private void setListData()
        {
            dataGrid.DataContext = SqliteHelper.QueryTable("Select * from datasource");
        }
    }
}
