using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcuCall.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
    }
}