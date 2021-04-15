using System;
using TestNetty.Data;
using TestNetty.Infrastructure.Common.DB;
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
