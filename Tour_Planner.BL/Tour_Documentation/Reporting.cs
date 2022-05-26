using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tour_Planner.DAL;
using Tour_Planner.Models;

namespace Tour_Planner.BL.Tour_Documentation
{
    public class Reporting
    {
        public void CreatePDFFromSelectedTour(Tour tour)
        {
            string pdfName = "";

            if (tour.Title != null)
            {
                pdfName = $"TourReport{tour.Title}.pdf";
            }
            else
            {
                // popup -> keine tour ausgewählt!
            }

            PdfWriter writer = new PdfWriter(pdfName);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            Paragraph titelHeader = new Paragraph($"Tour: {tour.Title}")
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN))
                .SetFontSize(20)
                .SetBold()
                .SetFontColor(ColorConstants.BLACK);
            //.SetBorderBottom() + Position to Center
            document.Add(titelHeader);

            Table tableTour = new Table(UnitValue.CreatePercentArray(2)).UseAllAvailableWidth();
            tableTour.AddHeaderCell(getHeaderCell("ID"));
            tableTour.AddHeaderCell(tour.Id.ToString());
            tableTour.SetFontSize(14).SetBackgroundColor(ColorConstants.WHITE);
            tableTour.AddCell(getHeaderCell("Description"));
            tableTour.AddCell(tour.Desciption ?? string.Empty);
            tableTour.AddCell(getHeaderCell("From"));
            tableTour.AddCell(tour.From ?? string.Empty);
            tableTour.AddCell(getHeaderCell("To"));
            tableTour.AddCell(tour.To ?? string.Empty);
            tableTour.AddCell(getHeaderCell("Transport Type"));
            tableTour.AddCell(tour.TransportType ?? string.Empty);
            tableTour.AddCell(getHeaderCell("Distance[km]"));
            tableTour.AddCell(tour.TourDistance ?? string.Empty);
            tableTour.AddCell(getHeaderCell("Estimated Time i[ss]"));
            tableTour.AddCell(tour.EstimatedTime ?? string.Empty);
            /*table.AddCell(getHeaderCell("Route Image path"));
            //table.AddCell(tour.RouteImagePath);
            table.AddCell(getHeaderCell("Session"));
            //table.AddCell(tour.Session);
            table.AddCell(getHeaderCell("Boundingbox"));
            //table.AddCell(tour.BoundingBox);*/
            document.Add(tableTour);


            Paragraph logsHeader = new Paragraph($"Tour logs: ")
               .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN))
               .SetFontSize(16)
               .SetBold()
               .SetFontColor(ColorConstants.BLACK);
            document.Add(logsHeader);

            Table tableLogs = new Table(UnitValue.CreatePercentArray(2)).UseAllAvailableWidth();
            foreach (TourLog log in tour.Logs)
            {
                tableLogs.AddCell(getHeaderCell("ID"));
                tableLogs.AddCell(log.Id.ToString());
                tableLogs.AddCell(getHeaderCell("Date"));
                tableLogs.AddCell(log.Date != null ? log.Date : System.DateTime.Today.ToShortDateString());                // aktuelles datum in Datum einfügen ? 
                tableLogs.AddCell(getHeaderCell("Time"));
                tableLogs.AddCell(log.Time.ToString());
                tableLogs.AddCell(getHeaderCell("Comment"));
                tableLogs.AddCell(log.Comment);
                tableLogs.AddCell(getHeaderCell("Difficulty"));
                tableLogs.AddCell(log.Difficulty.ToString());
                tableLogs.AddCell(getHeaderCell("Total Time"));
                tableLogs.AddCell(log.TotalTime.ToString());
                tableLogs.AddCell(getHeaderCell("Rating"));
                tableLogs.AddCell(log.Rating.ToString());
                tableLogs.AddCell("");
                tableLogs.AddCell("");
            }
            document.Add(tableLogs);

            //document.Add(new AreaBreak());
            Paragraph imageHeader = new Paragraph("Tour Image")
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN))
                    .SetFontSize(16)
                    .SetBold()
                    .SetFontColor(ColorConstants.BLACK);
            document.Add(imageHeader);

            ImageData imageData;
            if (String.IsNullOrEmpty(tour.RouteImagePath))
            {
                //for Mock Data
                imageData = ImageDataFactory.Create("..\\..\\.\\images\\car.png");
            }
            else
            {
                imageData = ImageDataFactory.Create(tour.RouteImagePath);
            }
            document.Add(new Image(imageData));

            document.Close();
        }
        private static Cell getHeaderCell(String s)
        {
            return new Cell().Add(new Paragraph(s)).SetBold().SetBackgroundColor(ColorConstants.GRAY);
        }

        public void exportTour(Tour tour)
        {
            string json = JsonSerializer.Serialize(tour);
            Console.WriteLine(json);
            File.WriteAllText(tour.Title + ".json", json);
        }

        public Tour importTour(string jsonObject)
        {
            Tour _tour = JsonSerializer.Deserialize<Tour>(jsonObject);
            return _tour;
        }
    }
}
