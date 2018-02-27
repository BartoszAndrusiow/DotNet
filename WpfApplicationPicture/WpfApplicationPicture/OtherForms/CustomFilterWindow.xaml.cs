using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplicationPicture.OtherForms
{
    /// <summary>
    /// Interaction logic for CustomFilterWindow.xaml
    /// </summary>
    public partial class CustomFilterWindow : Window
    {
        String customFilter = "Custom filter";
        String mergeFilter = "Averaging filter";
        String sharpFilter = "SharpFilter";

        public int[,] choseTable;
        public String filterName = "";
        public bool isSelectFilter = false;
        public CustomFilterWindow()
        {
            InitializeComponent();
            ClearFilterTable();
            MergeFilterComboBoxItem.Content = mergeFilter;
            CustomFilterComboBoxItem.Content=customFilter;
            SharpFilterComboBoxItem.Content = sharpFilter;
        }
        /// <summary>
        /// Set filter table
        /// </summary>
        private void ClearFilterTable()
        {
            DataSet date = new DataSet();
            var table = GenerateDataTable();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                table.Rows.Add(table.NewRow());
            }
            for (int i = 0; i < table.Columns.Count; i++)
            {
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    table.Rows[i][j] = 0;
                }
            }
            SetDataTable(date, table);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_date"></param>
        /// <param name="_table"></param>
        private void SetDataTable(DataSet _date, DataTable _table)
        {
            _date.Tables.Add(_table);
            FilterDataGrid.ItemsSource = _date.Tables[0].DefaultView;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectItem = (ComboBoxItem)FilterComboBox.SelectedItem;
           if (selectItem.Content== mergeFilter)
            {
                filterName = mergeFilter;
                this.FillAveTable(WpfApplicationPicture.filterArraySet.AveragingArray.arrayAve);
            }
            if (selectItem.Content == sharpFilter)
            {
                filterName = sharpFilter;
                this.FillAveTable(WpfApplicationPicture.filterArraySet.AveragingArray.arraySharp);
            }
        }
        /// <summary>
        /// Fill Ave
        /// </summary>
        private void FillAveTable(int[,] _tab)
        {
            DataSet date = new DataSet();

            DataTable table = GenerateDataTable();
            for(int i=0;i< _tab.Rank+1;i++)
            {
                table.Rows.Add(table.NewRow());
            }
            for (int i = 0; i < _tab.Rank + 1; i++)
            {
                for (int j = 0; j < _tab.Rank + 1; j++)
                {
                    table.Rows[i][j] = _tab[i,j];//WpfApplicationPicture.filterArraySet.AveragingArray.arrayAve[i,j];
                }
            }
            choseTable = _tab;
            SetDataTable(date, table);
        }

        private static DataTable GenerateDataTable()
        {
            var table=new DataTable("filter");
            table.Columns.Add(new DataColumn("col1"));
            table.Columns.Add(new DataColumn("col2"));
            table.Columns.Add(new DataColumn("col3"));

            return table;
        }

        private void choseButton_Click(object sender, RoutedEventArgs e)
        {
            if(String.IsNullOrEmpty(filterName)==false)
            {
                isSelectFilter = true;
            }
            this.Close();
        }
    }
}
