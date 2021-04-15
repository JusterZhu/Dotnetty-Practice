using System;
using System.Collections.Generic;
using System.Text;

namespace TestNetty.Infrastructure.Common.DTO
{
    public class TestResponseDTO : IMessage
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public List<string> Employees { get; set; }
    }
}
