INSERT INTO Adoption (ApplicationDate, Status, ParentId, ChildrenId)
SELECT GETDATE(), 'Pending', AspNetUsers.Id, Children.ChildrenId
FROM AspNetUsers
JOIN Children ON Children.Status = 'Available'
WHERE AspNetUsers.Email = 'chloe.bennett@example.com'
  AND Children.ChildrenId = 1;