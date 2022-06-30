using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnitNPM
{
    public class MyDB
    {
        public static void ExNonQuery(string[] CS, string query)
        {
            //CRUD create update delete - return nothing

            var sqlConn = new MySqlConnection();
            sqlConn.ConnectionString = $" server=\"{CS[0]}\"; user id=\"{CS[1]}\"; password=\"{CS[2]}\"; database=\"{CS[3]}\" ";
            try
            {
                sqlConn.Open();
                var sqlCmd = new MySqlCommand(query, sqlConn);
                sqlCmd.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                DebugInfo.GetInfo(ex.Message);
            }
            finally
            {
                sqlConn.Close();
            }

        }
        
        public static DataTable ExReader(string[] CS, string query)
        {
            //Select Query - return object datatable

            MySqlDataReader sqlReader;
            var sqlData = new DataTable();
            var sqlConn = new MySqlConnection();

            sqlConn.ConnectionString = $" server=\"{CS[0]}\"; user id=\"{CS[1]}\"; password=\"{CS[2]}\"; database=\"{CS[3]}\" ";
            try
            {
                sqlConn.Open();
                var sqlCmd = new MySqlCommand(query, sqlConn);

                sqlReader = sqlCmd.ExecuteReader();
                sqlData.Load(sqlReader);
                sqlReader.Close();
                sqlConn.Close();

                return sqlData;
            }
            catch (Exception ex)
            {
                DebugInfo.GetInfo(ex.Message);
                return null;
            }
            finally
            {
                sqlConn.Close();
            }
        }

        public static string ExScalar(string[] CS, string query)
        {
            //Select Query - return first row column
            var sqlConn = new MySqlConnection();
            sqlConn.ConnectionString = $" server=\"{CS[0]}\"; user id=\"{CS[1]}\"; password=\"{CS[2]}\"; database=\"{CS[3]}\" ";
            try
            {
                sqlConn.Open();
                var sqlCmd = new MySqlCommand(query, sqlConn);
                string result = sqlCmd.ExecuteScalar().ToString();
                sqlConn.Close();
                return result;
            }
            catch (Exception ex)
            {
                DebugInfo.GetInfo(ex.Message);
                return "0";
            }
            finally
            {
                sqlConn.Close();
            }
        }
    }
}
