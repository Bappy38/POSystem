DECLARE @SupplierIdA INT, @SupplierIdB INT;
SELECT @SupplierIdA = Id FROM Suppliers WHERE Name = 'Supplier A';
SELECT @SupplierIdB = Id FROM Suppliers WHERE Name = 'Supplier B';

INSERT INTO Orders (ReferenceId, PlacedAtUtc, SupplierId, ExpectedDate, Remark)
VALUES
    ('001', GETUTCDATE(), @SupplierIdA, DATEADD(DAY, 7, GETUTCDATE()), 'Urgent'),
    ('002', GETUTCDATE(), @SupplierIdB, DATEADD(DAY, 10, GETUTCDATE()), 'Standard');