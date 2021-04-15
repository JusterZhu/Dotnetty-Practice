using System;
using System.Collections.Generic;
using System.Text;

namespace TestNetty.Infrastructure.Common.DTO
{
    public class TestRequestDTO : IMessage
    {
        //public string Account { get; set; }

        //public string Passworld { get; set; }

        public string Sign { get; set; }

        public string Nonce { get; set; }

        public string HashValue { get; set; }

        public long ValidateTime { get; set; }
    }
}
