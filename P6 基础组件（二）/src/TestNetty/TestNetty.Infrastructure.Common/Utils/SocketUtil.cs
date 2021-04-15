using System;
using System.Collections.Generic;
using System.Text;

namespace TestNetty.Infrastructure.Common.Utils
{
    using System;
    using DotNetty.Common.Internal.Logging;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging.Console;

    public static class SocketUtil
    {
        static SocketUtil()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(ProcessDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
        }

        /// <summary>
        /// 获取运行目录路径
        /// </summary>
        public static string ProcessDirectory
        {
            get
            {
#if NETSTANDARD1_3
                return AppContext.BaseDirectory;
#else
                return AppDomain.CurrentDomain.BaseDirectory;
#endif
            }
        }

        public static IConfigurationRoot Configuration { get; }

        public static void SetConsoleLogger() => InternalLoggerFactory.DefaultFactory.AddProvider(new ConsoleLoggerProvider((s, level) => true, false));
    }
}
