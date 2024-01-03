using Confluent.Kafka;
using ETCDistribueeMaze.Models;
using Newtonsoft.Json;

namespace ETCDistribueeMaze.Services
{
    public interface IPositionConsumer
    {
        Task BeginConsumeAsync();
        event EventHandler<Position> PositionReceived;
    }

    public class PositionConsumer : IPositionConsumer
    {
        private readonly ConsumerConfig _consconf;

        public List<IConsumer<Ignore, string>> buildConsumers(string[] arStr)
        {
            List<IConsumer<Ignore, string>> arCon = new();

            foreach (var str in arStr)
            {
                var con = new ConsumerBuilder<Ignore, string>(_consconf)
                            .SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}"))
                            .SetPartitionsAssignedHandler((c, partitions) => {
                                Console.WriteLine($"Assigned partitions: {string.Join(", ", partitions)}");
                            })
                            .SetPartitionsRevokedHandler((c, partitions) => {
                                Console.WriteLine($"Revoked partitions: {string.Join(", ", partitions)}");
                            }).Build();
                con.Subscribe(str);
                arCon.Add(con);
            }
            return arCon;
        }

        public PositionConsumer(ConsumerConfig consconf)
        {
            _consconf = consconf;
        }

        public async Task BeginConsumeAsync()
        {
            var topics = new string[] { "Position-topic" };
            var cancellationTokenSource = new CancellationTokenSource();
            var consumers = buildConsumers(topics);
            try
            {
                foreach (var consumer in consumers)
                {
                    var thread = new Thread(() =>
                    {
                        try
                        {
                            while (!cancellationTokenSource.Token.IsCancellationRequested)
                            {
                                try
                                {
                                    var position = consumer.Consume(cancellationTokenSource.Token);
                                    this.PositionReceived?.Invoke(this, JsonConvert.DeserializeObject<Position>(position.Message.Value));
                                }
                                catch { }
                            }
                        }
                        catch (OperationCanceledException)
                        {
                            Console.WriteLine($"Consumer thread canceled.");
                        }
                        finally
                        {
                            consumer.Close();
                        }
                    });
                    thread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }

        public event EventHandler<Position> PositionReceived;
    }
}
