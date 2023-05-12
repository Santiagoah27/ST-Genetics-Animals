CREATE DATABASE STGenetics;

USE STGenetics;

CREATE TABLE Animal
(
    AnimalId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50),
    Breed NVARCHAR(50),
    BirthDate DATE,
    Sex NVARCHAR(10),
    Price DECIMAL(10, 2),
    Status NVARCHAR(10)
);

CREATE TABLE Orders
(
    OrderId INT PRIMARY KEY IDENTITY(1,1),
    TotalAmount DECIMAL(18,2),
    FreightCharge DECIMAL(18,2)
);

CREATE TABLE OrderItems
(
    OrderItemId INT PRIMARY KEY IDENTITY(1,1),
    OrderId INT FOREIGN KEY REFERENCES Orders(OrderId),
    AnimalId INT FOREIGN KEY REFERENCES Animal(AnimalId),
    Quantity INT,
    Price DECIMAL(18,2)
);