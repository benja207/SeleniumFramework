using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Kernel.Geom;
using iText.Layout.Element;
using iText.IO.Image;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Kernel.Font;
using NUnit.Framework;
using iText.Layout.Properties;
using iText.Layout.Borders;

namespace mds.Framework
{
    class Reporting
    {
        private const string logo_airbus = @"\Resources\logo_airbus.png";
        private const string logo_sogeti = @"\Resources\logo_sogeti.png";
        private static ExtentReports extent;
        private static ExtentTest test;
        private static string projectPath;

        private static string reportPath;

        private static Document documento;
        private static Paragraph newline;

        private static int intStepNum;

        public static void StartReport()
        {
            string pth = AppContext.BaseDirectory;
            string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            projectPath = new Uri(actualPath).LocalPath;

            reportPath = projectPath + "Output/";

            extent = new ExtentReports();
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(reportPath);
            extent.AttachReporter(htmlReporter);
        }

        public static void CreateTestLog()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            FlushReport();

            intStepNum = 1;
            //PDF
            String functionality = TestContext.CurrentContext.Test.ClassName.Split('.')[2];
            String evidencespath = reportPath + functionality;
            if (!System.IO.File.Exists(evidencespath))
            {
                System.IO.Directory.CreateDirectory(evidencespath);
            }
            PdfWriter pdfWriter = new PdfWriter(evidencespath + "//" + TestContext.CurrentContext.Test.Name + ".pdf");
            PdfDocument pdf = new PdfDocument(pdfWriter);
            documento = new Document(pdf, PageSize.A3);
            newline = new Paragraph(new Text("\n"));


            Image imgSog = new Image(ImageDataFactory.Create(projectPath + logo_sogeti));
            imgSog.ScaleToFit(125f, 60F);

            Image imgAirbus = new Image(ImageDataFactory.Create(projectPath + logo_airbus));
            imgAirbus.ScaleToFit(115f, 50F);


            Table tblHeader = new Table(UnitValue.CreatePercentArray(8)).UseAllAvailableWidth();
            Cell cell1 = new Cell();
            cell1.Add(imgSog);
            cell1.SetBorder(Border.NO_BORDER);
            Cell cell3 = new Cell().SetBorder(Border.NO_BORDER);
            Cell cell4 = new Cell().SetBorder(Border.NO_BORDER);
            Cell cell5 = new Cell().SetBorder(Border.NO_BORDER);
            Cell cell6 = new Cell().SetBorder(Border.NO_BORDER);
            Cell cell7 = new Cell().SetBorder(Border.NO_BORDER);
            Cell cell8 = new Cell().SetBorder(Border.NO_BORDER);
            Cell cell2 = new Cell().Add(new Paragraph().SetTextAlignment(TextAlignment.RIGHT));
            cell2.Add(imgAirbus);
            cell2.SetBorder(Border.NO_BORDER);
            tblHeader.AddCell(cell1);
            tblHeader.AddCell(cell3);
            tblHeader.AddCell(cell4);
            tblHeader.AddCell(cell5);
            tblHeader.AddCell(cell6);
            tblHeader.AddCell(cell7);
            tblHeader.AddCell(cell8);
            tblHeader.AddCell(cell2);
            documento.Add(tblHeader);

            LineSeparator ls = new LineSeparator(new SolidLine());
            documento.Add(ls);
            documento.Add(newline);
            var paragraph = new Paragraph("Automated Execution: " + TestContext.CurrentContext.Test.Name).SetFontSize(20);
            documento.Add(paragraph);

        }

        public static void FlushReport()
        {
            extent.Flush();
        }

        public static void Report(string description)
        {
            Reporter(description, TestStatus.OK);
        }
        public static void Reporter(string description, TestStatus teststatus)
        {
            String ssPath;

            switch (teststatus)
            {
                case TestStatus.OK:
                    test.Pass(description);
                    break;
                case TestStatus.KO:
                    test.Fail(description);
                    break;
            }
            FlushReport();

            ssPath = BrowserFactory.TakeScreenShot();
            test.AddScreenCaptureFromPath(ssPath);
            documento.Add(newline);

            PdfFont font = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.TIMES_ROMAN);
            PdfFont bold = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.TIMES_BOLD);
            Text stepnum = new Text("STEP " + intStepNum.ToString() + ": ").SetFont(bold).SetFontSize(16);
            Text stepdesc = new Text(description).SetFont(font).SetFontSize(16);
            var paragraph = new Paragraph().Add(stepnum).Add(stepdesc);
            documento.Add(paragraph);
            Image img = new Image(ImageDataFactory.Create(ssPath));
            img.ScaleToFit(600, 600);
            documento.Add(img);
            intStepNum++;
        }

        public static void ClosePDFDoc(string error)
        {
            PdfFont statusfont = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.TIMES_BOLD);
            PdfFont errordescfont = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.TIMES_ROMAN);
            Text statusreported;
            Text errordesc = null;
            Paragraph paragraph = null;

            documento.Add(newline);

            if (error == null)
            {
                statusreported = new Text("TEST RESULT OK").SetFont(statusfont).SetFontColor(iText.Kernel.Colors.DeviceRgb.GREEN).SetFontSize(20);
                paragraph = new Paragraph().Add(statusreported);
            }
            else
            {
                statusreported = new Text("TEST RESULT KO: ").SetFont(statusfont).SetFontColor(iText.Kernel.Colors.DeviceRgb.RED).SetFontSize(20);
                errordesc = new Text(error).SetFont(errordescfont).SetFontSize(14);
                paragraph = new Paragraph().Add(statusreported).Add(errordesc);
            }

            documento.Add(paragraph);
            documento.Close();
        }

        public enum TestStatus
        {
            OK,
            KO
        }
    }
}
