using Asp.Versioning;
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


        [Authorize(Roles = "Admin, User1, User2")]
        [HttpGet("{id}")]
        public ActionResult<Equipment> GetEquipmentById(int id)
        {
            var equipment = _unitOfWork.Equipements.GetEquipmentById(id);
            if (equipment == null) 
            { 
                return NotFound($"No equipement found with id {id}");
            }
            return Ok(equipment);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<Equipment> CreateEquipement([FromBody]Equipment equipment)
        {
            if (equipment == null)
                return BadRequest("Equipment data is required.");

            _unitOfWork.Equipements.CreateEquipement(equipment);
            _unitOfWork.complete();
            return CreatedAtAction(
                nameof(GetAllEquipments),
                new { Id=equipment.Id },
                equipment
            );
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public ActionResult<Equipment> UpdateEquipement([FromBody] Equipment equipment)
        {
            if (equipment == null)
                return BadRequest("Equipment data is required.");

            var existingEquipment = _unitOfWork.Equipements.GetEquipmentById(equipment.Id);

            if (existingEquipment == null)
                return NotFound($"No equipment found with id {equipment.Id}");

            existingEquipment.Id = equipment.Id;
            existingEquipment.Name = equipment.Name;
            existingEquipment.Description = equipment.Description;
            existingEquipment.IsAvailable = equipment.IsAvailable;
            existingEquipment.Status = equipment.Status;
            existingEquipment.Category = equipment.Category;
            existingEquipment.Condition = equipment.Condition;

            _unitOfWork.Equipements.UpdateEquipement(existingEquipment);
            _unitOfWork.complete();
            return Ok(equipment);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("{id}")]
        public ActionResult<Equipment> DeleteEquipement(int id)
        {
            var existingEquipment = _unitOfWork.Equipements.GetEquipmentById(id);

            if (existingEquipment == null)
                return NotFound($"No equipment found with id {id}");

            _unitOfWork.Equipements.DeleteEquipement(existingEquipment);
            _unitOfWork.complete();
            return Ok(existingEquipment);
        }
    }
}
