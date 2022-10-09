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
using EarthPressureCalculator.Views;

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
                FlowDocument doc = new FlowDocument();
                doc.PagePadding = new System.Windows.Thickness(50);
                doc.ColumnWidth = printDialog.PrintableAreaWidth;
                doc.PageHeight = printDialog.PrintableAreaHeight;
                doc.Blocks.Add(new BlockUIContainer(
                    new PrintView()
                    {
                        DataContext = _vm
                    }));
                
                doc.Name = "FlowDoc";
                IDocumentPaginatorSource pageSource = doc;
                printDialog.PrintDocument(pageSource.DocumentPaginator, "Earth pressure " + _vm.Name);
                //printDialog.PrintVisual((System.Windows.Media.Visual)parameter, "Hello printing");
            }
            
        }
    }
}
