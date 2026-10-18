using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Lab06_CSDL_Basic
{
    /// <summary>Hàm dùng chung để mở kết nối và truy vấn SQL Server.</summary>
    internal static class Db
    {
        private static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["QLSinhVien"].ConnectionString;

        public static SqlConnection OpenConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }

        public static int Execute(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = OpenConnection())
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                if (parameters != null) command.Parameters.AddRange(parameters);
                return command.ExecuteNonQuery();
            }
        }

        public static DataTable Query(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = OpenConnection())
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                if (parameters != null) command.Parameters.AddRange(parameters);
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    var table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
        }

        public static object Scalar(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = OpenConnection())
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                if (parameters != null) command.Parameters.AddRange(parameters);
                return command.ExecuteScalar();
            }
        }

        public static SqlParameter P(string name, SqlDbType type, object value, int size = 0)
        {
            SqlParameter parameter = size > 0
                ? new SqlParameter(name, type, size)
                : new SqlParameter(name, type);
            parameter.Value = value ?? System.DBNull.Value;
            return parameter;
        }
    }
}
