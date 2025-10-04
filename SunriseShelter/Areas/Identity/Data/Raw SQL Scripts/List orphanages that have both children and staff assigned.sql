SELECT Orphanage.Name
FROM Orphanage
WHERE Orphanage.OrphanageId IN (SELECT DISTINCT Children.OrphanageId FROM Children)
  AND Orphanage.OrphanageId IN (SELECT DISTINCT Staff.OrphanageId FROM Staff);