using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using TripActor;
using todo;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace EventProcessor
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class EventProcessor : StatelessService
    {
        private static string _connectionString = "Endpoint=sb://infinitybus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=cHlZK7t6GXIOmQAxq+uQz3FzePOsjS3xjSGz8UX0isc=";
        private static string _queueName = "testq";
        public EventProcessor(StatelessServiceContext context)
            : base(context)
        {
            DocumentDBRepository<TripState>.Initialize();
        }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[0];
        }

        /// <summary>
        /// This is the main entry point for your service instance.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service instance.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {

            NamespaceManager manager = NamespaceManager.CreateFromConnectionString(_connectionString);
            try
            {
                await manager.CreateQueueAsync("testq");
            }
            catch (Exception ex) { }

            QueueClient queue = QueueClient.CreateFromConnectionString(_connectionString,
                _queueName);
            while (true)
            {
                try
                {
                    var msg = await queue.ReceiveAsync();
                    if (msg != null)
                    {
                        //Do Something - save etc
                        var trip = msg.GetBody<TripState>();
                        if (trip != null)
                        {
                            throw new UnauthorizedAccessException();
                            await DocumentDBRepository<TripState>.CreateItemAsync(trip);
                        }
                        ServiceEventSource.Current.Message($"{this.Context.InstanceId} - {msg.MessageId}");
                        await msg.CompleteAsync();

                    }
                }
                catch(Exception e)
                {
                    ServiceEventSource.Current.Error($"{this.Context.InstanceId} - {e.Message}");
                }
            }



            // var client = QueueClient.CreateFromConnectionString(_connectionString, _queueName);
            // TODO: Replace the following sample code with your own logic 
            //       or remove this RunAsync override if it's not needed in your service.

            //long iterations = 0;

            //while (true)
            //{
            //    cancellationToken.ThrowIfCancellationRequested();

            //    ServiceEventSource.Current.ServiceMessage(this.Context, "Working-{0}", ++iterations);

            //    await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            //}
        }
    }
}
