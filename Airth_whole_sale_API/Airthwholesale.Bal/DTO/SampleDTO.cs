using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airthwholesale.Bal.DTO
{
    public class SampleDTO
    {
        public string id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public double ppu { get; set; }
        public Batters batters { get; set; }
        public List<Topping> topping { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Batter
    {
        public string id { get; set; }
        public string type { get; set; }
    }

    public class Batters
    {
        public List<Batter> batter { get; set; }
    }

    public class Topping
    {
        public string id { get; set; }
        public string type { get; set; }
    }

}
