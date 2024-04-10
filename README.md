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

- API BASE URL: <https://bookaroom-staging.onrender.com/api/v1>
  - Test endpoint: <https://bookaroom-staging.onrender.com/api/v1/rooms/1>

- The API is deployed via dockerhub to render, which contains the repository for the images. An automatic deploy is triggered with a webhook to render whenever a new image is pushed to the hub.

## NOTE

You can only use the API from the deployed api base url. See [API DEPLOYMENTS](#api-documentation-and-docker). If you would like to build the image or run the application, a PostgreSQL database and a Redis instance must be available, locally or via a container.

This sections should be configured in your appsettings.Environment.json file:

```json
{
    "Redis": {
    "Host": "",
    "Port": "6379",
    "Name": "bookaroom-redis"
  },
  "ConnectionStrings": {
    "PostgresqlDatabase": "Host=; Database=bookaroom_db; Username=; Password=",
    "PostgresqlDatabaseStaging": "Host=; Database=bookaroom_db; Username=; Password="
  },
}
```

Then run, locally:

```bash
dotnet run --launch-profile DevelopmentProfileName
```

The `DevelopmentProfileName` is the `launchprofile.json` profile for development.

## Call to stardom

If you find this project interesting, I'm open to collaborations and other interesting projects with C#/.NET.

Thank you for visiting.

I'm open to criticisms and suggestions on improvements I could make.
