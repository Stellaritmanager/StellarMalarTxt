using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Data;
using System.Web;
using Microsoft.Data.SqlClient;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.CodeAnalysis.Elfie.Model;
using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.CodeAnalysis.FlowAnalysis;


namespace StellarBillingSystem.Business
{
    public static class BusinessClassCommon
    {
        public static String AddParameter(String FromDate, String ToDate, String ColName, String DateColumn, String Colvalue)
        {
            String Where = String.Empty;
            if (!FromDate.Equals(String.Empty) || !ToDate.Equals(String.Empty) || !Colvalue.Equals(String.Empty))
            {
                Where = " Where ";
            }

            if (!FromDate.Equals(string.Empty) && !ToDate.Equals(String.Empty))
                Where = Where + String.Format(" {0} between '{1}' and {2} ", DateColumn, FromDate, ToDate);
            else if (!FromDate.Equals(string.Empty))
                Where = Where + String.Format(" {0} = '{1}' ", DateColumn, FromDate);
            else if (!ToDate.Equals(string.Empty))
                Where = Where + String.Format(" {0} = '{1}' ", DateColumn, ToDate);

            if (Where.Contains("between") || Where.Contains("="))
                Where = Where + " and ";

            if (!Colvalue.Equals(string.Empty))
                Where = Where + String.Format(" {0} ='{1}' ", ColName, Colvalue);

            return Where;
        }
        public static DataTable DataTable(DbContext context, string sqlQuery,
                                        params DbParameter[] parameters)
        {


            // Your DbContext
            var dbContext = context;

            ///

            // Your SQL query
            string query = sqlQuery ;


            // Execute raw SQL query and retrieve data into a DataTable
            DataTable dataTable = new DataTable();
            using (var connection = dbContext.Database.GetDbConnection() as SqlConnection)
            {
                if (connection != null)
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dataTable.Load(reader);
                            }
                        }
                    }
                }
            }

            return dataTable;
        }

        public static DataTable DataTableReport(DbContext context, string sqlQuery, string Datecolumn, string Fromdate, string Todate,string GroupBy,
                                       params DbParameter[] parameters)
        {


            // Your DbContext
            var dbContext = context;

            ///

            // Your SQL query
            string query = sqlQuery;


            if (Fromdate!=null&Todate!=null)
            {
                sqlQuery = sqlQuery + "and " + Datecolumn + " between '" + Fromdate + "' and '" + Todate +"' ";
            }
            else if (Fromdate!=null)
            {
                sqlQuery = sqlQuery + "and " + Datecolumn + " >= '"+ Fromdate +"'";

            }
            else if (Todate!=null)
            {
                sqlQuery = sqlQuery + Datecolumn + " <= '" + Todate +"'";
            }
          
           if(GroupBy!= string.Empty)
            {
                sqlQuery =sqlQuery+ GroupBy + "";  
            }

            // Execute raw SQL query and retrieve data into a DataTable
            DataTable dataTable = new DataTable();
            using (var connection = dbContext.Database.GetDbConnection() as SqlConnection)
            {
                if (connection != null)
                {
                    connection.Open();
                            
                        using (var command = new SqlCommand(sqlQuery, connection))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    dataTable.Load(reader);
                                }
                            }
                        }
                    
                }
            }

            return dataTable;
        }




    }
}
