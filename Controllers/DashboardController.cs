using Microsoft.AspNetCore.Mvc;

namespace AAOAdmin.Controllers
{
  public class DashboardController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}
