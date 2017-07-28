using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TripActor
{
    [DataContract]
    public class TripState
    {
        [DataMember]
        public int TripCode { get; set; }
        [DataMember]
        public string TripName { get; set; }
    }
}
