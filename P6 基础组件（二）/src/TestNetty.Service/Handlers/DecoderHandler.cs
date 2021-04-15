using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Text;
using TestNetty.Infrastructure.Common.DTO;
using TestNetty.Infrastructure.Common.Utils;

namespace TestNetty.Service.Handlers
{
    /// <summary>
    /// Decoder Packet
    /// </summary>
    public class DecoderHandler : ByteToMessageDecoder
    {
        protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            //0 if (!input.HasArray) return;

            var request = new PacketRequest<string>();
            request.Checkbit = input.ReadInt();//0-4
            request.Length = input.ReadInt();//4-8
            request.CommandId = input.ReadInt();//8-12
            IByteBuffer result = Unpooled.Buffer(request.Length - 4 * 3);
            input.ReadBytes(result, request.Length - 4 * 3);//12 - (20 - 4 * 3) 从第12个字节位置开始往后读8个字节
            request.Deserialize(result.Array);
            output.Add(request);
            result.Clear();

            //1 显式丢弃消息
            //ReferenceCountUtil.Release(msg);

            //2增加引用计数防止释放
            //ReferenceCountUtil.retain(message)

            //3 IByteBuffer.Read 读字节 IByteBuffer.Write 写字节

            /*
             * 4 移除可丢弃字节
             * 所谓可丢弃字节就是调用read方法之后，readindex已经移动过了的区域，这段区域的字节称为可丢弃字节。
             * 只有在内存十分宝贵需要清理的时候再调用这个方法，随便调用有可能会造成内存的复制，降低效率。
             */
            //message.DiscardReadBytes();

            //5 读取所有可读字节（移动读索引）
            //while (message.IsReadable())
            //{
            //    Console.WriteLine(message.ReadByte());
            //}
        }
    }
}
