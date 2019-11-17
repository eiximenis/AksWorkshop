using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClient.Clients;

namespace WebClient.Pages
{
    public class IndexModel : PageModel
    {

        public int ApiValue { get; private set; }
        private readonly IApiClient _apiClient;


        public IndexModel(IApiClient apiClient) => _apiClient = apiClient;


        public async Task OnGet()
        {
            ApiValue = await _apiClient.GetRandomValue();
        }
    }
}
