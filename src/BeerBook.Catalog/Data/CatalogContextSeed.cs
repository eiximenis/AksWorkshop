using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BeerBook.Catalog.Data
{
    public class CatalogContextSeed
    {
        public async Task Seed(CatalogContext context, ILogger<CatalogContextSeed> logger)
        {
            if (context.Beers.Any()) return;
            await SeedBreweries(context, logger);
            await SeedBeers(context, logger);
        }

        private async Task SeedBreweries(CatalogContext context, ILogger<CatalogContextSeed> logger)
        {
            context.Breweries.Add(new Brewery()
            {
                Name = "La Virgen",
                Country = "Spain"
            });

            context.Breweries.Add(new Brewery()
            {
                Name = "Mahou",
                Country = "Spain"
            });

            context.Breweries.Add(new Brewery()
            {
                Name = "Brewdog",
                Country = "Scotland"
            });

            context.Breweries.Add(new Brewery()
            {
                Name = "Espina de Ferro",
                Country = "Spain"
            });

            context.Breweries.Add(new Brewery()
            {
                Name = "Elysian",
                Country = "USA"
            });

            await context.SaveChangesAsync();
        }
        private async Task SeedBeers(CatalogContext context, ILogger<CatalogContextSeed> logger)
        {
            await SeedLaVirgen(context, logger);
            await SeedMahou(context, logger);
            await SeedBrewdog(context, logger);
            await SeedEspina(context, logger);
            await SeedElysian(context, logger);
        }

        private async Task SeedLaVirgen(CatalogContext context, ILogger<CatalogContextSeed> logger)
        {
            var brewery = context.Breweries.Single(b => b.Name == "La Virgen");

            context.Beers.Add(new Beer()
            {
                Name = "360",
                Abv = 5.5,
                Ibus = 35,
                Style ="Pale Ale",
                Brewery = brewery
            });

            await context.SaveChangesAsync();
        }

        private async Task SeedMahou(CatalogContext context, ILogger<CatalogContextSeed> logger)
        {
            var brewery = context.Breweries.Single(b => b.Name == "Mahou");

            context.Beers.Add(new Beer()
            {
                Name = "5 estrellas",
                Abv = 4.0,
                Ibus = 10,
                Style = "Lager",
                Brewery = brewery
            });

            context.Beers.Add(new Beer()
            {
                Name = "San Miguel",
                Abv = 4.2,
                Ibus = 15,
                Style = "Lager",
                Brewery = brewery
            });

            await context.SaveChangesAsync();
        }

        private async Task SeedBrewdog(CatalogContext context, ILogger<CatalogContextSeed> logger)
        {
            var brewery = context.Breweries.Single(b => b.Name == "Brewdog");

            context.Beers.Add(new Beer()
            {
                Name = "Punk IPA",
                Abv = 5.6,
                Ibus = 35,
                Style = "IPA",
                Brewery = brewery
            });

            await context.SaveChangesAsync();
        }

        private async Task SeedEspina(CatalogContext context, ILogger<CatalogContextSeed> logger)
        {
            var brewery = context.Breweries.Single(b => b.Name == "Espina de Ferro");

            context.Beers.Add(new Beer()
            {
                Name = "Impaled",
                Abv = 8.0,
                Ibus = 105,
                Style = "Imperial IPA",
                Brewery = brewery
            });

            context.Beers.Add(new Beer()
            {
                Name = "Mortale",
                Abv = 6.5,
                Ibus = 45,
                Style = "IPA",
                Brewery = brewery
            });

            await context.SaveChangesAsync();
        }

        private async Task SeedElysian(CatalogContext context, ILogger<CatalogContextSeed> logger)
        {
            var brewery = context.Breweries.Single(b => b.Name == "Elysian");

            context.Beers.Add(new Beer()
            {
                Name = "Zephyrus",
                Abv = 7.5,
                Ibus = 80,
                Style = "Pilsner",
                Brewery = brewery
            });

            context.Beers.Add(new Beer()
            {
                Name = "Space Dust",
                Abv = 8.2,
                Ibus = 73,
                Style = "IPA",
                Brewery = brewery
            });

            await context.SaveChangesAsync();
        }

        
    }
}
