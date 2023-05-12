--List animals older than 2 years and female, sorted by name
SELECT *
FROM Animal
WHERE BirthDate <= DATEADD(year, -2, GETDATE()) AND Sex = 'Female'
ORDER BY Name;

--List the quantity of animals by sex
SELECT Sex, COUNT(*) as Quantity
FROM Animal
GROUP BY Sex;