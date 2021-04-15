using System;
using TestNetty.Client.Clients;

namespace TestNetty.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            new TcpClient().
                RunClientAsync().
                GetAwaiter().
                GetResult();
        }
    }
}
