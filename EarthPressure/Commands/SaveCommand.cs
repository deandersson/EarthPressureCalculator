using EarthPressure.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EarthPressureCalculator.Commands
{
    public class SaveCommand : CommandBase
    {
        EarthPressureModel _model;

        public SaveCommand(EarthPressureModel model)
        {
            _model = model;
        }

        public override void Execute(object? parameter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                MessageBox.Show(saveFileDialog.FileName);
            }
        }
    }
}
