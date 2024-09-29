using Microsoft.AspNetCore.Mvc;
using Ocean.Core.BrowserDetective.Web.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Ocean.Core.BrowserDetective.Data.Models;


namespace Ocean.Core.BrowserDetective.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private Ocean.Core.BrowserDetective.Data.Context.BrowserCapsContext BrowserCapsContext;
        private List<Ocean.Core.BrowserDetective.Data.Models.Browser> BrowserList;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            BrowserCapsContext = new Data.Context.BrowserCapsContext(configuration.GetConnectionString("BrowserCaps"));
            BrowserList = BrowserCapsContext.Browsers.ToList();
            BrowserList.ForEach(X => X._logger = _logger);

            var identifications = BrowserCapsContext.Identifications.AsNoTracking().ToList();
            var captures = BrowserCapsContext.Captures.AsNoTracking().ToList();
            var capabilities = BrowserCapsContext.Capabilities.AsNoTracking().ToList();
            var sampleHeaders = BrowserCapsContext.SampleHeaders.AsNoTracking().ToList();


            //-----------------------------------------------------------------------------
            //Loops though all the Browsers and builds the Browser Tree.
            //-----------------------------------------------------------------------------
            foreach (var b in BrowserList)
            {
                b.Captures = captures.Where(c => c.BrowserId == b.Id).ToList();
                b.Identifications = identifications.Where(c => c.BrowserId == b.Id).ToList();
                b.Capabilities = capabilities.Where(c => c.BrowserId == b.Id).ToList();
                b.Samples = sampleHeaders.Where(c => c.BrowserId == b.Id).ToList();
                b.InverseParent = BrowserList.Where(c => c.ParentId == b.Id).ToList();
                if (b.ParentId != null && b.ParentId > 0)
                {
                    b.Parent = BrowserList.FirstOrDefault(x => x.Id == b.ParentId);
                    b.parentID = b.Parent.Name;
                }
            }
        }

        public IActionResult Index()
        {
            return View(BrowserList);
        }

        public IActionResult BrowserNode(long ID)
        {
            return View(BrowserList.FirstOrDefault(X=>X.Id == ID));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
