namespace ETCDistribueeMaze.Services
{
    public class PositionsConsumerWorker : BackgroundService
    {
        private readonly IPositionConsumer _consumer;

        public PositionsConsumerWorker(IPositionConsumer consumer)
        {
            _consumer = consumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _consumer.BeginConsumeAsync();
        }
    }
}