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
    // Internal interface - Row Indexes, Sheets and other things domain layer is unaware about - of UserRepository
    internal interface IUserDB : IUserRepository
    {
        event EventHandler<UserSpreadsheetLoadedEventArgs> UserSpreadsheetLoaded;
        event EventHandler<UserSpreadsheetAddedEventArgs> UserSpreadsheetAdded;
        event EventHandler<UserSpreadsheetReplacedEventArgs> UserSpreadsheetReplaced;
        //event EventHandler<UserSpreadsheetArchivedEventArgs> UserSpreadsheetArchived;
        //event EventHandler<UserSpreadsheetUnarchivedEventArgs> UserSpreadsheetUnarchived;
        event EventHandler<UserSpreadsheetRemovedEventArgs> UserSpreadsheetRemoved;

        object UsersDBLock { get; }

        IDictionary<string, int> RowIndexesByUsernames { get; }

        LiveSheet DbSheet { get; }

        void AssureUserDataIsLoaded(string serialNumber);
    }
}
