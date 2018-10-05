using CopyinfoWPF.Interfaces;
using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine;
using System.Collections.Generic;
using System.Linq;

namespace CopyinfoWPF.Workflows.Printing
{
    static class PrintingWorkflow
    {

        public static IPrintContentProvider PrinterContentProvider { get; }

        public static void Print(ICollection<Record> records, bool printOnlyNotPrinted = false)
        {
            var recordsToPrint = printOnlyNotPrinted ? records : records.Where(rec => rec.Printed == false);
            var documentsToPrint = PrinterContentProvider.GetTextToPrint(records);

            var result = Common.Printing.InvokePrinting(documentsToPrint, string.Empty);

            if (result)
            {
                foreach (Record rec in recordsToPrint)
                {
                    //rec.SetPrintedTrue(); //todo implement this in an other layer
                }
            }
        }
    }
}
