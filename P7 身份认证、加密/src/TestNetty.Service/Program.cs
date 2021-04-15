using System;
using TestNetty.Service.Services;

namespace TestNetty.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            new TcpService().
                RunServerAsync().
                GetAwaiter().
                GetResult();
        }
    }
}
