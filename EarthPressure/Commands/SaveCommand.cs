using EarthPressure.Model;
using EarthPressure.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace EarthPressureCalculator.Commands
{
    public class SaveCommand : CommandBase
    {
        EarthPressureModel _model;
        EarthPressureViewModel _vm;
        bool _createNewSave;

        public SaveCommand(EarthPressureViewModel vm, EarthPressureModel model, bool createNewSave)
        {
            _model = model;
            _vm = vm;
            _createNewSave = createNewSave;
        }

        public override void Execute(object? parameter)
        {
            string? fileName = _vm.FileName;
            if(_createNewSave || fileName == null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Dec7 files (*.dec7)|*.dec7|All files (*.*)|*.*";
                saveFileDialog.DefaultExt = "dec7";
                if (saveFileDialog.ShowDialog() == true)
                {
                    fileName = saveFileDialog.FileName;
                    
                }
            }

            if(fileName != null)
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(EarthPressureModel));
                XmlWriterSettings settings = new XmlWriterSettings()
                {
                    Indent = true,
                    IndentChars = "\t"
                };

                using (XmlWriter writer = XmlWriter.Create(fileName, settings))
                {
                    serializer.WriteObject(writer, _model);
                }
                _vm.IsSaved = true;
                _vm.FileName = fileName;
            }
        }
    }
}
