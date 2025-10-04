SELECT ParentId, COUNT(*) AS AdoptionCount
FROM Adoption
GROUP BY ParentId
HAVING COUNT(*) > 1;