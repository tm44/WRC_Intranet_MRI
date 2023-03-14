using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace MRI.Services.ART
{
    public class ArtHelper
    {
        private const string connectionString = "Data Source=SQL;Initial Catalog=mriwrc;Integrated Security=SSPI;";
        public List<UnbilledRecord> GetUnbilledRecords(string expensePeriod, string entityId)
        {
            var l = new List<UnbilledRecord>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                if (entityId == Constants.Entities.WRC_RealEstate)
                    cmd.CommandText = "WRC_GetUnbilledRecords_111";
                else
                    cmd.CommandText = "WRC_GetUnbilledRecords";

                cmd.Parameters.Add(new SqlParameter("ExpensePeriod", expensePeriod));
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var record = new UnbilledRecord()
                        {
                            Amount = reader.GetDecimal("Amount"),
                            CheckDate = reader.GetString("CheckDate"),
                            CheckNumber = reader.GetInt32("CheckNumber"),
                            Description = reader.GetString("Description"),
                            Entity = reader.GetInt32("Entity"),
                            Invoice = reader.GetString("Invoice"),
                            Item = reader.GetString("Item"),
                            UserID = reader.GetString("UserID"),
                            Vendor = reader.GetString("Vendor")
                        };
                        l.Add(record);
                    }
                }
            }

            var u1 = new UnbilledRecord()
            {
                Amount = 123.45M,
                CheckDate = "1/2/2023",
                CheckNumber = 1234,
                Description = "A description",
                Entity = 001,
                Invoice = "INV001",
                Item = "Item 1",
                UserID = "MIKE",
                Vendor = "Vendor 1"
            };
            var u2 = new UnbilledRecord()
            {
                Amount = 54.32M,
                CheckDate = "2/13/2023",
                CheckNumber = 1235,
                Description = "Another description",
                Entity = 001,
                Invoice = "INV002",
                Item = "Item 2",
                UserID = "JAIYA",
                Vendor = "Vendor 2"
            };
            l.Add(u1);
            l.Add(u2);

            return l;
        }

        public ArtRunResult RunArt(DateTime invoiceDate, string expensePeriod, string entity)
        {
            var r = new ArtRunResult();
            r.RowsCreated = 2;
            r.BatchID = "246810";
            return r;

            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;

                if (entity == Constants.Entities.WRC_RealEstate)
                    cmd.CommandText = "WRC_RunArt_111";
                else
                    cmd.CommandText = "WRC_RunArt";

                cmd.Parameters.Add(new SqlParameter("ExpensePeriod", expensePeriod));
                cmd.Parameters.Add(new SqlParameter("InvoiceDate", invoiceDate));
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }

            var result = new ArtRunResult();
            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result.BatchID = ds.Tables[0].Rows[0]["BatchID"].ToString();
                result.RowsCreated = Convert.ToInt32(ds.Tables[0].Rows[0]["RowsCreated"]);
            }
            return result;
        }
    }

    public class UnbilledRecord
    {
        public int Entity { get; set; }
        public string Vendor { get; set; }
        public string Invoice { get; set; }
        public string Item { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public int CheckNumber { get; set; }
        public string CheckDate { get; set; }
        public string UserID  { get; set; }
    }

    public class ArtRunResult
    {
        public int RowsCreated { get; set; }
        public string BatchID { get; set; }
    }

    public class RunArtRequestData
    {
        public string InvoiceDate { get; set; }
        public string ExpensePeriod { get; set; }
        public string Entity { get; set; }
    }

    public static class Constants
    {
        public class Entities
        {
            public static readonly string WRALP = "001";
            public static readonly string WRC_RealEstate = "111";
        }
    }
}
