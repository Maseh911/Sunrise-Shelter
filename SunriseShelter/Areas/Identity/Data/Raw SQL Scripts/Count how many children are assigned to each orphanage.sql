SELECT OrphanageId, COUNT(*) AS TotalChildren
FROM Children
GROUP BY OrphanageId;