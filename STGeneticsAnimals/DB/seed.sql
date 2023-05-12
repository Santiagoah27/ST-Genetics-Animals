INSERT INTO Animal (Name, Breed, BirthDate, Sex, Price, Status)
VALUES ('Bessie', 'Holstein', '2020-01-01', 'Female', 1500.00, 'Active');

--Insert 5000 animals in Animal Table
DECLARE @animalCounter int = 1;

WHILE (@animalCounter <= 5000)
BEGIN
    INSERT INTO Animal(Name, Breed, BirthDate, Sex, Price, Status)
    VALUES (
        CONCAT('Animal', @animalCounter), -- Name
        'Breed' + CAST((ABS(CHECKSUM(NEWID())) % 10) + 1 AS VARCHAR), -- Breed
        DATEADD(YEAR, -((ABS(CHECKSUM(NEWID())) % 10) + 1), GETDATE()), -- BirthDate
        CASE WHEN (ABS(CHECKSUM(NEWID())) % 2) = 0 THEN 'Male' ELSE 'Female' END, -- Sex
        ((ABS(CHECKSUM(NEWID())) % 1000) + 1) * 1.0, -- Precio
        CASE WHEN (ABS(CHECKSUM(NEWID())) % 2) = 0 THEN 'Active' ELSE 'Inactive' END -- Estado
    );

    SET @animalCounter = @animalCounter + 1;
END;