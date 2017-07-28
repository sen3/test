using Microsoft.ServiceFabric.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripActor.Interfaces
{
    public interface ITripEvents : IActorEvents
    {
        // Notify that the actor idenfitied by actor id has completed the request.
        // The recipient can find the actor id based on the request item id, but we want to avoid another lookup
        void UpdateTripRequestCompleted(ActorId actorId, TripState trip);
    }


}
