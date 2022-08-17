using EarthPressure.Model;
using EarthPressure.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EarthPressureCalculator.Commands
{
    public class LoadCommand : CommandBase
    {
        EarthPressureViewModel _viewModel;

        public LoadCommand(EarthPressureViewModel vm)
        {
            _viewModel = vm;
        }

        public override void Execute(object? parameter)
        {
            MessageBox.Show("Loading");
        }
    }
}