namespace ETCDistribueeMaze.Services
{
    public class MazeConsumerWorker :BackgroundService
    {

        private readonly IMazeConsumer _mazeConsumer;

        public MazeConsumerWorker(IMazeConsumer mazeConsumer)
        {
            _mazeConsumer = mazeConsumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _mazeConsumer.BeginConsumeAsync();
        }
    }
}
