using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TP1examuml.Models.API;

namespace TP1examuml.Controllers
{
    public class APIController : Controller
    {
        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = new HttpClient();
            var reponse = await httpClient.GetAsync("http://localhost:7292/api/CLIENTS");
            if (reponse.IsSuccessStatusCode)
            {
                var content=await reponse.Content.ReadAsStringAsync();
                List<Client> clients=JsonConvert.DeserializeObject<List<Client>>(content);
                return View(clients);
            }
            return View(new List<Client>());

        }
    }
}
