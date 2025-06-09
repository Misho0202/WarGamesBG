using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarGamesBG.DL.Kafka.KafkaCache
{
    public interface IKafkaCache<TKey, TValue>
        where TKey : notnull
        where TValue : class
    {
    }
}
