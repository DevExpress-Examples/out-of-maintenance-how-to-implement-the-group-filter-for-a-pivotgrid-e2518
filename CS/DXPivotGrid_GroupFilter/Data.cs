using System.Data;
using System.Globalization;
using System.Windows;
using DevExpress.Xpf.PivotGrid;

namespace DXPivotGrid_GroupFilter {
    public partial class MainWindow : Window {
        DataView GetTimeTableView() {
            int[] years = new int[] { 2009, 2010 };

            DataTable table = new DataTable();
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Price", typeof(int));
            table.Columns.Add("Year", typeof(int));
            table.Columns.Add("Month", typeof(int));
            table.Columns.Add("Day", typeof(int));
            for (int month = 1; month <= 12; month++) {
                for (int day = 1; day <= 30; day++) {
                    table.Rows.Add("Name 1", month, 2009, month, day);
                    table.Rows.Add("Name 2", 2 * month, 2009, month, day);
                    table.Rows.Add("Name 1", month + 10, 2010, month, day);
                    table.Rows.Add("Name 2", 2 * month + 10, 2010, month, day);
                }
            }
            return table.DefaultView;
        }
        private void pivotGridControl1_FieldValueDisplayText(object sender,
                PivotFieldDisplayTextEventArgs e) {
            if (e.Field == fieldMonth) {
                e.DisplayText =
                    CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName((int)e.Value);
            }
        }
    }
}