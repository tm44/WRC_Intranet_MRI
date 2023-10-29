using MRI.Services.ART;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace MRI.Services.Budget
{
    public class BudgetHelper
    {
        public void InsertBudgetRow(BudgetRecord record)
        {
            using (SqlConnection connection = new SqlConnection(ConfigHelper.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "WRC_InsertBudget";

                cmd.Parameters.Add(new SqlParameter("Period", record.Period));
                cmd.Parameters.Add(new SqlParameter("EntityID", record.EntityID));
                cmd.Parameters.Add(new SqlParameter("Department", "@"));
                cmd.Parameters.Add(new SqlParameter("AccoutNumber", record.AccountNumber)); // Accidental misspelling when I first created the sproc
                cmd.Parameters.Add(new SqlParameter("Basis", "A"));
                cmd.Parameters.Add(new SqlParameter("BudgetType", "STD"));
                cmd.Parameters.Add(new SqlParameter("Activity", record.Activity));
                cmd.Parameters.Add(new SqlParameter("LastDate", DateTime.Now));
                cmd.Parameters.Add(new SqlParameter("UserID", "SYSADM"));

                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteBudgetRecord(string entity, string period)
        {
            using (SqlConnection connection = new SqlConnection(ConfigHelper.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "WRC_DeleteBudget";

                cmd.Parameters.Add(new SqlParameter("Period", period));
                cmd.Parameters.Add(new SqlParameter("EntityID", entity));

                cmd.ExecuteNonQuery();
            }
        }
    }
}
