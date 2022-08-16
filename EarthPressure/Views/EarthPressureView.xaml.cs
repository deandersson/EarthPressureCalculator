using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using EarthPressure.ViewModel;

namespace EarthPressure.Views
{
    /// <summary>
    /// Interaction logic for EarthPressureView.xaml
    /// </summary>
    public partial class EarthPressureView : UserControl
    {
        public EarthPressureView()
        {
            InitializeComponent();
            this.DataContext = new EarthPressureViewModel();
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            main_grid.Focus();
        }
    }
}
