using System;
namespace SERVER
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConnectionServer server = new ConnectionServer();
            server.ConnectionMethod();
            Console.Read();
        }
    }
}