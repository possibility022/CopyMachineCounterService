using System;
using MongoDB.Bson;
using System.Collections.Generic;
using Repositories.Enums;
using MongoDB.Driver;

namespace Repositories.MongoInterface
{
    public interface IMongoRepository
    {

        bool Connect();

        IEnumerable<MachineRecord> GetAllReports(RecordsCollection collectionType, string serialNumber = null);
        IEnumerable<MachineRecord> GetOtherReports(string serial_number);

        void ReplaceMachineRecrod(MachineRecord r, RecordsCollection recordsCollection);

        MachineRecord GetFirstInMonth(string serial_number, DateTime month);
        MachineRecord GetLatestForDevice(string serial_number);
        HTMLCounter GetHTMLCounter(ObjectId id);
        HTMLSerial GetHTMLSerial(ObjectId id);

        void DeleteMachineRecords(MachineRecord[] r);
        bool DeleteMachineRecord(MachineRecord r);

        bool DeleteHTMLSerial(HTMLSerial html);
        bool DeleteHTMLCounter(HTMLCounter html);

    }
}
