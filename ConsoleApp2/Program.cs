using Microsoft.ServiceBus.Messaging;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todo;
using TripActor;
using TripActor.Interfaces;

namespace ConsoleApp2
{
    class Program : ITripEvents
    {
        private static string _connectionString = "Endpoint=sb://infinitybus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=cHlZK7t6GXIOmQAxq+uQz3FzePOsjS3xjSGz8UX0isc=";
        private static string _queueName = "testq";
        static void Main(string[] args)
        {

            Program p = new Program();
            //p.Run().Wait();
            Program.TestMessage();
        }

        public void TripUpdateRequestCompleted(ActorId actorId, TripState trip)
        {
           // throw new NotImplementedException();
        }

        public async Task Run()
        {

            Uri m_actorUri = new Uri("fabric:/TestSFA/TripActorService");
            Guid actorid = Guid.NewGuid();
            var actor = ActorProxy.Create<ITripActor>(new ActorId(actorid), m_actorUri);

            await actor.SetTripAsync(new TripActor.TripState() { TripCode = 1, TripName="Test 1" }, new System.Threading.CancellationToken());


            // Successfully added, register for event notifications for completion
            await actor.SubscribeAsync<ITripEvents>(this);

            var cntTask = actor.GetTripAsync(new System.Threading.CancellationToken());
            cntTask.Wait();
            var c = cntTask.Result;

        }

        static void TestMessage()
        {
       

            // Writing to the service bus for testing purposes
            var client = QueueClient.CreateFromConnectionString(_connectionString, _queueName);

            //var message = new BrokeredMessage()

            //var message = new BrokeredMessage("Testing 123");

            TripState ts = new TripState()
            {
                TripCode = 715,
                TripName ="Lakeshore east"
            };
            //DocumentDBRepository<TripState>.Initialize();

            //DocumentDBRepository<TripState>.CreateItemAsync(ts).Wait();

            client.Send(new BrokeredMessage(ts));
        }

        public void RecountRequestCompleted(ActorId actorId, TripState trip)
        {
            throw new NotImplementedException();
        }

        public void UpdateTripRequestCompleted(ActorId actorId, TripState trip)
        {
            throw new NotImplementedException();
        }
    }
}
