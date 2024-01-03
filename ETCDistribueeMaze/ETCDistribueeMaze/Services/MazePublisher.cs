using Confluent.Kafka;
using ETCDistribueeMaze.Models;
using ETCDistribueeMaze.ViewModels;
using Newtonsoft.Json;

namespace ETCDistribueeMaze.Services
{
    public interface IMazePublisher
    {
        Task PublishAsync(MazeGrid grid);

    }
    public class MazePublisher : IMazePublisher
    {
        private readonly ProducerConfig _prodconf;

        public MazePublisher(ProducerConfig prodconf)
        {
            _prodconf = prodconf;
        }

        public async Task PublishAsync(MazeGrid maze)
        {
            using var producer = new ProducerBuilder<Null, string>(_prodconf).Build();
            string topic = "maze-topic";
            var bigmess = new Message<Null, string> { Value = JsonConvert.SerializeObject(maze) };
            await producer.ProduceAsync(topic, bigmess);
            producer.Flush(new TimeSpan(0, 0, 30));
        }
    }
}
