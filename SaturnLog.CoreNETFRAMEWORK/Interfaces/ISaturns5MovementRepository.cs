using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core
{
    public interface ISaturns5MovementRepository : IDisposable
    {
        // Create
        void AddSendToITFaultedAndDamagedLog(string serialNumber, string consignmentNumber, string incidentNumber, string sentBy, string damagedBy, string faultReportedBy, string damageDescription, string movementNote);

        void AddSendToITOnlyDamagedLog(string serialNumber, string consignmentNumber, string incidentNumber, string sentBy, string damagedBy, string damageDescription, string movementNote);

        void AddSendToITOnlyFaultedLog(string serialNumber, string consignmentNumber, string incidentNumber, string sentBy, string faultReportedBy, string faultDescription, string movementNote);

        void AddSendToITFullyFunctionalSurplusLog(string serialNumber, string consignmentNumber, string incidentNumber, string sentBy, string noIssueSentDescription, string movementNote);

        void AddReceiveFromITLog(string serialNumber, string consignmentNumber, string receivedBy, string noIssueReceivedDescription, string movementNote);
    }
}
