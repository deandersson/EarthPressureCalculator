using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        }



        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            main_grid.Focus();
        }

        
    }
}
