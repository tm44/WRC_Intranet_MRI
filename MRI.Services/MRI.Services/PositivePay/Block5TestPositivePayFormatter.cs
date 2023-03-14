using System;
using System.Data;
using System.IO;
using System.Text;

namespace MRIServices
{
    public class Block5TestPositivePayFormatter : BasePositivePayFormatter
    {
        public override string BankID => "999113";

        public override string FileName => $"BOA{DateTime.Now.ToString("MMddyy")}_{BankID}.txt";

        public override string AccountNumber => "001416912074";

        public Block5TestPositivePayFormatter(StreamWriter writer) : base(writer)
        {
        }

        public override void WriteCheckRow(CheckInfo checkInfo, bool includePayee)
        {
            var s = new StringBuilder();
            s.Append(AccountNumber); // 12
            s.Append(checkInfo.TransactionCode); // 1
            s.Append(checkInfo.CheckNumber.PadLeft(10, '0')); // 10
            s.Append(string.Format("{0:f2}", checkInfo.CheckAmount).Replace(".", String.Empty).PadLeft(12, '0')); // 12
            s.Append(checkInfo.CheckDate.ToString("MMddyy")); // 6 (or could be 8)
            s.Append(string.Empty.PadLeft(3, ' ')); // 3 (or could be 1, depending on date format)
            s.Append(checkInfo.Payee.PadRight(256,' ')); // 256
            s.Append(string.Empty.PadLeft(16, ' ')); // 16

            _writer.WriteLine(s.ToString());
        }

        public override CheckInfo ParseCheckInfo(DataRow dr)
        {
            var checkInfo = base.ParseCheckInfo(dr);
            checkInfo.TransactionCode = TransactionCodes.Outstanding;
            return checkInfo;
        }

        public class TransactionCodes
        {
            //public static string Issue = "I";
            //public static string Cancel = "C";
            //public static string Stop = "S";
            //public static string RevokeStop = "R";
            public static string Outstanding = "O";
            public static string Void = "V";
        }
    }
}