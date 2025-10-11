using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Midterm_EquipmentRental_Team2.Models;
using Midterm_EquipmentRental_Team2.UnitOfWork;


/// <summary>
/// API Controller for managing Equipment entity.
/// Provides full CRUD operations to Create, Read, Update, and Delete products.
/// Uses services and Unit of Work patterns for data access.
/// </summary>
namespace Midterm_EquipmentRental_Team2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public EquipmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [Authorize(Roles = "Admin, User1, User2")]
        [HttpGet]
        public ActionResult<IEnumerable<Equipment>> GetAllEquipments()
        { 
            return _unitOfWork.Equipements.GetAllEquipments().ToList();
        }
    }
}
