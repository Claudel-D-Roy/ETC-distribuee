using Confluent.Kafka;
using ETCDistribueeMaze.Models;
using ETCDistribueeMaze.Providers;
using ETCDistribueeMaze.Services;
using ETCDistribueeMaze.ViewModels;
using Microsoft.AspNetCore.Components.Server.Circuits;

namespace ETCDistribueeMaze
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddSingleton<IUserStateProvider, UserStateProvider>();
   

            services.AddScoped<IConnectedClientService, InMemoryConnectedClientService>();
            services.AddScoped<ClientCircuitHandler>();
            services.AddScoped<CircuitHandler>(ctx => ctx.GetService<ClientCircuitHandler>());

            var channel = System.Threading.Channels.Channel.CreateBounded<Position>(100);
            var messagesChannel = System.Threading.Channels.Channel.CreateBounded<Message>(100);
            var mazeChannel = System.Threading.Channels.Channel.CreateBounded<Maze>(100);


       //   string SERVEURKAFKA = "kafka-src:9092";
           string SERVEURKAFKA = "localhost:9092";

            services.AddSingleton<IPositionPublisher>(ctx => {
                return new PositionPublisher(new ProducerConfig()
                {
                    BootstrapServers = SERVEURKAFKA
                });
            });

            services.AddSingleton<IMessagesPublisher>(ctx => {
                return new MessagesPublisher(new ProducerConfig()
                {
                    BootstrapServers = SERVEURKAFKA
                });
            });
            services.AddSingleton<IMazePublisher>(ctx =>
            {
                return new MazePublisher(new ProducerConfig()
                {
                    BootstrapServers = SERVEURKAFKA
                });
            });
            services.AddSingleton<IPositionConsumer>(ctx => {
                return new PositionConsumer(new ConsumerConfig()
                {
                    BootstrapServers = SERVEURKAFKA,
                    GroupId = "position-test-group",
                    AutoOffsetReset = AutoOffsetReset.Earliest
                });
            });

            services.AddSingleton<IMessagesConsumer>(ctx => {
                return new MessagesConsumer(new ConsumerConfig()
                {
                    BootstrapServers = SERVEURKAFKA,
                    GroupId = "messages-test-group",
                    AutoOffsetReset = AutoOffsetReset.Earliest
                });
            });

            services.AddSingleton<IMazeConsumer>(ctx =>
            {
                return new MazeConsumer(new ConsumerConfig()
                {
                    BootstrapServers = SERVEURKAFKA,
                    GroupId = "maze-test-group",
                    AutoOffsetReset = AutoOffsetReset.Earliest
                });
            });

           

            services.AddHostedService<MazeConsumerWorker>();
            services.AddHostedService<PositionsConsumerWorker>();
            services.AddHostedService<MessagesConsumerWorker>();

            services.AddSingleton<IMazeService, MazeService>();


      
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
