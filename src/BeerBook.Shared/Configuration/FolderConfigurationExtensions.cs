using Microsoft.Extensions.Configuration;

namespace BeerBook.Shared.Configuration
{

    public static class FolderConfigurationExtensions
    {
        public static IConfigurationBuilder AddFolder(this IConfigurationBuilder builder, string folderName, string prefix = null) =>
            builder.Add(new FolderConfigurationSource(folderName, prefix));
    }

}
