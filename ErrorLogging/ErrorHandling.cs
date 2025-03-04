using System;
using System.Data;
using System.Diagnostics;
using MySQLFactory;
using MySql.Data.MySqlClient;

namespace ErrorLogging
{
    public class ErrorHandling
    {
        public string ConnectionString { get; set; }

        public void LogErrorToEvents(Exception ex)
        {
            var helper = new MySQLDBHelper(ConnectionString);
            var errorMessage = string.Empty;
            var errorStackTrace = string.Empty;

            var mySqlParameters = new MySqlParameter[4];
            mySqlParameters[0] = new MySqlParameter("?iErrorCode", 999);
            mySqlParameters[1] = new MySqlParameter("?sErrorMessage", ex.Message);
            mySqlParameters[2] = new MySqlParameter("?sErrorStackTrace", ex.StackTrace);
            mySqlParameters[3] = new MySqlParameter("?sErrorPage", "TTE Server");

            var dataSet = helper.FillDataSet(CommandType.StoredProcedure, "spi_Error", new string[] { "Tables" }, mySqlParameters);
        }
    }
}