using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Entities
{
    public class Reservations
    {
        public int IdReservation { get; set; }
        public Flights Flight { get; set; }
        public Passengers Passenger { get; set; }
    }
}
