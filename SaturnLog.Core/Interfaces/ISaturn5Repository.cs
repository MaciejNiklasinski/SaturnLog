using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core
{
    public interface ISaturn5Repository : IDisposable
    {
        // Archive
        void Archive(string serialNumber, string archivisationReason, string archivedByUsername);

        // Create or Unarchive
        void Unarchive(string serialNumber, string shortId);
        
        void Create(string serialNumber, string shortId, Saturn5Status status, string phoneNumber, string lastSeenDate, string lastSeenTime, string lastSeenUsername);

        // Read
        int Count();

        bool HasArchivedSaturn5(string serialNumber);

        bool HasSaturn5SerialNumber(string serialNumber);

        bool HasSaturn5ShortId(string shortId);
        
        IEnumerable<string> GetAllSerialNumbers();

        IEnumerable<string> GetAllShortIds();

        string GetSerialNumberFromShortId(string shortId);

        string GetShortIdFromSerialNumber(string serialNumber);

        // Get phone number of the existing saturn5
        string GetSaturn5PhoneNumber(string serialNumber);

        // Get Saturn5Status of the existing saturn5
        Saturn5Status GetSaturn5Status(string serialNumber);

        // Get last seen username of the existing saturn5
        string GetSaturn5LastSeenUsername(string serialNumber);

        // Get last seen date of the existing saturn5
        string GetSaturn5LastSeenDate(string serialNumber);

        // Get plast seen time of the existing saturn5
        string GetSaturn5LastSeenTime(string serialNumber);

        // Returns currently assigned saturn5 log spreadsheet id of the existing saturn5
        string GetSaturn5LogSpreadsheetId(string serialNumber);

        Saturn5 Read(string serialNumber);

        IList<Saturn5> ReadAll();

        // Update
        void AddSaturn5Log(string serialNumber, string saturn5Log);

        void Update(string serialNumber, string shortId = null, Saturn5Status? status = null, string phoneNumber = null, string lastSeenDate = null, string lastSeenTime = null, string lastSeenUsername = null);

        // Delete
        void Delete(string serialNumber);
    }
}
