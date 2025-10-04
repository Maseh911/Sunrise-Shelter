SELECT Adoption.*
FROM Adoption
INNER JOIN (
    SELECT ParentId, MAX(AdoptionDate) AS LatestDate
    FROM Adoption
    WHERE Status = 'Completed'
    GROUP BY ParentId
) AS LatestAdoptions ON Adoption.ParentId = LatestAdoptions.ParentId
                    AND Adoption.AdoptionDate = LatestAdoptions.LatestDate;