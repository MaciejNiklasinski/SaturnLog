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
    internal interface ISaturn5IssuesDB : ISaturn5IssuesRepository
    {
        void OnSaturn5IssuesSheetLoaded(object sender, Saturn5SpreadsheetLoadedEventArgs e);
    }
}
