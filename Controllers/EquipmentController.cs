using Microsoft.AspNetCore.Mvc;

namespace Midterm_EquipmentRental_Team2.Controllers
{
    public class EquipmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
