using System;
using System.Collections.Generic;
using System.Text;

namespace TestNetty.Infrastructure.Common.DTO
{
    public interface IPacket
    {
        byte[] Serialize();

        void Deserialize(byte[] data);
    }
}
