using EarthPressure.Model;
using EarthPressure.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

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
            OpenFileDialog openFileDialog= new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                using (FileStream fileStream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                {
                    using (XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fileStream, new XmlDictionaryReaderQuotas()))
                    {
                        DataContractSerializer serializer = new DataContractSerializer(typeof(EarthPressureModel));
                        EarthPressureModel model = (EarthPressureModel)serializer.ReadObject(reader, true);
                        _viewModel.SetModel(model);
                    }
                }
            }
        }
    }
}