SELECT OrphanageId, COUNT(*) AS StaffCount
FROM Staff
GROUP BY OrphanageId
HAVING COUNT(*) >= 1;