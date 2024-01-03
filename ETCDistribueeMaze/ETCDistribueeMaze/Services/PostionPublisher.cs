using Confluent.Kafka;
using ETCDistribueeMaze.Models;
using Newtonsoft.Json;

namespace ETCDistribueeMaze.Services
{
    public interface IPositionPublisher
    {
        Task PublishAsync(Position position);
    }

    public class PositionPublisher : IPositionPublisher
    {
        private readonly ProducerConfig _prodconf;

        public PositionPublisher(ProducerConfig prodconf)
        {
            _prodconf = prodconf;
        }

        public async Task PublishAsync(Position position)
        {
            using var producer = new ProducerBuilder<Null, string>(_prodconf).Build();
            string topic = "Position-topic";
            var bigmess = new Message<Null, string> { Value = JsonConvert.SerializeObject(position) };
            await producer.ProduceAsync(topic, bigmess);
            //producer.Flush(new TimeSpan(0, 0, 30));
        }
    }
}
