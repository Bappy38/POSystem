DECLARE @SupplierIdA INT, @SupplierIdB INT;
SELECT @SupplierIdA = Id FROM Suppliers WHERE Name = 'Supplier A';
SELECT @SupplierIdB = Id FROM Suppliers WHERE Name = 'Supplier B';

INSERT INTO Orders (ReferenceId, PurchaseOrderNo, PlacedAtUtc, SupplierId, ExpectedDate, Remark)
VALUES
    (1, 'X01', GETUTCDATE(), @SupplierIdA, DATEADD(DAY, 7, GETUTCDATE()), 'Urgent'),
    (2, 'X02', GETUTCDATE(), @SupplierIdB, DATEADD(DAY, 10, GETUTCDATE()), 'Standard');