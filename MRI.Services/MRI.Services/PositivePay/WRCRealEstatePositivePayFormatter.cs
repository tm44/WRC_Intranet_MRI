using System.IO;

namespace MRIServices
{
    public class WRCRealEstatePositivePayFormatter : WralpPositivePayFormatter
    {
        public override string BankID => "111113";

        public override string AccountNumber => "4943276618";

        public WRCRealEstatePositivePayFormatter(StreamWriter writer) : base(writer)
        {
        }
    }
}