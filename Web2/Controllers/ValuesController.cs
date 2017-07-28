using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TripActor.Interfaces;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;

namespace Web2.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            Uri m_actorUri = new Uri("fabric:/TestSFA/TripActorService");
            Guid actorid = Guid.NewGuid();
            var actor = ActorProxy.Create<ITripActor>(new ActorId(actorid), m_actorUri);

            await actor.SetCountAsync(25, new System.Threading.CancellationToken());


            // Successfully added, register for event notifications for completion
            // await actor.SubscribeAsync<ITripEvents>(this);

            var cntTask = actor.GetCountAsync(new System.Threading.CancellationToken());
            cntTask.Wait();
            var c = cntTask.Result;
            return new string[] { "value1", "value2" };
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
