using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FPL_API.Models;
using System.Text.Json;

namespace FPL_API.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public HttpClient client = new HttpClient();
        public Statistic Statistic { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Statistic = await GetStatsAsync();

            return Page();
        }


        public async Task<Statistic> GetStatsAsync()
        {
            string link = $"https://fantasy.premierleague.com/api/bootstrap-static/";
            Task<string> getFplStringTask = client.GetStringAsync(link);
            string fplString = await getFplStringTask;
            Statistic = JsonSerializer.Deserialize<Statistic>(fplString);
            return Statistic;

        }
    }
}
