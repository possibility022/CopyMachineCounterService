using CopyinfoWPF.ORM.MetCounterServiceDatabase.Machine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CopyinfoWPF.Workflows
{
    class PrintingWorkflow
    {
        public static void Print(ICollection<Record> records, bool printOnlyNotPrinted = false)
        {
            var recordsToPrint = SelectDocumentsToPrint(records, printOnlyNotPrinted);
            //var documentsToPrint = recordsToPrint.Select(rec => rec.GetTextToPrint());

            //var result = Printing.InvokePrinting(documentsToPrint, string.Empty);

            //if (result)
            //{
            //    foreach (Record rec in recordsToPrint)
            //    {
            //        rec.SetPrintedTrue();
            //    }
            //}
        }

        private static IEnumerable<Record> SelectDocumentsToPrint(IEnumerable<Record> records, bool printOnlyNotPrinted)
        {
            IEnumerable<Record> selectedRecords;

            if (printOnlyNotPrinted)
            {
                //selectedRecords = records.Where(allRec => allRec.printed == false && !string.IsNullOrEmpty(allRec.serial_number));
            }
            else
            {
                selectedRecords = records;
            }

            throw new NotImplementedException();
            //return records
            //    .Where(allRec => !string.IsNullOrEmpty(allRec.serial_number));
                
        }
    }
}
