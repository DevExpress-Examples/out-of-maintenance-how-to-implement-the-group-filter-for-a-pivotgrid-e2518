using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.PivotGrid;

namespace DXPivotGrid_GroupFilter {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e) {
            this.pivotGridControl1.DataSource = GetTimeTableView();
            fieldMonth.CollapseAll();
        }
        private void RadioButtonList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (pivotGridControl1 == null) return;
            this.pivotGridControl1.BeginUpdate();
            PivotGridGroup group = this.pivotGridControl1.Groups[0];
            group.FilterValues.Reset();
            group.FilterValues.BeginUpdate();
            switch (radioGroup1.SelectedIndex) {
                case 0:
                    group.FilterValues.FilterType = FieldFilterType.Excluded;
                    break;
                case 1:
                    group.FilterValues.FilterType = FieldFilterType.Included;
                    group.FilterValues.Values.Add(2009).ChildValues.Add(12);
                    group.FilterValues.Values.Add(2010).ChildValues.Add(1);
                    break;
                case 2:
                    group.FilterValues.FilterType = FieldFilterType.Excluded;
                    group.FilterValues.Values.Add(2009);
                    break;
                case 3:
                    group.FilterValues.FilterType = FieldFilterType.Included;
                    SelectFirstDays(group);
                    break;
            }
            group.FilterValues.EndUpdate();
            this.pivotGridControl1.EndUpdate();
            if (radioGroup1.SelectedIndex == 3)
                fieldMonth.ExpandAll();
            else
                fieldMonth.CollapseAll();
        }
        void SelectFirstDays(PivotGridGroup group) {
            foreach (object year in group.GetUniqueValues(null)) {
                DevExpress.XtraPivotGrid.PivotGroupFilterValue value = 
                    group.FilterValues.Values.Add(year);
                foreach (object month in group.GetUniqueValues(new object[] { year })) {
                    value.ChildValues.Add(month).ChildValues.Add(1);
                }
            }
        }
    }
}
