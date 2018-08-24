using Microsoft.AspNetCore.Mvc;

namespace AcuCall.Web.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {            
            return View();
        }        
    }
}
