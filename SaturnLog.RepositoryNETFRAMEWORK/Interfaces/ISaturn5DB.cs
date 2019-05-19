using LiveGoogle.Sheets;
using SaturnLog.Core;
using SaturnLog.Repository.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Repository.Interfaces
{
    // Internal interface - Row Indexes, Sheets and other things domain layer is unaware about - of Saturn5Repository
    internal interface ISaturn5DB : ISaturn5Repository
    {
        event EventHandler<Saturn5SpreadsheetLoadedEventArgs> Saturn5SpreadsheetLoaded;
        event EventHandler<Saturn5SpreadsheetAddedEventArgs> Saturn5SpreadsheetAdded;
        event EventHandler<Saturn5SpreadsheetReplacedEventArgs> Saturn5SpreadsheetReplaced;
        event EventHandler<Saturn5SpreadsheetArchivedEventArgs> Saturn5SpreadsheetArchived;
        event EventHandler<Saturn5SpreadsheetUnarchivedEventArgs> Saturn5SpreadsheetUnarchived;
        event EventHandler<Saturn5SpreadsheetRemovedEventArgs> Saturn5SpreadsheetRemoved;

        object Saturns5DBLock { get; }

        IDictionary<string, int> RowIndexesBySerialNumbers { get; }

        IDictionary<string, int> RowIndexesByShortIds { get; }

        LiveSheet DbSheet { get; }

        void AssureSaturn5DataIsLoaded(string serialNumber);
    }
}
