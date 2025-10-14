using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Midterm_EquipmentRental_Team2.Models;
using Midterm_EquipmentRental_Team2.UnitOfWork;

namespace Midterm_EquipmentRental_Team2.Controllers
{

    [Route("api/rentals")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public RentalController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAllRentals()
        {
            var isAdmin = User.IsInRole("Admin");
            int? userId = isAdmin ? (int?)null : GetUserId();
            var rentals = _unitOfWork.Rentals.GetAllRentals(userId, isAdmin);
            return Ok(rentals);
        }

        [HttpGet("{id}")]
        public IActionResult GetRental(int id)
        {
            var isAdmin = User.IsInRole("Admin");
            int? userId = isAdmin ? (int?)null : GetUserId();
            var rental = _unitOfWork.Rentals.GetRental(id, userId, isAdmin);
            if (rental == null) return NotFound();
            return Ok(rental);
        }

        [HttpGet("equipment/{equipmentId}")]
        public IActionResult GetRentalsByEquipment(int equipmentId)
        {
            var rentals = _unitOfWork.Rentals.GetRentalsByEquipment(equipmentId);
            return Ok(rentals);
        }

        [HttpGet("active")]
        public IActionResult GetActiveRentals()
        {
            var isAdmin = User.IsInRole("Admin");
            int? userId = isAdmin ? (int?)null : GetUserId();
            var rentals = _unitOfWork.Rentals.GetActiveRentals(userId, isAdmin);
            return Ok(rentals);
        }

        [HttpGet("completed")]
        public IActionResult GetCompletedRentals()
        {
            var isAdmin = User.IsInRole("Admin");
            int? userId = isAdmin ? (int?)null : GetUserId();
            var rentals = _unitOfWork.Rentals.GetCompletedRentals(userId, isAdmin);
            return Ok(rentals);
        }

        [HttpGet("overdue")]
        public IActionResult GetOverdueRentals()
        {
            var rentals = _unitOfWork.Rentals.GetOverdueRentals();
            return Ok(rentals);
        }

        [HttpPost("issue")]
        public IActionResult IssueRental([FromBody] Rental rental)
        {
            var userId = GetUserId();
            try
            {
                _unitOfWork.Rentals.IssueRental(rental, userId);
                _unitOfWork.Complete();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("return")]
        public IActionResult ReturnRental([FromBody] Rental rental)
        {
            var userId = GetUserId();
            try
            {
                _unitOfWork.Rentals.ReturnRental(rental.Id, userId, rental.ReturnNotes, rental.ReturnCondition.ToString(), false);
                _unitOfWork.Complete();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/extend")]
        public IActionResult ExtendRental(int id, [FromBody] Rental rental)
        {
            var userId = GetUserId();
            try
            {
                _unitOfWork.Rentals.ExtendRental(id, rental.DueDate ?? DateTime.UtcNow, rental.ReturnNotes, userId);
                _unitOfWork.Complete();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult CancelRental(int id, [FromQuery] bool force = false)
        {
            var userId = GetUserId();
            try
            {
                _unitOfWork.Rentals.CancelRental(id, userId, force);
                _unitOfWork.Complete();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private int GetUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
        }
    }
}
