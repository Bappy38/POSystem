namespace POSystem.Application.Constants;

public static class OrderExporterConfig
{
    public const int SummaryModelRow = 1;
    public const int RefIdColumn = 1;
    public const int PoNoColumn = 2;
    public const int PoDateColumn = 3;
    public const int SupplierColumn = 4;
    public const int ExpectedDateColumn = 5;

    public const int DetailModelRow = 5;
    public const int ItemNameColumn = 2;
    public const int ItemQuantityColumn = 3;
    public const int ItemRateColumn = 4;

    public const string RefIdHeader = "REF. ID";
    public const string PoNoHeader = "PO NO";
    public const string PoDateHeader = "PO DATE";
    public const string SupplierHeader = "SUPPLIER";
    public const string ExpectedDateHeader = "EX.DATE";
    public const string ItemNameHeader = "ITEM NAME";
    public const string ItemQuantityHeader = "QUANTITY";
    public const string ItemRateHeader = "RATE";

    public const int RowHeight = 20;
    public const int ColumnWidth = 20;

    public const string ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    public const string ExportFileName = "Orders.xlsx";

    public const string DateFormat = "yyyy-mm-dd";
}
