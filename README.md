# BookARoom - A robust room booking API application

The idea behind BookARoom is to help accomodation-seeking users book rooms easily. An enterprise that seeks to use this API will be able to:

- Add new rooms for users to book
- Update amenities of each room
- Provide an interface for a user to book a room or multiple rooms, set checkin date and checkout date

## Entity Relationship Diagram

![ERD Diagram](./config/BookARoom-3.png)

- **rooms - amenities**: many-to-many relationship *(one room can have many amenities, and one amenity can be connected to many rooms)*.

- **rooms - bookings**: many-to-many relationship *(one room can have many bookings, and one booking can have many rooms reserved)*.

- **guests - bookings**: one-to-many relationship *(one guest can have many bookings, and one booking can only have one guest at a time)*.

The entity relationship diagram was brought to life with [dbdiagram.io](dbdiagram.io). It's an awesome tool.

## API Documentation and Docker

The API is fully documented on postman with example request and responses.

Link: [BookARoom API Documentation](https://documenter.getpostman.com/view/27156707/2sA35LVzEk)

A container image is also available here:

```bash
docker run -p 127.0.0.1:5098:5098 taiwopeter/bookaroom:development
```

## Call to stardom

If you find this project interesting, I'm open to collaborations and other interesting projects with C#/.NET.

Thank you for visiting.
