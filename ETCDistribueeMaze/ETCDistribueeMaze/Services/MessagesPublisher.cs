using Confluent.Kafka;
using ETCDistribueeMaze.Models;
using Newtonsoft.Json;

namespace ETCDistribueeMaze.Services
{
    public interface IMessagesPublisher
    {
        Task PublishAsync(Message message);
    }

    public class MessagesPublisher : IMessagesPublisher
    {
        private readonly ProducerConfig _prodconf;

        public MessagesPublisher(ProducerConfig prodconf)
        {
            _prodconf = prodconf;
        }

        public async Task PublishAsync(Message message)
        {
            using var producer = new ProducerBuilder<Null, string>(_prodconf).Build();
            string topic = "massage-topic";
            var bigmess = new Message<Null, string> { Value = JsonConvert.SerializeObject(message) };
            await producer.ProduceAsync(topic, bigmess);
            producer.Flush(new TimeSpan(0, 0, 30));
        }
    }
}
