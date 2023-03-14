using System;

namespace MRIServices
{
    public interface IPositivePayFormatter
    {
        string FileName { get; }
        string BankID { get; }
        string AccountNumber { get; }
        CheckBatchInfo GetChecks(DateTime start, DateTime end);
        void WriteCheckRow(CheckInfo checkInfo, bool includePayee);
        void WriteHeader();
        void WriteFooter(CheckBatchInfo info);
    }
}
