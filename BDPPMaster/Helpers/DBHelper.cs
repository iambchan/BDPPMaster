using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BDPPMaster.Helpers
{
    public static class DBHelper
    {
        private static readonly string _bdppmasterdb = ConfigurationManager.ConnectionStrings["bdppmasterdb"].ConnectionString;

        #region CREATE
        public static Object CreateNewUser(string FirstName, string LastName, string ScreenName, string BDLoginName, string Email, string RFID)
        {
            var query = String.Format(@"INSERT INTO [Players] (FirstName, LastName, ScreenName, BDLoginName, Email, RFID)
                                        OUTPUT Inserted.PlayerId
                                        VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');", 
                                        FirstName, LastName, ScreenName, BDLoginName, Email, RFID);
            using (var connection = new SqlConnection(_bdppmasterdb)) {
                using (var command = new SqlCommand(query, connection)) {
                    connection.Open();
                    using (var reader = command.ExecuteReader()) {
                        if (!reader.HasRows) { return null; }
                        reader.Read(); //only need the first item
                        return new Object();
                    }
                }
            }
        }
        #endregion

   //     // Set the connection, command, and then execute the command with non query.
   //   public static Int32 ExecuteNonQuery(String connectionString, String commandText,
   //       CommandType commandType, params SqlParameter[] parameters) {
   //      using (SqlConnection conn = new SqlConnection(connectionString)) {
   //         using (SqlCommand cmd = new SqlCommand(commandText, conn)) {
   //            // There're three command types: StoredProcedure, Text, TableDirect. The TableDirect 
   //            // type is only for OLE DB.  
   //            cmd.CommandType = commandType;
   //            cmd.Parameters.AddRange(parameters);

   //            conn.Open();
   //            return cmd.ExecuteNonQuery();
   //         }
   //      }
   //   }

   //   // Set the connection, command, and then execute the command and only return one value.
   //   public static Object ExecuteScalar(String connectionString, String commandText,
   //       CommandType commandType, params SqlParameter[] parameters) {
   //      using (SqlConnection conn = new SqlConnection(connectionString)) {
   //         using (SqlCommand cmd = new SqlCommand(commandText, conn)) {
   //            cmd.CommandType = commandType;
   //            cmd.Parameters.AddRange(parameters);

   //            conn.Open();
   //            return cmd.ExecuteScalar();
   //         }
   //      }
   //   }

   //   // Set the connection, command, and then execute the command with query and return the reader.
   //   public static SqlDataReader ExecuteReader(String connectionString, String commandText,
   //       CommandType commandType, params SqlParameter[] parameters) {
   //      SqlConnection conn = new SqlConnection(connectionString);

   //      using (SqlCommand cmd = new SqlCommand(commandText, conn)) {
   //         cmd.CommandType = commandType;
   //         cmd.Parameters.AddRange(parameters);

   //         conn.Open();
   //         // When using CommandBehavior.CloseConnection, the connection will be closed when the 
   //         // IDataReader is closed.
   //         SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

   //         return reader;
   //      }
   //   }
   //}


        #region UPDATE
        #endregion
    }
}