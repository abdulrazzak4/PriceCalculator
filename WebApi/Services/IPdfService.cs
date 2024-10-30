using DinkToPdf;
using System.Drawing.Imaging;
using System.Drawing.Printing;

namespace PriceCalculator.Api.Services
{
    public interface IPdfService
    {
        byte[] GeneratePdf(string htmlContent);
    }

    public class PdfService : IPdfService
    {
        public byte[] GeneratePdf(string htmlContent)
        {
            // Example using DinkToPdf to generate a PDF from HTML
            var converter = new BasicConverter(new PdfTools());
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings
                {
                    ColorMode = DinkToPdf.ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = DinkToPdf.PaperKind.A4
                },
                Objects = { new ObjectSettings { HtmlContent = htmlContent } }
            };

            return converter.Convert(doc);
        }
    }

}
