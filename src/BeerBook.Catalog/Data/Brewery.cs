using System.Collections;
using System.Collections.Generic;

namespace BeerBook.Catalog.Data
{
    public class Brewery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public ICollection<Beer> Beers { get; set; }

        public Brewery()
        {
            Beers = new List<Beer>();
        }
    }
}