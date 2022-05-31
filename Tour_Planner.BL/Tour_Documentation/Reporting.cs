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
using Tour_Planner.BL.Exceptions;
using Tour_Planner.DAL;
using Tour_Planner.Models;

namespace Tour_Planner.BL.Tour_Documentation
{
    public class Reporting
    {
        public void CreatePDFFromSelectedTour(Tour tour)
        {
            string pdfName = $"TourReport_{tour.Title}.pdf";

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
                tableLogs.AddCell(log.Date != null ? log.Date : System.DateTime.Today.ToShortDateString());
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

        public void CreateSummary(List<Tour> allTourData = null)
        {
            List<Tour> tours = new List<Tour>();
            //Tour summary = new Tour();
            if (allTourData == null)
            {
                TourManager service = new TourManager();
                tours = service.GetTourData();
            }
            else
            {
                tours = allTourData;
            }


            string pdfNameSummary = $"ToursSummary.pdf";

            PdfWriter writer1 = new PdfWriter(pdfNameSummary);
            PdfDocument pdf1 = new PdfDocument(writer1);
            Document doc = new Document(pdf1);

            Paragraph titelHeader = new Paragraph($"Summary: ")
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN))
                .SetFontSize(20)
                .SetBold()
                .SetFontColor(ColorConstants.BLACK);
            doc.Add(titelHeader);

            //Top 3 most used tours -> count logs
            tours = tours.OrderByDescending(o => o.Logs.Count).ToList();

            Paragraph listHeader = new Paragraph("Favorites:")
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD))
                .SetFontSize(13)
                .SetBold()
                .SetFontColor(ColorConstants.BLACK);
            List list = new List()
                .SetSymbolIndent(12)
                .SetListSymbol("\u2022")
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD));
            foreach (Tour tour in tours)
            {
                list.Add(new ListItem($"{tour.Title}"));
            }
            doc.Add(listHeader);
            doc.Add(list);

            Paragraph titelTourStats = new Paragraph($"\nTour Statistics: ")
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN))
                .SetFontSize(15)
                .SetBold()
                .SetFontColor(ColorConstants.BLACK);
            doc.Add(titelTourStats);

            foreach (Tour tour in tours)
            {
                Paragraph titelTour = new Paragraph($"{tour.Title}")
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN))
                    .SetFontSize(15)
                    .SetBold()
                    .SetFontColor(ColorConstants.BLACK);
                doc.Add(titelTour);

                Paragraph TourData = new Paragraph($"From: {tour.From}\nTo: {tour.To}")
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN))
                    .SetFontSize(10)
                    //.SetBold()
                    .SetFontColor(ColorConstants.BLACK);
                doc.Add(TourData);

                Paragraph AverageData = new Paragraph($"Average Difficulty: {GetAverage(tour.Logs, 1)}\n" +
                    $"Average Time: {GetAverage(tour.Logs, 2)}\nAverage Rating: {GetAverage(tour.Logs, 3)}")
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN))
                    .SetFontSize(10)
                    //.SetBold()
                    .SetFontColor(ColorConstants.BLACK);
                doc.Add(AverageData);
            }
            doc.Close();
        }

        public double GetAverage(List<TourLog> logs, int type)
        {
            double sum = 0;
            switch (type)
            {
                case 1:
                    foreach (TourLog log in logs)
                    {
                        sum += log.Difficulty;
                    }
                    return sum / logs.Count;

                case 2:
                    foreach (TourLog log in logs)
                    {
                        sum += log.TotalTime;
                    }
                    return sum / logs.Count;

                case 3:
                    foreach (TourLog log in logs)
                    {
                        sum += log.Rating;
                    }
                    return sum / logs.Count;
                default:
                    Console.WriteLine("Fehler beim erstellen einer Zusammenfassung");
                    return 0;
            }
        }
    }
}
