SELECT Children.Name AS ChildName,
       Orphanage.Name AS OrphanageName,
       Staff.FirstName + ' ' + Staff.LastName AS Caretaker
FROM Children
INNER JOIN Orphanage ON Children.OrphanageId = Orphanage.OrphanageId
INNER JOIN Staff ON Children.OrphanageId = Staff.OrphanageId
WHERE Staff.Role = 'Caretaker';