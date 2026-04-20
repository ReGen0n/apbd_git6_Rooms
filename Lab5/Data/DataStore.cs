using Lab5.Models;

namespace Lab5.Data
{
    public static class DataStore
    {
        public static List<Room> Rooms { get; } = new()
        {
            new Room
            {
                Id = 1,
                Name = "Lab 101",
                BuildingCode = "A",
                Floor = 1,
                Capacity = 20,
                HasProjector = true,
                IsActive = true
            },
            new Room
            {
                Id = 2,
                Name = "Lab 204",
                BuildingCode = "B",
                Floor = 2,
                Capacity = 24,
                HasProjector = true,
                IsActive = true
            },
            new Room
            {
                Id = 3,
                Name = "Room 305",
                BuildingCode = "C",
                Floor = 3,
                Capacity = 12,
                HasProjector = false,
                IsActive = false
            }
        };

        public static List<Reservation> Reservations { get; } = new()
        {
            new Reservation
            {
                Id = 1,
                RoomId = 2,
                OrganizerName = "Anna Kowalska",
                Topic = "HTTP and REST Workshop",
                Date = new DateOnly(2026, 5, 10),
                StartTime = new TimeOnly(10, 0, 0),
                EndTime = new TimeOnly(12, 30, 0),
                Status = "confirmed"
            }
        };
    }
}