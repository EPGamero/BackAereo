using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Entities
{
    public class Flights
    {
        public string NoFlight { get; set; }
        public DateTime RegistrationDate { get; set; }
        public Countries OCountry { get; set; }
        public Countries DCountry { get; set; }
        public DateTime DepartureDate { get; set; }

    }
}
