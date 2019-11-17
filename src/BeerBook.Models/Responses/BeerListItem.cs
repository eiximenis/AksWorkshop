using System;
using System.Collections.Generic;
using System.Text;

namespace BeerBook.Models.Responses
{
    public class BeerListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BreweryName { get; set; }
        public int BreweryId { get; set; }
        public string Style { get; set; }
    }
}
