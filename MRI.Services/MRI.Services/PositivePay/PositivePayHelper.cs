using MRIServices;
using System.Text;

namespace MRI_Services
{
    public class PositivePayHelper
    {
        private string _entity { get; set; }
        private IPositivePayFormatter _formatter;
        public PositivePayHelper(string entity)
        {
            _entity = entity;
        }

        public byte[] GetPositivePayFile(DateTime start, DateTime end, bool includePayee)
        {
            Encoding utf8WithoutBom = new UTF8Encoding(false);
            MemoryStream stream = new MemoryStream();
            using (StreamWriter writer = new StreamWriter(stream, utf8WithoutBom))
            {
                _formatter = GetFormatter(writer);
                _formatter.WriteHeader();

                var info = _formatter.GetChecks(start, end);

                foreach (var check in info.CheckList)
                {
                    _formatter.WriteCheckRow(check, includePayee);
                }
                _formatter.WriteFooter(info);
            }
            return stream.ToArray();
        }

        public string Filename => _formatter.FileName;

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
