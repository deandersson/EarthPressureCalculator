using EarthPressure.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Printing;
using System.Windows.Documents;
using System.Windows.Controls;

using EarthPressure.Components;

namespace EarthPressureCalculator.Commands
{
    public class PrintCommand : CommandBase
    {
        EarthPressureViewModel _vm;

        public PrintCommand(EarthPressureViewModel vm)
        {
            _vm = vm;
        }

        public override void Execute(object? parameter)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.UserPageRangeEnabled = false;

            if (printDialog.ShowDialog() == true)
            {
                //FlowDocument doc = new FlowDocument(new Paragraph(new Run("Hello world")));
                //doc.Name = "FlowDoc";
                //IDocumentPaginatorSource pageSource = doc;
                //printDialog.PrintDocument(pageSource.DocumentPaginator, "Hello printing");
                printDialog.PrintVisual((System.Windows.Media.Visual)parameter, "Hello printing");
            }
            
        }
    }
}
