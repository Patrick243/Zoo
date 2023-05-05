using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportApp.Domain
{
    public class BusStop
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int RouteId { get; set; }

        public Route Route { get; set; }

        public List<Street> Streets { get; set; }

    }
}
