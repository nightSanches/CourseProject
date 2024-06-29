using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CourseProject.Classes.Common
{
    public class Connection
    {
        //private static readonly string config = "server=student.permaviat.ru;Trusted_Connection=No;DataBase=base1_ISP_21_2_20;User=ISP_21_2_20;pwd=kD2G6P0Zl#";
        private static readonly string config = "server=SANCHES\\SQLEXPRESS;Trusted_Connection=No;DataBase=Airport;User=sa;pwd=sa";
        public static SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(config);
            connection.Open();
            return connection;
        }

        public static SqlDataReader Query(string SQL, out SqlConnection connection)
        {
            connection = OpenConnection();
            return new SqlCommand(SQL, connection).ExecuteReader();
        }

        public static void CloseConnection(SqlConnection connection)
        {
            connection.Close();
            SqlConnection.ClearPool(connection);
        }
    }
}
