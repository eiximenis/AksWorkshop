using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerBook.Shared.Configuration
{
    public class FolderConfigurationSource : IConfigurationSource
    {
        public string Folder { get; }
        public string Prefix { get;  }
        public FolderConfigurationSource(string folder, string prefix = null)
        {
            Folder = folder;
            Prefix = prefix ?? string.Empty;
        }
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new FolderConfigurationProvider(this);
        }
    }
}
