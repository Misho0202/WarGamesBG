using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarGamesBG.DL.Kafka
{
    public interface IKafkaProducer<TData>
    {
        Task ProduceAll(IEnumerable<TData> messages);

        Task Produce(TData message);

        Task ProduceBatches(IEnumerable<TData> messages);
    }
}
