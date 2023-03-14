using System;
using System.Data;
using System.IO;
using System.Text;

namespace MRIServices
{
    public class Block13PositivePayFormatter : Block5PositivePayFormatter
    {
        public override string BankID => "910112";

        public override string AccountNumber => "001416918410";

        public Block13PositivePayFormatter(StreamWriter writer) : base(writer)
        {
        }
    }
}