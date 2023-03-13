using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TP1examuml.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager; //registrer model 

        public RoleController(RoleManager<IdentityRole> roleManager)//constructeur
        {
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View(_roleManager.Roles);
        }

        public IActionResult Create() //create pre
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(IdentityRole model)  // create post
        {
            if(!_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult() )
            {
                _roleManager.CreateAsync(new IdentityRole { Name=model.Name}).GetAwaiter().GetResult();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
