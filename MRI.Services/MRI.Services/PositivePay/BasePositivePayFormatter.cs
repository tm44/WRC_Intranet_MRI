using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace MRIServices
{
    public class BasePositivePayFormatter : IPositivePayFormatter
    {
        protected StreamWriter _writer;
        public virtual string FileName => throw new NotImplementedException();

        public virtual string BankID => throw new NotImplementedException();
        public virtual string AccountNumber => String.Empty;

        protected string FormatAmountWithoutDecimals(decimal amount, int totalFieldLength) => string.Format("{0:f2}", amount).Replace(".", "").PadLeft(totalFieldLength, '0');

        public BasePositivePayFormatter(StreamWriter writer)
        {
            _writer = writer;
        }


        public DataSet GetRecords(DateTime startDate, DateTime endDate)
        {
            // TODO: Figure out how to get around this
            var connectionString = "Data Source=SQL;Initial Catalog=mriwrc;Integrated Security=SSPI;";
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "WRC_GetChecks";
                cmd.Parameters.Add(new SqlParameter("StartDate", startDate));
                cmd.Parameters.Add(new SqlParameter("EndDate", endDate));
                cmd.Parameters.Add(new SqlParameter("BankID", BankID));
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            return ds;
        }

        public virtual void WriteCheckRow(CheckInfo checkInfo, bool includePayee)
        {
            throw new NotImplementedException();
        }

        public CheckBatchInfo GetChecks(DateTime start, DateTime end)
        {
            var ds = GetRecords(start, end);

            var batch = new CheckBatchInfo();

            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    batch.CheckList.Add(ParseCheckInfo(row));
                }
            }
            return batch;
        }

        public virtual CheckInfo ParseCheckInfo(DataRow dr)
        {
            var ci = new CheckInfo();
            ci.AccountNumber = AccountNumber;
            ci.CheckAmount = Decimal.Parse(dr["CheckAmount"].ToString());
            ci.CheckDate = DateTime.Parse(dr["CheckDate"].ToString());
            ci.CheckNumber = dr["CheckNumber"].ToString();
            ci.Payee = dr["Payee"].ToString();
            return ci;
        }


        public virtual void WriteFooter(CheckBatchInfo info)
        {
        }

        public virtual void WriteHeader()
        {
        }
    }
}