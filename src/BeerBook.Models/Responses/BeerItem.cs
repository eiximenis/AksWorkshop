using System;
using System.Collections.Generic;
using System.Text;

namespace BeerBook.Models.Responses
{
    public class BeerItem
    {
        public string Name { get; set; }
        public string Brewery { get; set; }
        public double Abv { get; set; }
        public int Ibus { get; set; }
        public string Style { get; set; }
        public int Id { get; set; }
    }
}
