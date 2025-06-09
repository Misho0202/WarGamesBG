using Confluent.Kafka;
using MessagePack;

namespace WarGamesBG.Models.Serialization
{
    public class MessagePackDeserializer<T> : IDeserializer<T>
    {
        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            return MessagePackSerializer.Deserialize<T>(data.ToArray());
        }
    }
}
