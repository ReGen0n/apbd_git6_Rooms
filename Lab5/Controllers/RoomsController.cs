using Lab5.Data;
using Lab5.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Room>> GetAll(
            [FromQuery] int? minCapacity,
            [FromQuery] bool? hasProjector,
            [FromQuery] bool? isActive)
        {
            var rooms = DataStore.Rooms.AsEnumerable();

            if (minCapacity.HasValue)
                rooms = rooms.Where(r => r.Capacity >= minCapacity.Value);

            if (hasProjector.HasValue)
                rooms = rooms.Where(r => r.HasProjector == hasProjector.Value);
            if (isActive.HasValue)
                rooms = rooms.Where(r => r.IsActive == isActive.Value);

            return Ok(rooms);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Room> GetById(int id)
        {
            var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);

            if (room == null)
                return NotFound(new { message = $"Room with id {id} not found." });

            return Ok(room);
        }

        [HttpGet("building/{buildingCode}")]
        public ActionResult<IEnumerable<Room>> GetByBuildingCode(string buildingCode)
        {
            var rooms = DataStore.Rooms
                .Where(r => r.BuildingCode.Equals(buildingCode, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!rooms.Any())
                return NotFound(new { message = $"No rooms found in building {buildingCode}." });

            return Ok(rooms);
        }

        [HttpPost]
        public ActionResult<Room> Create(Room room)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            room.Id = DataStore.Rooms.Any() ? DataStore.Rooms.Max(r => r.Id) + 1 : 1;
            DataStore.Rooms.Add(room);

            return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, Room updatedRoom)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var existingRoom = DataStore.Rooms.FirstOrDefault(r => r.Id == id);

            if (existingRoom == null)
                return NotFound(new { message = $"Room with id {id} not found." });

            existingRoom.Name = updatedRoom.Name;
            existingRoom.BuildingCode = updatedRoom.BuildingCode;
            existingRoom.Floor = updatedRoom.Floor;
            existingRoom.Capacity = updatedRoom.Capacity;
            existingRoom.HasProjector = updatedRoom.HasProjector;
            existingRoom.IsActive = updatedRoom.IsActive;

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);

            if (room == null)
                return NotFound(new { message = $"Room with id {id} not found." });

            var hasReservations = DataStore.Reservations.Any(r => r.RoomId == id);

            if (hasReservations)
                return Conflict(new { message = "Cannot delete room because related reservations exist." });

            DataStore.Rooms.Remove(room);
            return NoContent();
        }
    }
}