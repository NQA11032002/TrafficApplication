using LiveCharts;
using LiveCharts.Wpf;
using QuanLyShop.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace giaothong.ViewModel
{
    class HomeViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public ICommand LogoutCommand { get; set; }
        public ICommand OpenTeacherCommand { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<string, string> Values { get; set; }

        public HomeViewModel()
        {
            LogoutCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            OpenTeacherCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Hide();
                TeacherWindow teacher = new TeacherWindow();
                teacher.ShowDialog();
                p.ShowDialog();
            });

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Venta 2021",
                    Values = new ChartValues<double>() {20, 15, 30 ,25, 6, 7, 2, 3 ,5 , 9 ,11 ,12}
                }
            };

            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Venta 2021",
                Values = new ChartValues<double>() { 22, 9, 45, 88 }
            });

            SeriesCollection[1].Values.Add(48d);
            Labels = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };

        }
    }
}
