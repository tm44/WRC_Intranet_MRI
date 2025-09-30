using System.IO;

namespace MRIServices
{
    public class PTOIFrankAlbertFormatter : WralpPositivePayFormatter
    {
        public override string BankID => "111567";

        public override string AccountNumber => "4635830706";

        public PTOIFrankAlbertFormatter(StreamWriter writer) : base(writer)
        {
        }
    }
}