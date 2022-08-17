using EarthPressure.Model;
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

        public SaveCommand(EarthPressureModel model)
        {
            _model = model;
        }

        public override void Execute(object? parameter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {

                DataContractSerializer serializer = new DataContractSerializer(typeof(EarthPressureModel));
                XmlWriterSettings settings = new XmlWriterSettings()
                {
                    Indent = true,
                    IndentChars = "\t"
                };

                using(XmlWriter writer = XmlWriter.Create(saveFileDialog.FileName, settings))
                {
                    serializer.WriteObject(writer, _model);
                }
            }
        }
    }
}
