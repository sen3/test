using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripActor.Interfaces;
using Microsoft.ServiceFabric.Actors.Client;
using Microsoft.ServiceFabric.Actors;

namespace TripActor.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Uri m_actorUri = new Uri("fabric:/TestSFA/TripActorService");
            //Guid actorid = Guid.NewGuid();
            //var actor = ActorProxy.Create<ITripActor>(new ActorId(actorid), m_actorUri);

            //actor.SetCountAsync(25, new System.Threading.CancellationToken()).Wait();

            //var cnt =  actor.GetCountAsync(new System.Threading.CancellationToken()).Result;
        }
    }
}
