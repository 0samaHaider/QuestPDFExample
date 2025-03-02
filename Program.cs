using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace QuestPDFExample;

internal class Program
{
    private static void Main()
    {
        QuestPDF.Settings.License = LicenseType.Community;
        GeneratePdf("SalesReport.pdf");
        Console.WriteLine("PDF Generated Successfully.");
    }

    private static void GeneratePdf(string fileName)
    {
        var base64Image = GetBase64Image(); // Get image in Base64 format

        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(30);

                // Header with Image
                page.Header()
                    .Column(column =>
                    {
                        column.Item().AlignCenter().Image(Placeholders.Image(100, 50)); // Placeholder if no Base64
                        if (!string.IsNullOrEmpty(base64Image))
                        {
                            column.Item().AlignCenter().Image(base64Image).FitWidth();
                        }
                        column.Item().Text("Monthly Sales Report")
                            .Bold()
                            .FontSize(24)
                            .AlignCenter()
                            .FontColor(Colors.Blue.Darken2);
                    });

                page.Content()
                    .PaddingVertical(20)
                    .Column(column =>
                    {
                        column.Spacing(15);

                        // Sales Summary Section
                        column.Item().Text("Sales Summary")
                            .FontSize(18)
                            .Bold()
                            .FontColor(Colors.Green.Darken2);

                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Product").Bold();
                                header.Cell().Text("Quantity Sold").Bold();
                                header.Cell().Text("Revenue").Bold();
                                header.Cell().Text("Profit").Bold();
                            });

                            // Sample Data
                            AddTableRow(table, "Laptop", "120", "$120,000", "$30,000", Colors.White);
                            AddTableRow(table, "Smartphone", "250", "$75,000", "$20,000", Colors.Grey.Lighten3);
                            AddTableRow(table, "Tablet", "80", "$40,000", "$10,000", Colors.White);
                            AddTableRow(table, "Monitor", "60", "$18,000", "$5,000", Colors.Grey.Lighten3);
                        });

                        // Top Performers Section
                        column.Item().Text("Top Performers")
                            .FontSize(18)
                            .Bold()
                            .FontColor(Colors.Red.Darken2);

                        column.Item().Table(nestedTable =>
                        {
                            nestedTable.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            nestedTable.Header(header =>
                            {
                                header.Cell().Text("Product").Bold();
                                header.Cell().Text("Revenue").Bold();
                            });

                            // Sample Data
                            AddNestedTableRow(nestedTable, "Laptop", "$120,000", Colors.White);
                            AddNestedTableRow(nestedTable, "Smartphone", "$75,000", Colors.Grey.Lighten3);
                            AddNestedTableRow(nestedTable, "Tablet", "$40,000", Colors.White);
                        });

                        // Image Section (if Base64 Image Exists)
                        if (!string.IsNullOrEmpty(base64Image))
                        {
                            column.Item().Text("Company Sales Chart")
                                .FontSize(16)
                                .Bold()
                                .FontColor(Colors.Purple.Darken2);

                            column.Item().Image(base64Image).FitWidth();
                        }

                        // Disclaimer Section
                        column.Item().Text("Disclaimer: This report is generated for internal use only. " +
                                           "The data is subject to change based on final audits.")
                            .FontSize(10)
                            .Italic()
                            .FontColor(Colors.Grey.Darken1);
                    });

                // Footer with Page Numbering
                page.Footer()
                    .AlignCenter()
                    .Text(text =>
                    {
                        text.Span("Page ").FontColor(Colors.Grey.Darken2);
                        text.CurrentPageNumber().FontColor(Colors.Blue.Darken2);
                        text.Span(" of ").FontColor(Colors.Grey.Darken2);
                        text.TotalPages().FontColor(Colors.Blue.Darken2);
                    });
            });
        }).GeneratePdf(fileName);
    }

    private static void AddTableRow(TableDescriptor table, string product, string quantity, string revenue, string profit, string backgroundColor)
    {
        table.Cell().Background(backgroundColor).Text(product).FontSize(12);
        table.Cell().Background(backgroundColor).Text(quantity).FontSize(12);
        table.Cell().Background(backgroundColor).Text(revenue).FontSize(12).FontColor(Colors.Green.Darken2);
        table.Cell().Background(backgroundColor).Text(profit).FontSize(12).FontColor(Colors.Green.Darken2);
    }

    private static void AddNestedTableRow(TableDescriptor table, string product, string revenue, string backgroundColor)
    {
        table.Cell().Background(backgroundColor).Text(product).FontSize(12);
        table.Cell().Background(backgroundColor).Text(revenue).FontSize(12).FontColor(Colors.Green.Darken2);
    }

    private static string GetBase64Image()
    {
        const string imagePath = "chart.png"; // Change this to your actual image path

        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Image file not found, using placeholder.");
            return string.Empty;
        }

        var imageBytes = File.ReadAllBytes(imagePath);
        return $"data:image/png;base64,{Convert.ToBase64String(imageBytes)}";
    }
}