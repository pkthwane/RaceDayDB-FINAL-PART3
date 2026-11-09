USE RaceDayDB;
GO

-- ===================================================================
-- Drop existing tables (if any exist to prevent duplicates and errors
-- ===================================================================
DROP TABLE IF EXISTS RaceSponsor;
DROP TABLE IF EXISTS Sponsor;
DROP TABLE IF EXISTS Result;
DROP TABLE IF EXISTS Registration;
DROP TABLE IF EXISTS Category;
DROP TABLE IF EXISTS Race;
DROP TABLE IF EXISTS Runner;
DROP TABLE IF EXISTS Organizer;
GO

CREATE TABLE Organizer (
    OrgID INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    ContactInfo VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL
);

CREATE TABLE Runner (
    RunnerID INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Age INT CHECK (Age > 0),
    Gender VARCHAR(10),
    ContactInfo VARCHAR(100),
    Email VARCHAR(100) NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL
);

CREATE TABLE Race (
    RaceID INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Description VARCHAR(500),
    EventType VARCHAR(20),
    Date DATE NOT NULL,
    Location VARCHAR(100),
    Distance DECIMAL(5,2),
    OrgID INT NOT NULL,
    FOREIGN KEY (OrgID) REFERENCES Organizer(OrgID)
);

CREATE TABLE Category (
    CategoryID INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName VARCHAR(100) NOT NULL,
    RaceID INT NOT NULL,
    FOREIGN KEY (RaceID) REFERENCES Race(RaceID)
);

CREATE TABLE Registration (
    RegID INT IDENTITY(1,1) PRIMARY KEY,
    RunnerID INT NOT NULL,
    RaceID INT NOT NULL,
    CategoryID INT NOT NULL,
    PaymentStatus VARCHAR(20) DEFAULT 'Pending',
    FOREIGN KEY (RunnerID) REFERENCES Runner(RunnerID),
    FOREIGN KEY (RaceID) REFERENCES Race(RaceID),
    FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID)
);

CREATE TABLE Result (
    ResultID INT IDENTITY(1,1) PRIMARY KEY,
    RunnerID INT NOT NULL,
    RaceID INT NOT NULL,
    FinishTime TIME,
    Position INT,
    FOREIGN KEY (RunnerID) REFERENCES Runner(RunnerID),
    FOREIGN KEY (RaceID) REFERENCES Race(RaceID)
);

CREATE TABLE Sponsor (
    SponsorID INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL
);

CREATE TABLE RaceSponsor (
    RaceSponsorID INT IDENTITY(1,1) PRIMARY KEY,
    RaceID INT NOT NULL,
    SponsorID INT NOT NULL,
    SponsorshipAmount DECIMAL(10,2),
    FOREIGN KEY (RaceID) REFERENCES Race(RaceID),
    FOREIGN KEY (SponsorID) REFERENCES Sponsor(SponsorID)
);

INSERT INTO Organizer (Name, ContactInfo, Email, PasswordHash)
VALUES ('Speed Events SA', 'speed@events.co.za', 'contact@speed.co.za', 'HASHED_PASSWORD'),
       ('Marathon Masters', 'info@mm.co.za', 'support@mm.co.za', 'HASHED_PASSWORD');

INSERT INTO Runner (Name, Age, Gender, ContactInfo, Email, PasswordHash)
VALUES ('Preston M', 20, 'Male', 'preston@mail.com', 'preston@mail.com', 'HASHED_PASSWORD'),
       ('Lerato N', 23, 'Female', 'lerato@mail.com', 'lerato@mail.com', 'HASHED_PASSWORD');

INSERT INTO Race (Name, Description, EventType, Date, Location, Distance, OrgID)
VALUES ('Pretoria Fun Run', 'Annual community fun run event', 'Run', '2026-09-15', 'Pretoria', 5.00, 1),
       ('Gauteng Marathon', 'Provincial marathon race', 'Run', '2026-10-01', 'Johannesburg', 42.20, 2),
       ('Spring Sprint', 'Fast paced spring race event', 'Run', '2026-11-10', 'Centurion', 10.00, 1);

INSERT INTO Category (CategoryName, RaceID)
VALUES ('Under 20', 1),
       ('Open', 1),
       ('10km', 3),
       ('Marathon', 2);

INSERT INTO Registration (RunnerID, RaceID, CategoryID, PaymentStatus)
VALUES (1, 1, 1, 'Paid'),
       (2, 2, 4, 'Pending');

INSERT INTO Result (RunnerID, RaceID, FinishTime, Position)
VALUES (1, 1, '00:25:30', 1),
       (2, 2, '03:45:10', 5);

INSERT INTO Sponsor (Name, Email)
VALUES ('Nike SA', 'nike@sa.com'),
       ('Coca-Cola', 'coke@za.com');

INSERT INTO RaceSponsor (RaceID, SponsorID, SponsorshipAmount)
VALUES (1, 1, 50000.00),
       (2, 2, 30000.00);

ALTER TABLE Organizer
ADD Role VARCHAR(20) DEFAULT 'Organizer';

ALTER TABLE Runner
ADD Role VARCHAR(20) DEFAULT 'Runner';

SELECT * FROM Organizer;
SELECT * FROM Runner;
SELECT * FROM Race;
SELECT * FROM Category;
SELECT * FROM Registration;
SELECT * FROM Result;
SELECT * FROM Sponsor;
SELECT * FROM RaceSponsor;


