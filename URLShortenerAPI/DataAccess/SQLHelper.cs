using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace URLShortenerAPI.DataAccess
{
    internal class SQLHelper : ISQLHelper
    {
        private TelemetryClient telemetry = new TelemetryClient();

        /// <summary>
        /// Generic call Stored Procedure call to SQL Database
        /// Could be decoupled into own DLL if need be
        /// String Builder Parameters could be method parameters
        /// Stored procedure is always expected to return JSON response
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns>Stored procedure response</returns>
        public string GetData(string storedProcedure, Dictionary<string, string> parameters)
        {
            var dataSource = Environment.GetEnvironmentVariable("SQLDataSource", EnvironmentVariableTarget.Process);
            var userID = Environment.GetEnvironmentVariable("SQLUserID", EnvironmentVariableTarget.Process);
            var initialCatalog = Environment.GetEnvironmentVariable("SQLInitialCatalog", EnvironmentVariableTarget.Process);

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = dataSource,
                UserID = userID,
                Password = Environment.GetEnvironmentVariable("SQLPassword", EnvironmentVariableTarget.Process),
                InitialCatalog = initialCatalog
            };

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            int rows = 0;
            var success = false;
            var timer = Stopwatch.StartNew();
            string response = string.Empty;
            try
            {
                using (var conn = new SqlConnection(builder.ConnectionString))
                {
                    using (var cmd = new SqlCommand(storedProcedure, conn) { CommandType = CommandType.StoredProcedure })
                    {
                        conn.Open();
                        foreach (var param in parameters)
                        {
                            cmd.Parameters.AddWithValue(param.Key, param.Value);
                        }
                        using SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            rows++;
                            int fields = reader.FieldCount;
                            for (int i = 0; i < fields; i++)
                            {
                                response += reader.GetString(i);
                            }
                        }
                    }
                    conn.Close();
                    success = true;
                }
            }
            catch (SqlException sqlEx)
            {
                telemetry.TrackException(sqlEx);
                throw sqlEx;
            }
            finally
            {
                timer.Stop();
                // Log event details in application insights
                telemetry.TrackEvent($"SQL Call to DataSource - {dataSource}, Database - {initialCatalog}, Stored Procedure - {storedProcedure}, UserID - {userID}, Success - {success}, Time - {timer}, Response - {response} ");
            }
            return response;
        }
    }
}