using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace MRIServices
{
    public class WralpPositivePayFormatter : BasePositivePayFormatter
    {
        public override string BankID => "001113";

        public override string FileName => $"ARPW{DateTime.Now.ToString("MMddyy")}_{BankID}.txt";

        public override string AccountNumber => "4311846125";

        public WralpPositivePayFormatter(StreamWriter writer) : base(writer)
        {

        }
        public override void WriteCheckRow(CheckInfo checkInfo, bool includePayee)
        {
            var s = new StringBuilder();
            s.Append(checkInfo.CheckNumber.PadLeft(10, '0'));
            s.Append(checkInfo.CheckDate.ToString("MMddyy"));
            s.Append(checkInfo.AccountNumber);
            s.Append("320");
            s.Append(string.Format("{0:f2}", checkInfo.CheckAmount).Replace(".", String.Empty).PadLeft(10, '0'));
            if (includePayee)
                s.Append(checkInfo.Payee);

            _writer.WriteLine(s.ToString());
        }

        public override void WriteHeader()
        {
            _writer.WriteLine("$$ADD ID=WRUUSRF1 BID='DII4000031518'");
            _writer.WriteLine($"WRRECV        {AccountNumber}");
        }

        public override void WriteFooter(CheckBatchInfo info)
        {
            _writer.WriteLine("              " + info.CheckCount.ToString().PadLeft(5, '0') + "   " + string.Format("{0:f2}", info.BatchTotal).Replace(".", string.Empty).PadLeft(10, '0'));
            _writer.WriteLine(@"\\\\\\");
        }
    }
}