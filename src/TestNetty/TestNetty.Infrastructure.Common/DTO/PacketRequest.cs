using System;
using System.Collections.Generic;
using System.Text;
using TestNetty.Infrastructure.Common.Utils;

namespace TestNetty.Infrastructure.Common.DTO
{
    public class PacketRequest<TMessage> : IPacket
      where TMessage : class
    {
        /// <summary>
        /// 包头标志，用于校验 4byte
        /// </summary>
        public int Checkbit { get; set; }

        #region Head

        /// <summary>
        /// 4个byte表示package长度
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 4个byte表示commandId  1001为商品数据
        /// </summary>
        public int CommandId { get; set; }

        #endregion

        /// <summary>
        /// package内容
        /// </summary>
        public TMessage Body { get; set; }

        public void Deserialize(byte[] data)
        {
            Checkbit = 0x1F;
            Body = SerializerUtilitys.DeSerialize<TMessage>(data);
        }

        public byte[] Serialize()
        {
            Checkbit = 0x1F;
            Length = 4 * 3;//12
            var bodyArray = SerializerUtilitys.Serialize(Body);//20
            Length += bodyArray.Length;//32
            return bodyArray;
        }
    }
}
