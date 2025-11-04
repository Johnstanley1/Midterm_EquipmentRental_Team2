using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Midterm_EquipmentRental_Team2.Models;
using Midterm_EquipmentRental_Team2.Models.DTOs;
using Midterm_EquipmentRental_Team2.UnitOfWork;
using static Midterm_EquipmentRental_Team2.Models.Equipment;

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


        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public ActionResult<IEnumerable<RentalDTO>> GetAllRentals()
        {
            IEnumerable<RentalDTO> rentals;

            if (User.IsInRole("Admin"))
            {
                // Admin sees all rentals
                rentals = _unitOfWork.Rentals.GetAllRentals();
            }
            else
            {
                // users see only their rentals
                var username = User.Identity.Name; 
                rentals = _unitOfWork.Rentals.GetAllRentals();
            }

            return Ok(rentals);
        }



        [Authorize(Roles = "Admin, User")]
        [HttpGet("{id}")]
        public ActionResult<RentalDTO> GetRentalsById(int id)
        {
            var rental = _unitOfWork.Rentals.GetRentalsById(id);

            if (rental == null)
                return NotFound($"No rentals found with id {id}");


            if (User.IsInRole("User") && rental.CustomerName != User.Identity?.Name) // Users can only access their own data
                return Forbid();

            return Ok(rental);
        }


         
        [Authorize(Roles = "Admin, User")]
        [HttpGet("equipment/{equipmentId}")]
        public ActionResult<IEnumerable<RentalDTO>> GetRentedEquipments(int equipmentId)
        {
            var rentals = _unitOfWork.Rentals.GetRentedEquipments(equipmentId);
            Console.WriteLine($"Found {rentals.Count()} rentals for equipment ID {equipmentId}");


            if (rentals == null)
                return NotFound($"No rentals found with id");


            // If user is not admin, only return their own rentals
            if (User.IsInRole("User"))
            {
                var username = User.Identity?.Name;
                rentals = rentals.Where(r => r.CustomerName == username);
            }


            return Ok(rentals);
        }



        [Authorize(Roles = "Admin, User")]
        [HttpGet("active")]
        public ActionResult<IEnumerable<RentalDTO>> GetActiveRentals()
        {
            var rentals = _unitOfWork.Rentals.GetActiveRentals();

            if (rentals == null)
                return NotFound($"No rental found with id");


            // If user is not admin, only return their own rentals
            if (User.IsInRole("User"))
            {
                var username = User.Identity?.Name;
                rentals = rentals.Where(r => r.CustomerName == username);
            }

            return Ok(rentals);
        }



        [Authorize(Roles = "Admin, User")]
        [HttpGet("completed")]
        public ActionResult<IEnumerable<RentalDTO>> GetCompletedRentals()
        {
            var rentals = _unitOfWork.Rentals.GetCompletedRentals();

            if (rentals == null)
                return NotFound($"No rental found with id");


            // If user is not admin, only return their own rentals
            if (User.IsInRole("User"))
            {
                var username = User.Identity?.Name;
                rentals = rentals.Where(r => r.CustomerName == username);
            }

            return Ok(rentals);
        }



        [Authorize(Roles = "Admin, User")]
        [HttpGet("overdue")]
        public ActionResult<IEnumerable<RentalDTO>> GetOverdueRentals()
        {
            var rentals = _unitOfWork.Rentals.GetOverdueRentals();

            if (rentals == null)
                return NotFound($"No customer found with id");

            // If user is not admin, only return their own rentals
            if (User.IsInRole("User"))
            {
                var username = User.Identity?.Name;
                rentals = rentals.Where(r => r.CustomerName == username);
            }

            return Ok(rentals);
        }



        [Authorize(Roles = "Admin, User")]
        [HttpPost("issue")]
        public ActionResult<Rental> AddRental([FromBody] Rental rental)
        {
            if (rental == null)
                return BadRequest("Customer data is required.");

            _unitOfWork.Rentals.AddRental(rental);
            _unitOfWork.Complete();
            return CreatedAtAction(nameof(GetAllRentals), new { id = rental.Id }, rental);
        }



        [Authorize(Roles = "Admin, User")]
        [HttpPost("return")]
        public ActionResult<Rental> ReturnRental(int id)
        {
            // Fetch exisiting rental
            var existingRental = _unitOfWork.Rentals.GetRentalEntityById(id);
            if (existingRental == null)
                return NotFound($"No customer found with id {id}");

            // Mark as returned
            existingRental.ReturnedAt = DateTime.UtcNow;
            existingRental.Status = Rental.RentalStatus.Returned;

            _unitOfWork.Rentals.UpdateRental(existingRental);
            _unitOfWork.Complete();

            // Return DTO
            var rentalDTO = new RentalDTO
            {
                Id = existingRental.Id,
                IssuedAt = existingRental.IssuedAt,
                DueDate = existingRental.DueDate,
                ReturnedAt = existingRental.ReturnedAt,
                ReturnNotes = existingRental.ReturnNotes,
                Status = existingRental.Status.ToString(),
                EquipmentCondition = existingRental.EquipmentCondition.ToString(),
                EquipmentStatus = existingRental.EquipmentStatus.ToString(),
                CustomerId = existingRental.CustomerId,
                CustomerName = existingRental.Customer.Name,
                EquipmentId = existingRental.EquipmentId,
                EquipmentName = existingRental.Equipment.Name,
                Equipment = existingRental.Equipment 
            };

            return CreatedAtAction(nameof(GetRentalsById), new { id = existingRental.Id }, existingRental);
        }



        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/extend")]
        public ActionResult<Rental> ExtendRental(int id, [FromBody] Rental rental)
        {
            var existingRental = _unitOfWork.Rentals.GetRentalEntityById(id);
            if (existingRental == null)
                return NotFound($"No customer found with id {id}");

            existingRental.IssuedAt = rental.IssuedAt;
            existingRental.DueDate = rental.DueDate;
            existingRental.ReturnedAt = rental.ReturnedAt;
            existingRental.ReturnNotes = rental.ReturnNotes;
            existingRental.Status = rental.Status;
            existingRental.EquipmentCondition = rental.EquipmentCondition;
            existingRental.EquipmentStatus = rental.EquipmentStatus;
            existingRental.CustomerId = rental.CustomerId; 
            existingRental.EquipmentId = rental.EquipmentId;
            existingRental.Equipment = rental.Equipment;

            _unitOfWork.Rentals.UpdateRental(existingRental);
            _unitOfWork.Complete();
            return Ok(existingRental);
        }




        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult<Rental> DeleteRental(int id)
        {
            var existingRental = _unitOfWork.Rentals.GetRentalEntityById(id);
            if (existingRental == null)
                return NotFound($"No customer found with id {id}");

            _unitOfWork.Rentals.DeleteRental(existingRental);
            _unitOfWork.Complete();

            var resultDto = new RentalDTO
            {
                Id = existingRental.Id,
                IssuedAt = existingRental.IssuedAt,
                DueDate = existingRental.DueDate,
                ReturnedAt = existingRental.ReturnedAt,
                ReturnNotes = existingRental.ReturnNotes,
                Status = existingRental.Status.ToString(),
                EquipmentCondition = existingRental.EquipmentCondition.ToString(),
                EquipmentStatus = existingRental.EquipmentStatus.ToString(),
                CustomerId = existingRental.CustomerId,
                CustomerName = existingRental.Customer.Name,
                EquipmentId = existingRental.EquipmentId,
                EquipmentName = existingRental.Equipment.Name,
                Equipment = existingRental.Equipment
            };

            return Ok(existingRental);
        }



        private int GetUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
        }
    }
}
