using ClosedXML.Excel;
using POSystem.Application.Constants;
using POSystem.Application.Interfaces;
using POSystem.Domain.Entities;

namespace POSystem.Application.Services;

public class OrderExporter : IOrderExporter
{
    public async Task<MemoryStream> ExportAsync(Order order)
    {
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Orders");

            AddStyle(worksheet);
            WriteSummaryHeader(worksheet);
            WriteSummaryData(worksheet, order);
            WriteOrderDetail(worksheet, order);

            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return stream;
        }
    }

    #region Private Methods

    private void AddStyle(IXLWorksheet worksheet)
    {
        worksheet.RowHeight = OrderExporterConfig.RowHeight;
        worksheet.ColumnWidth = OrderExporterConfig.ColumnWidth;
        worksheet.Columns().AdjustToContents();

        worksheet.Row(OrderExporterConfig.SummaryModelRow).Style.Font.Bold = true;
        worksheet.Row(OrderExporterConfig.SummaryModelRow).Style.Font.Shadow = true;
        worksheet.Row(OrderExporterConfig.SummaryModelRow).Style.Font.FontColor = XLColor.DarkBlue;

        worksheet.Row(OrderExporterConfig.DetailModelRow).Style.Font.Bold = true;
        worksheet.Row(OrderExporterConfig.DetailModelRow).Style.Font.Shadow = true;
        worksheet.Row(OrderExporterConfig.DetailModelRow).Style.Font.FontColor = XLColor.DarkBlue;

        worksheet.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
    }

    private void WriteSummaryHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell(OrderExporterConfig.SummaryModelRow, OrderExporterConfig.RefIdColumn).Value = OrderExporterConfig.RefIdHeader;

        worksheet.Cell(OrderExporterConfig.SummaryModelRow, OrderExporterConfig.PoNoColumn).Value = OrderExporterConfig.PoNoHeader;

        worksheet.Cell(OrderExporterConfig.SummaryModelRow, OrderExporterConfig.PoDateColumn).Value = OrderExporterConfig.PoDateHeader;

        worksheet.Cell(OrderExporterConfig.SummaryModelRow, OrderExporterConfig.SupplierColumn).Value = OrderExporterConfig.SupplierHeader;

        worksheet.Cell(OrderExporterConfig.SummaryModelRow, OrderExporterConfig.ExpectedDateColumn).Value = OrderExporterConfig.ExpectedDateHeader;
    }

    private void WriteSummaryData(IXLWorksheet worksheet, Order order)
    {
        worksheet.Cell(OrderExporterConfig.SummaryModelRow + 1, OrderExporterConfig.RefIdColumn).Value = order.ReferenceId;

        worksheet.Cell(OrderExporterConfig.SummaryModelRow + 1, OrderExporterConfig.PoNoColumn).Value = order.PurchaseOrderNo;

        worksheet.Cell(OrderExporterConfig.SummaryModelRow + 1, OrderExporterConfig.PoDateColumn).Value = order.PlacedAtUtc.ToString("yyyy-mm-dd");

        worksheet.Cell(OrderExporterConfig.SummaryModelRow + 1, OrderExporterConfig.SupplierColumn).Value = order.SupplierId;

        worksheet.Cell(OrderExporterConfig.SummaryModelRow + 1, OrderExporterConfig.ExpectedDateColumn).Value = order.ExpectedDate?.ToString("yyyy-mm-dd");
    }

    private void WriteOrderDetail(IXLWorksheet worksheet, Order order)
    {
        worksheet.Cell(OrderExporterConfig.DetailModelRow, OrderExporterConfig.ItemNameColumn).Value = OrderExporterConfig.ItemNameHeader;
        worksheet.Cell(OrderExporterConfig.DetailModelRow, OrderExporterConfig.ItemQuantityColumn).Value = OrderExporterConfig.ItemQuantityHeader;
        worksheet.Cell(OrderExporterConfig.DetailModelRow, OrderExporterConfig.ItemRateColumn).Value = OrderExporterConfig.ItemQuantityHeader;

        var offset = 1;
        foreach (var item in order.Items)
        {
            worksheet.Cell(OrderExporterConfig.DetailModelRow + offset,
                OrderExporterConfig.ItemNameColumn).Value = item.Name;
            worksheet.Cell(OrderExporterConfig.DetailModelRow + offset,
                OrderExporterConfig.ItemQuantityColumn).Value = item.Quantity;
            worksheet.Cell(OrderExporterConfig.DetailModelRow + offset,
                OrderExporterConfig.ItemRateColumn).Value = item.Rate;
            offset++;
        }
    }

    #endregion
}
