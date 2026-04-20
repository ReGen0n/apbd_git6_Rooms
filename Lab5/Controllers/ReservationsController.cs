using Lab5.Data;
using Lab5.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Reservation>> GetAll()
        {
            return Ok(DataStore.Reservations);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Reservation> GetById(int id)
        {
            var reservation = DataStore.Reservations.FirstOrDefault(r => r.Id == id);

            if (reservation == null)
                return NotFound(new { message = $"Reservation with id {id} not found." });

            return Ok(reservation);
        }

        [HttpPost]
        public ActionResult<Reservation> Create(Reservation reservation)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            if (reservation.StartTime >= reservation.EndTime)
            {
                return BadRequest(new { message = "StartTime must be earlier than EndTime." });
            }

            var room = DataStore.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);

            if (room == null)
            {
                return BadRequest(new { message = "Reservation cannot be added because the room does not exist." });
            }

            if (!room.IsActive)
            {
                return BadRequest(new { message = "Reservation cannot be added because the room is inactive." });
            }

            var overlappingReservation = DataStore.Reservations.Any(r =>
                r.RoomId == reservation.RoomId &&
                r.Date == reservation.Date &&
                reservation.StartTime < r.EndTime &&
                reservation.EndTime > r.StartTime
            );

            if (overlappingReservation)
            {
                return Conflict(new { message = "Reservation overlaps with an existing reservation for this room." });
            }

            reservation.Id = DataStore.Reservations.Any() ? DataStore.Reservations.Max(r => r.Id) + 1 : 1;
            DataStore.Reservations.Add(reservation);

            return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, Reservation updatedReservation)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            if (updatedReservation.StartTime >= updatedReservation.EndTime)
            {
                return BadRequest(new { message = "StartTime must be earlier than EndTime." });
            }

            var existingReservation = DataStore.Reservations.FirstOrDefault(r => r.Id == id);
            if (existingReservation == null)
            {
                return NotFound(new { message = $"Reservation with id {id} not found." });
            }

            var room = DataStore.Rooms.FirstOrDefault(r => r.Id == updatedReservation.RoomId);
            if (room == null)
            {
                return BadRequest(new { message = "Reservation cannot be updated because the room does not exist." });
            }

            if (!room.IsActive)
            {
                return BadRequest(new { message = "Reservation cannot be updated because the room is inactive." });
            }

            var overlappingReservation = DataStore.Reservations.Any(r =>
                r.Id != id &&
                r.RoomId == updatedReservation.RoomId &&
                r.Date == updatedReservation.Date &&
                updatedReservation.StartTime < r.EndTime &&
                updatedReservation.EndTime > r.StartTime
            );

            if (overlappingReservation)
            {
                return Conflict(new { message = "Updated reservation overlaps with an existing reservation." });
            }

            existingReservation.RoomId = updatedReservation.RoomId;
            existingReservation.OrganizerName = updatedReservation.OrganizerName;
            existingReservation.Topic = updatedReservation.Topic;
            existingReservation.Date = updatedReservation.Date;
            existingReservation.StartTime = updatedReservation.StartTime;
            existingReservation.EndTime = updatedReservation.EndTime;
            existingReservation.Status = updatedReservation.Status;

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var reservation = DataStore.Reservations.FirstOrDefault(r => r.Id == id);

            if (reservation == null)
                return NotFound(new { message = $"Reservation with id {id} not found." });

            DataStore.Reservations.Remove(reservation);
            return NoContent();
        }
    }
}