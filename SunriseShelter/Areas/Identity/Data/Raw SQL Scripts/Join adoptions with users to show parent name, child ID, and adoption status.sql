SELECT AspNetUsers.FirstName + ' ' + AspNetUsers.LastName AS ParentName,
       Adoption.ChildrenId,
       Adoption.Status
FROM Adoption
INNER JOIN AspNetUsers ON Adoption.ParentId = AspNetUsers.Id;