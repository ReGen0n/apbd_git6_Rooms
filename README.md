# Room Reservation API

This is a simple ASP.NET Core Web API for managing rooms and reservations.

## Features

- Create, update, delete rooms
- Create, update, delete reservations
- Business rules:
    - Cannot reserve a non-existing room
    - Cannot reserve an inactive room
    - No overlapping reservations
    - Cannot delete room with reservations

## Endpoints

### Rooms
- GET /api/rooms
- GET /api/rooms/{id}
- GET /api/rooms/building/{buildingCode}
- POST /api/rooms
- PUT /api/rooms/{id}
- DELETE /api/rooms/{id}

### Reservations
- GET /api/reservations
- GET /api/reservations/{id}
- POST /api/reservations
- PUT /api/reservations/{id}
- DELETE /api/reservations/{id}

## Technologies

- ASP.NET Core
- C#
- REST API
- Postman (testing)

## Author

Your Name