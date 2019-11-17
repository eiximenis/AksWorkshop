using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerBook.Catalog.Data
{
    public class CatalogContext : DbContext
    {

        public CatalogContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Beer> Beers { get; set; }

        public DbSet<Brewery> Breweries { get; set; }
    }
}
