using System;
using System.Data;
using System.IO;
using System.Text;

namespace MRIServices
{
    public class OEGIXPositivePayFormatter : BasePositivePayFormatter
    {
        public override string BankID => "330113";

        public override string FileName => "JPM" + DateTime.Now.ToString("MMddyy") + "_330113.txt";

        public override string AccountNumber => "726639138";

        public OEGIXPositivePayFormatter(StreamWriter writer) : base(writer)
        {
        }

        public override void WriteCheckRow(CheckInfo checkInfo, bool includePayee)
        {
            var s = new StringBuilder();
            s.Append(checkInfo.TransactionCode);
            s.Append(AccountNumber.PadLeft(13, '0'));
            s.Append(checkInfo.CheckNumber.PadLeft(10, '0'));
            s.Append(checkInfo.CheckDate.ToString("MMddyy"));
            s.Append(string.Format("{0:f2}", checkInfo.CheckAmount).PadLeft(11, '0'));
            if (includePayee)
                s.Append(checkInfo.Payee.PadLeft(30,'0'));

            _writer.WriteLine(s.ToString());
        }

        public override CheckInfo ParseCheckInfo(DataRow dr)
        {
            var checkInfo = base.ParseCheckInfo(dr);
            checkInfo.TransactionCode = TransactionCodes.Issue;
            return checkInfo;
        }

        public class TransactionCodes
        {
            public static string Issue = "I";
            public static string Cancel = "C";
            public static string Stop = "S";
            public static string RevokeStop = "R";
        }
    }
}