using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Text;

namespace SERVER
{
    public class ConnectionDataBase
    {
        public byte[] bufer = new byte[1024];
        public string connectionString = "server=localhost;port=3306;username=root;password=1234;database=DataBaseAvtoServis;";
        public byte[] ConnDataBase(string sqlrequest)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sqlrequest, conn);

                try
                {
                    conn.Open();
                    Console.WriteLine("Database connection established - " + DateTime.Now.ToString());
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);
                    dataAdapter.Update(dt);
                    string jsonData = JsonConvert.SerializeObject(dt);
                    bufer = Encoding.UTF8.GetBytes(jsonData);
                    conn.Close();
                    conn.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                }
            }
            return bufer;
        }
    }
}
