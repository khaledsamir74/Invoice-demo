using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IO;
using InvoiceDemo.Data;
using Document = iTextSharp.text.Document;
using PdfWriter = iTextSharp.text.pdf.PdfWriter;
using System.Collections;
using InvoiceDemo.Models;

namespace InvoiceDemo.PDF
{
    public class PDFGenerator
    {
        public void ViewPdfNewTap(IJSRuntime js, List<ValidationError> errors, string filename = "report.pdf")
        {

            js.InvokeVoidAsync("OpenPdfNewTab",
               filename,
               Convert.ToBase64String(PDFReport(errors))
               );

        }

        private byte[] PDFReport(List<ValidationError> errors)
        {
            //initializing
            var memoryStream = new MemoryStream();
            float margeLeft = 0.1f;
            float margeRight = 0.1f;
            float margeTop = 1.0f;
            float margeBottom = 1.0f;
            Document pdf = new Document(
                PageSize.A4,
                margeLeft.ToDpi(),
                margeRight.ToDpi(),
                margeTop.ToDpi(),
                margeBottom.ToDpi()
                );

            PdfWriter writer = PdfWriter.GetInstance(pdf, memoryStream);

            //PDF header
            var fontStyle = FontFactory.GetFont("Arial", 14, BaseColor.Black);
            var lableHeader = new Chunk("Error list in Excel sheet", fontStyle);
            HeaderFooter header = new HeaderFooter(new Phrase(lableHeader), false)
            {
                BackgroundColor = new BaseColor(System.Drawing.Color.LightGray),
                Alignment = Element.ALIGN_CENTER,
                Border = Rectangle.NO_BORDER
            };
            pdf.Header = header;

            //PDF Footer
            fontStyle = FontFactory.GetFont("Arial", 12, BaseColor.Black);
            var labelFooter = new Chunk(" ", fontStyle);
            HeaderFooter footer = new HeaderFooter(new Phrase(labelFooter), true)
            {
                BackgroundColor = new BaseColor(System.Drawing.Color.White),
                Alignment = Element.ALIGN_CENTER,
                Border = Rectangle.NO_BORDER
            };
            pdf.Footer = footer;

            //PDF Body
            pdf.Open();
            PdfPTable table = new PdfPTable(3);
            table.WidthPercentage = 90f;

            //Table Headres
            PdfPCell cell = new PdfPCell(new Phrase("Row"));
            cell.BackgroundColor = new BaseColor(System.Drawing.Color.LightCyan);
            cell.FixedHeight = 25f;
            cell.Width = 25f;
            cell.Border = Rectangle.BOX;
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Column"));
            cell.BackgroundColor = new BaseColor(System.Drawing.Color.LightCyan);
            cell.FixedHeight = 25f;
            cell.Width = 25f;
            cell.Border = Rectangle.BOX;
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Message"));
            cell.BackgroundColor = new BaseColor(System.Drawing.Color.LightCyan);
            cell.FixedHeight = 25f;
            cell.Width = 25f;
            cell.Border = Rectangle.BOX;
            table.AddCell(cell);
            table.HeaderRows = 1;

            //table Body
            foreach (var element in errors)
            {
                cell = new PdfPCell(new Phrase(element.Row));
                cell.BackgroundColor = new BaseColor(System.Drawing.Color.WhiteSmoke);
                cell.FixedHeight = 25f;
                cell.Width = 25f;
                cell.Border = Rectangle.BOTTOM_BORDER;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(element.Col));
                cell.BackgroundColor = new BaseColor(System.Drawing.Color.WhiteSmoke);
                cell.FixedHeight = 25f;
                cell.Width = 25f;
                cell.Border = Rectangle.BOTTOM_BORDER;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(element.Message));
                cell.BackgroundColor = new BaseColor(System.Drawing.Color.WhiteSmoke);
                cell.FixedHeight = 25f;
                cell.Width = 50f;
                cell.Border = Rectangle.BOTTOM_BORDER;
                table.AddCell(cell);
            }
            pdf.Add(table);
            pdf.Close();
            return memoryStream.ToArray();

        }
    }
}
