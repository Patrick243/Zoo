using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportApp.Domain
{
    public class Route
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string InitialPlace { get; set; }
        [Required]
        public string FinalPlace { get; set; }

        public List<BusStop> BusStops { get; set; }
    }
}
