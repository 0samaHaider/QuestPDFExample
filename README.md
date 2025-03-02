# QuestPDF Sales Report Generator

## Overview
This project generates a **Sales Report PDF** using **QuestPDF**. It includes a structured layout with tables, an embedded image (Base64), and a footer with dynamic page numbers.

## Features
- 📊 **Sales Summary Table**
- 🏆 **Top Performers Table**
- 📷 **Embedded Image Support (Base64)**
- 📄 **Page Numbering in Footer**
- 🎨 **Styled Sections with Colors**

## Prerequisites
- .NET 8.0 or later
- [QuestPDF NuGet Package](https://www.nuget.org/packages/QuestPDF/)

## Installation
1. Clone the repository:
   ```sh
   git clone https://github.com/your-repo/QuestPDF-SalesReport.git
   cd QuestPDF-SalesReport
   ```
2. Install dependencies:
   ```sh
   dotnet add package QuestPDF
   ```
3. Ensure you have an image (`chart.png`) in the project directory or update the `imagePath` in `GetBase64Image()`.

## Usage
Run the application to generate the PDF:
```sh
dotnet run
```
The **SalesReport.pdf** will be generated in the project folder.

## File Structure
```
📂 QuestPDFExample
 ├── 📄 Program.cs        # Main application logic
 ├── 📄 README.md         # Project documentation
 ├── 📄 chart.png         # Image for report (optional)
 ├── 📄 SalesReport.pdf   # Generated report
```

## Customization
### Change Sales Data
Modify the `AddTableRow` and `AddNestedTableRow` functions in `Program.cs` to update the sales data.

### Update Report Header
Modify the `page.Header()` section to update the **title, logo, or colors**.

### Change Image
Replace `chart.png` in the project folder, or modify `GetBase64Image()` to use a different image.

## Example Output
After running the program, a **Sales Report PDF** is generated with structured tables and an optional embedded image.

---

## License
This project uses the **MIT License**.

## Contact
For issues or suggestions, open an issue in this repository.
