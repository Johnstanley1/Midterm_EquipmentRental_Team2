using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Midterm_EquipmentRental_Team2.Models;
using Midterm_EquipmentRental_Team2.Models.DTOs;
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

    [Authorize(Roles = "Admin,User")]
    [HttpGet]
        public ActionResult<IEnumerable<RentalDto>> GetAllRentals()
        {
            var isAdmin = User.IsInRole("Admin");
            int? userId = isAdmin ? (int?)null : GetUserId();
            var rentals = _unitOfWork.Rentals.GetAllRentalsDto(userId, isAdmin);
            return Ok(rentals);
        }

    [Authorize(Roles = "Admin,User")]
    [HttpGet("{id}")]
        public ActionResult<RentalDto> GetRental(int id)
        {
            var isAdmin = User.IsInRole("Admin");
            int? userId = isAdmin ? (int?)null : GetUserId();
            var rental = _unitOfWork.Rentals.GetRentalDto(id, userId, isAdmin);
            if (rental == null) return NotFound();
            return Ok(rental);
        }

    [Authorize(Roles = "Admin,User")]
        [HttpGet("equipment/{equipmentId}")]
        public ActionResult<IEnumerable<RentalDto>> GetRentalsByEquipment(int equipmentId)
        {
            var rentals = _unitOfWork.Rentals.GetRentalsByEquipment(equipmentId)
                .Select(r => new RentalDto
                {
                    Id = r.Id,
                    EquipmentId = r.EquipmentId,
                    EquipmentName = r.Equipment?.Name ?? string.Empty,
                    EquipmentStatus = r.Equipment?.Status.ToString() ?? string.Empty,
                    CustomerId = r.CustomerId,
                    CustomerName = r.Customer?.Name ?? string.Empty,
                    IssuedAt = r.IssuedAt,
                    DueDate = r.DueDate,
                    ReturnedAt = r.ReturnedAt,
                    Status = r.Status
                });
            return Ok(rentals);
        }

    [Authorize(Roles = "Admin,User")]
    [HttpGet("active")]
        public ActionResult<IEnumerable<RentalDto>> GetActiveRentals()
        {
            var isAdmin = User.IsInRole("Admin");
            int? userId = isAdmin ? (int?)null : GetUserId();
            var rentals = _unitOfWork.Rentals.GetActiveRentals(userId, isAdmin)
                .Select(r => new RentalDto
                {
                    Id = r.Id,
                    EquipmentId = r.EquipmentId,
                    EquipmentName = r.Equipment?.Name ?? string.Empty,
                    EquipmentStatus = r.Equipment?.Status.ToString() ?? string.Empty,
                    CustomerId = r.CustomerId,
                    CustomerName = r.Customer?.Name ?? string.Empty,
                    IssuedAt = r.IssuedAt,
                    DueDate = r.DueDate,
                    ReturnedAt = r.ReturnedAt,
                    Status = r.Status
                });
            return Ok(rentals);
        }

    [Authorize(Roles = "Admin,User")]
    [HttpGet("completed")]
        public ActionResult<IEnumerable<RentalDto>> GetCompletedRentals()
        {
            var isAdmin = User.IsInRole("Admin");
            int? userId = isAdmin ? (int?)null : GetUserId();
            var rentals = _unitOfWork.Rentals.GetCompletedRentals(userId, isAdmin)
                .Select(r => new RentalDto
                {
                    Id = r.Id,
                    EquipmentId = r.EquipmentId,
                    EquipmentName = r.Equipment?.Name ?? string.Empty,
                    EquipmentStatus = r.Equipment?.Status.ToString() ?? string.Empty,
                    CustomerId = r.CustomerId,
                    CustomerName = r.Customer?.Name ?? string.Empty,
                    IssuedAt = r.IssuedAt,
                    DueDate = r.DueDate,
                    ReturnedAt = r.ReturnedAt,
                    Status = r.Status
                });
            return Ok(rentals);
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("overdue")]
        public ActionResult<IEnumerable<RentalDto>> GetOverdueRentals()
        {
            var isAdmin = User.IsInRole("Admin");
            var userId = isAdmin ? (int?)null : GetUserId();
            var rentals = _unitOfWork.Rentals.GetOverdueRentals();
            if (!isAdmin && userId.HasValue)
            {
                rentals = rentals.Where(r => r.CustomerId == userId.Value);
            }
            var dtos = rentals.Select(r => new RentalDto
            {
                Id = r.Id,
                EquipmentId = r.EquipmentId,
                EquipmentName = r.Equipment?.Name ?? string.Empty,
                EquipmentStatus = r.Equipment?.Status.ToString() ?? string.Empty,
                CustomerId = r.CustomerId,
                CustomerName = r.Customer?.Name ?? string.Empty,
                IssuedAt = r.IssuedAt,
                DueDate = r.DueDate,
                ReturnedAt = r.ReturnedAt,
                Status = r.Status
            });
            return Ok(dtos);
        }

    [Authorize(Roles = "Admin,User")]
    [HttpPost("issue")]
        public IActionResult IssueRental([FromBody] IssueRentalRequest request)
        {
            var isAdmin = User.IsInRole("Admin");
            var userId = GetUserId();
            try
            {
                var targetCustomerId = isAdmin && (request.CustomerId ?? 0) != 0 ? request.CustomerId!.Value : userId;
                var rental = new Rental
                {
                    EquipmentId = request.EquipmentId,
                    CustomerId = targetCustomerId,
                    DueDate = request.DueDate,
                    IssuedAt = request.IssuedAt ?? default(DateTime),
                    Status = "Active"
                };
                _unitOfWork.Rentals.IssueRental(rental, targetCustomerId);
                _unitOfWork.Complete();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPost("return")]
        public IActionResult ReturnRental([FromBody] Rental rental, [FromQuery] bool force = false)
        {
            var userId = GetUserId();
            try
            {
                var canForce = force && User.IsInRole("Admin");
                _unitOfWork.Rentals.ReturnRental(rental.Id, userId, rental.ReturnNotes, rental.ReturnCondition?.ToString(), canForce);
                _unitOfWork.Complete();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    [Authorize(Roles = "Admin,User")]
    [HttpPut("{id}/extend")]
        public IActionResult ExtendRental(int id, [FromBody] Rental rental)
        {
            var userId = GetUserId();
            var isAdmin = User.IsInRole("Admin");
            try
            {
                _unitOfWork.Rentals.ExtendRental(id, rental.DueDate ?? DateTime.UtcNow, rental.ReturnNotes ?? string.Empty, userId, isAdmin);
                _unitOfWork.Complete();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult CancelRental(int id, [FromQuery] bool force = false)
        {
            var userId = GetUserId();
            try
            {
                var canForce = User.IsInRole("Admin") || force;
                _unitOfWork.Rentals.CancelRental(id, userId, canForce);
                _unitOfWork.Complete();
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(StatusCodes.Status403Forbidden, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Contains("not found", StringComparison.OrdinalIgnoreCase))
                {
                    return NotFound(ex.Message);
                }
                return BadRequest(ex.Message);
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
