SELECT ProductCategories.Name, COUNT(Products.Id), AVG(Price)
FROM ProductCategories
LEFT OUTER JOIN Products ON CategoryId = ProductCategories.Id
GROUP BY ProductCategories.Name