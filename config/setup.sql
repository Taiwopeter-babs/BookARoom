-- This sql script functions for desgining the database schema for postgresql

-- rooms table
CREATE TABLE IF NOT EXISTS rooms (
    id SERIAL PRIMARY KEY,
    name VARCHAR(128) NOT NULL,
    description VARCHAR(128) NOT NULL,
    maximumOccupancy INT NOT NULL DEFAULT 2,
    numberAvailable INT NOT NULL DEFAULT 1,
    price DECIMAL(10, 2) NOT NULL DEFAULT 0,
    createdAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP

);

-- amenities table
CREATE TABLE IF NOT EXISTS amenities (
    id SERIAL PRIMARY KEY,
    name VARCHAR(256) NOT NULL
);

-- rooms_amenities table
CREATE TABLE IF NOT EXISTS rooms_amenities (
    roomId INT,
    amenityId INT,
    CONSTRAINT fk_room FOREIGN KEY(roomId) REFERENCES rooms(id),
    CONSTRAINT fk_tag FOREIGN KEY(amenityId) REFERENCES amenities(id),
    PRIMARY KEY (roomId, amenityId)
);

-- guests
CREATE TABLE IF NOT EXISTS guests (
    id SERIAL PRIMARY KEY,
    firstName VARCHAR(128) NOT NULL,
    lastName VARCHAR(128) NOT NULL,
    email VARCHAR(128) NOT NULL,
    country VARCHAR(128) NOT NULL,
    city VARCHAR(128) NOT NULL,
    state VARCHAR(128) NOT NULL,
    createdAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- bookings
CREATE TABLE IF NOT EXISTS bookings (
    id SERIAL PRIMARY KEY,
    checkinDate TIMESTAMP NOT NULL,
    checkoutDate TIMESTAMP NOT NULL,
    createdAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    guestId INT NOT NULL,
    CONSTRAINT fk_guest FOREIGN KEY(guestId) REFERENCES guests(id)
);

-- rooms_bookings table
CREATE TABLE IF NOT EXISTS rooms_bookings (
    roomId INT,
    bookingId INT,
    CONSTRAINT fk_room FOREIGN KEY(roomId) REFERENCES rooms(id),
    CONSTRAINT fk_tag FOREIGN KEY(bookingId) REFERENCES bookings(id),
    PRIMARY KEY (roomId, bookingId)
);