using Microsoft.Extensions.Configuration;

namespace BeerBook.Shared
{
    public static class SharedConfigurationExtensions
    {
        public static string GetBasePath(this IConfiguration configuration)
        {
            var pathBase = configuration["PATH_BASE"]?.Trim();
            if (string.IsNullOrEmpty(pathBase) || pathBase == "/")
            {
                return null;
            }
            else return pathBase;
        }
    }
}
