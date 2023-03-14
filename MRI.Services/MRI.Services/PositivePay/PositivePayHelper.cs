using MRIServices;
using System.Text;

namespace MRI_Services
{
    public class PositivePayHelper
    {
        private string _entity { get; set; }
        public PositivePayHelper(string entity)
        {
            _entity = entity;
        }

        public byte[] GetPositivePayFile(DateTime start, DateTime end, bool includePayee)
        {

            //Response.AddHeader("content-disposition", "attachment; filename=" + formatter.FileName);

            MemoryStream stream = new MemoryStream();
            using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
            {
                var formatter = GetFormatter(writer);
                formatter.WriteHeader();

                var info = formatter.GetChecks(start, end);

                foreach (var check in info.CheckList)
                {
                    formatter.WriteCheckRow(check, includePayee);
                }
                formatter.WriteFooter(info);

                // writer.Write($"This is the content - {_entity} - {_expensePeriod}");
            }
            return stream.ToArray();
        }

        public string Filename => $"PositivePay_001_{DateTime.Now.ToString()}.txt";

        private IPositivePayFormatter GetFormatter(StreamWriter writer)
        {
            switch (_entity)
            {
                case "001":
                    return new WralpPositivePayFormatter(writer);
                case "111":
                    return new WRCRealEstatePositivePayFormatter(writer);
                case "900":
                    return new Block5PositivePayFormatter(writer);
                case "700":
                    return new Block6PositivePayFormatter(writer);
                case "999":
                    return new Block5TestPositivePayFormatter(writer);
                case "910":
                    return new Block13PositivePayFormatter(writer);
                case "130":
                    return new Block16PositivePayFormatter(writer);
                case "230":
                    return new Block24PositivePayFormatter(writer);
                case "330":
                    return new OEGIXPositivePayFormatter(writer);
                default:
                    return null;
            }
        }
    }
}
