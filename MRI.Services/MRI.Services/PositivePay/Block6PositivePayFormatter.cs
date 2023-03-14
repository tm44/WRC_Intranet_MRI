using System;
using System.Data;
using System.IO;
using System.Text;

namespace MRIServices
{
    public class Block6PositivePayFormatter : BasePositivePayFormatter
    {
        public override string BankID => "700112";

        public override string FileName => "USB" + DateTime.Now.ToString("MMddyy") + "_700112.txt";

        public override string AccountNumber => "157528506379";

        public Block6PositivePayFormatter(StreamWriter writer) : base(writer)
        {
        }

        public override void WriteCheckRow(CheckInfo checkInfo, bool includePayee)
        {
            var s = new StringBuilder();
            s.Append(checkInfo.TransactionCode);
            s.Append(AccountNumber.PadLeft(12, '0'));
            s.Append(checkInfo.CheckNumber.PadLeft(10, '0'));
            s.Append(FormatAmountWithoutDecimals(checkInfo.CheckAmount, 12));
            s.Append(checkInfo.CheckDate.ToString("MMddyyyy"));
            s.Append(" "); // Blank or "V" for Void

            if (includePayee)
                s.Append(checkInfo.Payee.TrimEnd().PadLeft(40, ' '));
            else
                s.Append(String.Empty.PadLeft(40, ' '));

            s.Append(String.Empty.PadLeft(40, ' ')); // Second payee - doesn't exist
            s.Append(String.Empty.PadLeft(20, ' ')); // Filler

            _writer.WriteLine(s.ToString());
        }

        public override void WriteFooter(CheckBatchInfo info)
        {
            var s = new StringBuilder();
            s.Append(TransactionCodes.TotalRecord);
            s.Append(AccountNumber.PadLeft(12, '0'));
            s.Append(info.CheckCount.ToString().PadLeft(10, '0'));
            s.Append(FormatAmountWithoutDecimals(info.BatchTotal, 12));
            s.Append(String.Empty.PadLeft(109, ' ')); // Filler
            _writer.WriteLine(s.ToString());
        }

        public override CheckInfo ParseCheckInfo(DataRow dr)
        {
            var checkInfo = base.ParseCheckInfo(dr);
            checkInfo.TransactionCode = TransactionCodes.DetailRecord;
            return checkInfo;
        }

        public class TransactionCodes
        {
            public static string DetailRecord = "01";
            public static string TotalRecord = "02";
        }
    }
}