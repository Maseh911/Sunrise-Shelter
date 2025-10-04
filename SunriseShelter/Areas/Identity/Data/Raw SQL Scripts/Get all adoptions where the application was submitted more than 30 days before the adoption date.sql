SELECT *
FROM Adoption
WHERE AdoptionDate IS NOT NULL
  AND DATEDIFF(DAY, ApplicationDate, AdoptionDate) > 30;