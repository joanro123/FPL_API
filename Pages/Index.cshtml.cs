using FPL_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

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
        [BindProperty(SupportsGet = true)]
        public string TeamName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Position { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool Bonus { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool Goals { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool Assists { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool PointsPerGame { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool TotalPoints { get; set; }
        public List<string> Positions { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Statistic = await GetStatsAsync();
            Positions = Statistic.element_types.Select(x => x.plural_name_short).Distinct().ToList();

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
