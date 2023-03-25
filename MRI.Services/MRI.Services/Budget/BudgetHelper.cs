using MRI.Services.ART;
using System.Data;
using System.Data.SqlClient;

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
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "WRC_InsertBudget";

                cmd.Parameters.Add(new SqlParameter("Period", record.Period));
                cmd.Parameters.Add(new SqlParameter("EntityID", record.EntityID));
                cmd.Parameters.Add(new SqlParameter("Department", record.Department));
                cmd.Parameters.Add(new SqlParameter("AccountNumber", record.AccountNumber));
                cmd.Parameters.Add(new SqlParameter("Basis", record.Basis));
                cmd.Parameters.Add(new SqlParameter("BudgetType", record.BudgetType));
                cmd.Parameters.Add(new SqlParameter("Activity", record.Activity));
                cmd.Parameters.Add(new SqlParameter("LastDate", record.LastDate));
                cmd.Parameters.Add(new SqlParameter("UserID", record.UserID));

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
