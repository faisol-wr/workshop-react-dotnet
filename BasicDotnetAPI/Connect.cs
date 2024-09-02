using Npgsql;

namespace BasicDotnetAPI
{
    public class Connect
    {
        public NpgsqlConnection GetConnection()
        {
            try
            {
                string host = "localhost";
                string port = "5433";
                string user = "postgres";
                string pass = "123456";
                string db = "db_cs_api";

                NpgsqlConnection conn = new NpgsqlConnection();
                conn.ConnectionString = string.Format("Server={0};Username={1};Database={2};Port={3};Password={4}",
                    host,
                    user,
                    db,
                    port,
                    pass
                );
                conn.Open();

                return conn;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

