INSERT INTO Suppliers (Name, Email, Phone, Address)
VALUES
('Supplier 1', 'supplier1@example.com', '123-456-7890', 'Mirpur-13'),
('Supplier 2', 'supplier2@example.com', '234-567-8901', 'Banani'),
('Supplier 3', 'supplier3@example.com', '345-678-9012', 'Mohammedpur');

DECLARE @OrderId INT;

SET NOCOUNT ON;

DECLARE @i INT = 1;
WHILE @i <= 50
BEGIN
    INSERT INTO Orders (ReferenceId, PurchaseOrderNo, PlacedAtUtc, SupplierId, ExpectedDate, Remark)
    VALUES (@i, CONCAT('PO-', FORMAT(@i, '0000')), GETDATE() - @i, ((@i - 1) % 3) + 1, GETDATE() + @i, CONCAT('Remark for order ', @i));

    SET @OrderId = SCOPE_IDENTITY();

    INSERT INTO LineItems (OrderId, Name, Quantity, Rate)
    VALUES
    (@OrderId, CONCAT('Item A for Order ', @i), ROUND(RAND() * 10, 0) + 1, ROUND(RAND() * 90 + 10, 2)),
    (@OrderId, CONCAT('Item B for Order ', @i), ROUND(RAND() * 10, 0) + 1, ROUND(RAND() * 90 + 10, 2)),
    (@OrderId, CONCAT('Item C for Order ', @i), ROUND(RAND() * 10, 0) + 1, ROUND(RAND() * 90 + 10, 2));

    SET @i = @i + 1;
END;
