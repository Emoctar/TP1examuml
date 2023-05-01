using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SelectPdf;

namespace TP1examuml.Controllers
{
    public class PDFController : Controller
    {
        private readonly ICompositeViewEngine _viewEngine;


        public PDFController(ICompositeViewEngine viewEngine)
        {
            _viewEngine = viewEngine;
        }

        public string Index()
        {

            HtmlToPdf converter = new HtmlToPdf();
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            converter.Options.MarginLeft = 10;
            converter.Options.MarginRight = 10;
            converter.Options.MarginTop = 20;
            converter.Options.MarginBottom = 20;


            string valeurhtml = this.RenderViewToString("Afficher", new object());
            // PdfDocument doc = converter.ConvertUrl("https://github.com/github");
             PdfDocument doc = converter.ConvertHtmlString(valeurhtml);
            Random random = new Random();
            int rand = random.Next(1, 100000);
            string chemin =Directory.GetCurrentDirectory()+"\\wwwroot\\Ordonnance";
            if (!Directory.Exists  (chemin))
                Directory.CreateDirectory(chemin); 

            var cheminImage = Path.Combine(chemin, "Ordonnance" + rand + ".pdf");

            doc.Save(cheminImage);
            doc.Close();
            return "Le fichier pdf est crees";
        }
        public IActionResult Afficher()
        {
            return View();
        }

        //Methodes

        public string RenderViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var writer = new StringWriter())
            {
                var viewEngineResult = _viewEngine.FindView(ControllerContext,
                    viewName, false);
                var viewContext = new ViewContext(
                    ControllerContext,
                    viewEngineResult.View,
                    ViewData,
                    TempData,
                    writer,
                    new HtmlHelperOptions()
                );
                viewEngineResult.View.RenderAsync(viewContext);
                return writer.GetStringBuilder().ToString();
            }
        }
    }

    
}
