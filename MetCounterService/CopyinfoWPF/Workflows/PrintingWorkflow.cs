using Copyinfo.Other;
using CopyinfoWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyinfoWPF.Workflows
{
    class PrintingWorkflow
    {
        public static void Print(ICollection<MachineRecord> records, bool printOnlyNotPrinted = false)
        {
            var recordsToPrint = SelectDocumentsToPrint(records, printOnlyNotPrinted);
            //var documentsToPrint = recordsToPrint.Select(rec => rec.GetTextToPrint());

            //var result = Printing.InvokePrinting(documentsToPrint, string.Empty);

            //if (result)
            //{
            //    foreach (MachineRecord rec in recordsToPrint)
            //    {
            //        rec.SetPrintedTrue();
            //    }
            //}
        }

        private static IEnumerable<MachineRecord> SelectDocumentsToPrint(IEnumerable<MachineRecord> records, bool printOnlyNotPrinted)
        {
            IEnumerable<MachineRecord> selectedRecords;

            if (printOnlyNotPrinted)
            {
                //selectedRecords = records.Where(allRec => allRec.printed == false && !string.IsNullOrEmpty(allRec.serial_number));
            }
            else
            {
                selectedRecords = records;
            }

            return records
                .Where(allRec => !string.IsNullOrEmpty(allRec.serial_number));
                
        }
    }
}
