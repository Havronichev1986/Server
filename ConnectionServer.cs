using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SERVER
{
    public class ConnectionServer
    {
        static IPAddress IPAddress = IPAddress.Parse("127.0.0.1");
        static IPEndPoint IPEndPoint = new IPEndPoint(IPAddress, 123);
        public static TcpClient client;
        public static TcpListener listener;
        byte[] bytes = new byte[1024];
        public async void ConnectionMethod()
        {
            try
            {
                listener = new TcpListener(IPEndPoint);
                listener.Start();
                await Console.Out.WriteLineAsync("Server is running - " + DateTime.Now.ToString() + "\n");
                ConnectionDataBase connectionData = new ConnectionDataBase();
                while (true)
                {
                    client = listener.AcceptTcpClient();
                    NetworkStream networkStream = client.GetStream();
                    int SqlRead = await networkStream.ReadAsync(bytes, 0, bytes.Length);
                    string SqlRequest = Encoding.UTF8.GetString(bytes, 0, SqlRead);
                    byte[] buf = connectionData.ConnDataBase(SqlRequest);
                    await networkStream.WriteAsync(buf, 0, buf.Length);
                    networkStream.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex);
            }
            finally
            {
                listener.Stop();
            }
        }
    }
}
