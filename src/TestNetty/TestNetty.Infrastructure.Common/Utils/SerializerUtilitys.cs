using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TestNetty.Infrastructure.Common.Utils
{
    public class SerializerUtilitys
    {
        public static byte[] Serialize<T>(T serializeObj)
        {
            try
            {
                using (var stream = new MemoryStream())
                {
                    ProtoBuf.Serializer.Serialize<T>(stream, serializeObj);
                    var result = new byte[stream.Length];
                    stream.Position = 0L;
                    stream.Read(result, 0, result.Length);
                    return result;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static T DeSerialize<T>(byte[] bytes)
        {
            try
            {
                using (var stream = new MemoryStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Position = 0L;
                    return ProtoBuf.Serializer.Deserialize<T>(stream);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return default(T);
            }
        }
    }
}
