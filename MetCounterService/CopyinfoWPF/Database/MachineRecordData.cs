using System;
using System.Collections;

using MongoDB.Bson;
using System.Collections.Generic;
using CopyinfoWPF.Common;
using System.ComponentModel;

namespace CopyinfoWPF.Database
{
    internal class MachineRecordData : MachineRecord, IComparable<MachineRecordData>
    {
        
        public int GetTotal()
        {
            return print_counter_black_and_white + print_counter_color;
        }

        public int CompareTo(MachineRecordData other)
        {
            return (datetime.CompareTo(other.datetime));
        }

        public static bool operator <(MachineRecordData e1, MachineRecordData e2)
        {
            return e1.CompareTo(e2) < 0;
        }

        public static bool operator >(MachineRecordData e1, MachineRecordData e2)
        {
            return e1.CompareTo(e2) > 0;
        }
    }
}
