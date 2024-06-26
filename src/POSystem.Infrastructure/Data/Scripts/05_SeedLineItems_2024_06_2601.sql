DECLARE @OrderId1 INT, @OrderId2 INT;
SELECT @OrderId1 = Id FROM Orders WHERE ReferenceId = '001';
SELECT @OrderId2 = Id FROM Orders WHERE ReferenceId = '002';

INSERT INTO LineItems (OrderId, Name, Quantity, Rate)
VALUES
    (@OrderId1, 'Product A', 10, 5.99),
    (@OrderId1, 'Product B', 5, 8.49),
    (@OrderId2, 'Product C', 20, 3.99);