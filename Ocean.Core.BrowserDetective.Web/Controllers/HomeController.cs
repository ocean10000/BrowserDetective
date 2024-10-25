using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ocean.Core.BrowserDetective.Data.Context;
using Ocean.Core.BrowserDetective.Data.Models;
using Ocean.Core.BrowserDetective.Web.Models;
using System.Diagnostics;
using Ocean.Core.BrowserDetective.Extentions;


namespace Ocean.Core.BrowserDetective.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private Ocean.Core.BrowserDetective.Data.Context.HeaderContext HeaderContext;
        private Ocean.Core.BrowserDetective.Data.Context.BrowserCapsContext BrowserCapsContext;
        private List<Ocean.Core.BrowserDetective.Data.Models.Browser> BrowserList;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            if (string.IsNullOrEmpty(configuration.GetConnectionString("Headers")) == false)
            {
                HeaderContext = new Data.Context.HeaderContext(configuration.GetConnectionString("Headers"));
            }
            else
            {
                HeaderContext = new Data.Context.HeaderContext();
            }
            if (string.IsNullOrEmpty(configuration.GetConnectionString("BrowserCaps")) == false)
            {
                BrowserCapsContext = new Data.Context.BrowserCapsContext(configuration.GetConnectionString("BrowserCaps"));
            }
            else
            {
                BrowserCapsContext = new Data.Context.BrowserCapsContext();
            }
            BrowserList = BrowserCapsContext.Browsers.AsNoTracking().OrderBy(X => X.ParentId).ThenBy(X => X.Name).ThenBy(X => X.Type).ToList();
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
            ViewBag.BrowserNodes = BrowserNodes.OrderBy(X=>X.Text);
            ViewBag.parentURL = string.Empty;
            Browser Model = null;
            if (ID > 0)
            {
                Model = BrowserList.FirstOrDefault(X => X.Id == ID);
            }
            else
            {
                Model = new Browser() { Id = 0, Name = "Sample", Type = BrowserType.Browser, _logger = this._logger };
            }
            return View("BrowserNode", Model);
        }

        public IActionResult BrowserNodeUpSert(Browser Model)
        {
            if (Model.Id > 0)
                BrowserCapsContext.Browsers.Update(Model);
            else
                BrowserCapsContext.Browsers.Add(Model);

            BrowserCapsContext.SaveChanges();

            var m = BrowserList.FirstOrDefault(X => X.Id == Model.Id);

            if (m != null)
            {
                Model.InverseParent = m.InverseParent;
                Model.Identifications = m.Identifications;
                Model.Capabilities = m.Capabilities;
                Model.Captures = m.Captures;
                Model.Samples = m.Samples;
                Model._logger = m._logger;

                BrowserList.Remove(m);
            }
            BrowserList.Add(Model);

            return Redirect(Url.Action("BrowserNode", "Home", new { ID = Model.Id }));
        }

        public IActionResult DeleteBrowserNode(long ID)
        {
            var m = BrowserList.FirstOrDefault(X => X.Id == ID);
            if (m != null && m.InverseParent.Count == 0)
            {
                foreach (var m2 in m.Capabilities)
                {
                    BrowserCapsContext.Capabilities.Remove(m2);
                }
                foreach (var m2 in m.Captures)
                {
                    BrowserCapsContext.Captures.Remove(m2);
                }
                foreach (var m2 in m.Identifications)
                {
                    BrowserCapsContext.Identifications.Remove(m2);
                }
                foreach (var m2 in m.Samples)
                {
                    BrowserCapsContext.SampleHeaders.Remove(m2);
                }
                BrowserCapsContext.Browsers.Remove(m);

                BrowserCapsContext.SaveChanges();
            }
            return Redirect("/");
        }

        [Route("~/Home/Identification/{ID}/{BrowserId}/")]
        public IActionResult Identification(long ID, long BrowserId)
        {
            Data.Models.Identification ident;

            if (ID == 0 && BrowserId != 0)
            {
                ident = new Data.Models.Identification() { Id = 0, BrowserId = BrowserId, Match = string.Empty, NonMatch = string.Empty, Type = CaptureType.Header };
            }
            else
            {
                ident = BrowserCapsContext.Identifications.AsNoTracking().FirstOrDefault(X => X.Id == ID && X.BrowserId == BrowserId);
            }

            if (ident != null)
            {
                return View(ident);
            }

            return NotFound();
        }
        public IActionResult IdentificationUpSert(Data.Models.Identification Model)
        {
            if (Model.Id > 0)
                BrowserCapsContext.Identifications.Update(Model);
            else
                BrowserCapsContext.Identifications.Add(Model);

            BrowserCapsContext.SaveChanges();

            var m = BrowserList.FirstOrDefault(X => X.Id == Model.Id);

            if (m != null)
            {
                m.Identifications = BrowserCapsContext.Identifications.AsNoTracking().Where(X => X.BrowserId == Model.BrowserId).ToList();
            }

            return View("Identification", Model);
        }
        [Route("~/Home/Identification/Deleted/{ID}/")]
        public IActionResult DeleteIdentification(long ID)
        {
            var ident = BrowserCapsContext.Identifications.AsNoTracking().FirstOrDefault(X => X.Id == ID);

            if (ident != null)
            {
                var m = BrowserList.FirstOrDefault(X => X.Id == ident.BrowserId);
                if (m != null)
                {
                    m.Identifications.Remove(ident);
                    BrowserCapsContext.Identifications.Remove(ident);
                    BrowserCapsContext.SaveChanges();

                    return Redirect(Url.Action("BrowserNode", "Home", new { ID = m.Id }));
                }
            }

            return NotFound();
        }

        [Route("~/Home/Captures/{ID}/{BrowserId}/")]
        public IActionResult Captures(long ID, long BrowserId)
        {
            Data.Models.Capture ident;

            if (ID == 0 && BrowserId != 0)
            {
                ident = new Data.Models.Capture() { Id = 0, BrowserId = BrowserId, Match = string.Empty, NonMatch = string.Empty, Type = CaptureType.Header };
            }
            else
            {
                ident = BrowserCapsContext.Captures.AsNoTracking().FirstOrDefault(X => X.Id == ID && X.BrowserId == BrowserId);
            }

            if (ident != null)
            {
                return View(ident);
            }

            return NotFound();
        }
        public IActionResult CapturesUpSert(Data.Models.Capture Model)
        {
            if (Model.Id > 0)
                BrowserCapsContext.Captures.Update(Model);
            else
                BrowserCapsContext.Captures.Add(Model);

            BrowserCapsContext.SaveChanges();

            var m = BrowserList.FirstOrDefault(X => X.Id == Model.Id);

            if (m != null)
            {
                m.Captures = BrowserCapsContext.Captures.AsNoTracking().Where(X => X.BrowserId == Model.BrowserId).ToList();
            }

            return View("Captures", Model);
        }
        [Route("~/Home/Captures/Deleted/{ID}/")]
        public IActionResult DeleteCaptures(long ID)
        {
            var ident = BrowserCapsContext.Captures.AsNoTracking().FirstOrDefault(X => X.Id == ID);

            if (ident != null)
            {
                var m = BrowserList.FirstOrDefault(X => X.Id == ident.BrowserId);
                if (m != null)
                {
                    m.Captures.Remove(ident);
                    BrowserCapsContext.Captures.Remove(ident);
                    BrowserCapsContext.SaveChanges();
                    return Redirect(Url.Action("BrowserNode", "Home", new { ID = m.Id }));
                }
            }

            return NotFound();
        }

        [Route("~/Home/Capabilities/{ID}/{BrowserId}/")]
        public IActionResult Capabilities(long ID, long BrowserId)
        {
            Data.Models.Capability Cap;

            if (ID == 0 && BrowserId != 0)
            {
                Cap = new Data.Models.Capability() { Id = 0, BrowserId = BrowserId, Name = string.Empty, Value = string.Empty };
            }
            else
            {
                Cap = BrowserCapsContext.Capabilities.AsNoTracking().FirstOrDefault(X => X.Id == ID && X.BrowserId == BrowserId);
            }

            if (Cap != null)
            {
                return View(Cap);
            }

            return NotFound();
        }
        public IActionResult CapabilitiesUpSert(Data.Models.Capability Model)
        {
            if (Model.Id > 0)
            {
                var m1 = BrowserCapsContext.Capabilities.AsNoTracking().FirstOrDefault(X => X.Id == Model.Id);
                if (m1 != null)
                {
                    m1.Name = Model.Name;
                    m1.Value = Model.Value;
                    m1.BrowserId = Model.BrowserId;

                    BrowserCapsContext.Capabilities.Update(m1);
                }
            }
            else
            {
                BrowserCapsContext.Capabilities.Add(Model);
            }
            BrowserCapsContext.SaveChanges();

            var m = BrowserList.FirstOrDefault(X => X.Id == Model.Id);

            if (m != null)
            {
                m.Capabilities = BrowserCapsContext.Capabilities.AsNoTracking().Where(X => X.BrowserId == Model.BrowserId).ToList();
            }

            return View("Capabilities", Model);
        }
        [Route("~/Home/Capabilities/Deleted/{ID}/")]
        public IActionResult DeleteCapabilities(long ID)
        {
            var ident = BrowserCapsContext.Capabilities.AsNoTracking().FirstOrDefault(X => X.Id == ID);

            if (ident != null)
            {
                var m = BrowserList.FirstOrDefault(X => X.Id == ident.BrowserId);
                if (m != null)
                {
                    m.Capabilities.Remove(ident);
                    BrowserCapsContext.Capabilities.Remove(ident);
                    BrowserCapsContext.SaveChanges();
                    return Redirect(Url.Action("BrowserNode", "Home", new { ID = m.Id }));
                }
            }

            return NotFound();
        }

        public IActionResult SamplesList()
        {
            var Model = BrowserCapsContext.SampleHeaders.Where(X => X.Name == "User-Agent").ToList();

            return View(Model);
        }
        [Route("~/Home/Sample/{ID}/{BrowserId}/")]
        public IActionResult Samples(long ID, long BrowserId)
        {
            Data.Models.SampleHeader Cap;

            if (ID == 0 && BrowserId != 0)
            {
                Cap = new Data.Models.SampleHeader() { Id = 0, BrowserId = BrowserId, Name = string.Empty, Value = string.Empty };
            }
            else
            {
                Cap = BrowserCapsContext.SampleHeaders.AsNoTracking().FirstOrDefault(X => X.Id == ID && X.BrowserId == BrowserId);
            }

            if (Cap != null)
            {
                return View("Samples", Cap);
            }

            return NotFound();
        }
        public IActionResult SamplesUpSert(Data.Models.SampleHeader Model)
        {
            if (Model.Id > 0)
                BrowserCapsContext.SampleHeaders.Update(Model);
            else
                BrowserCapsContext.SampleHeaders.Add(Model);

            BrowserCapsContext.SaveChanges();

            var m = BrowserList.FirstOrDefault(X => X.Id == Model.Id);

            if (m != null)
            {
                m.Samples = BrowserCapsContext.SampleHeaders.AsNoTracking().Where(X => X.BrowserId == Model.BrowserId).ToList();
            }

            return View("Samples", Model);
        }
        [Route("~/Home/Sample/Deleted/{ID}/")]
        public IActionResult DeleteSamples(long ID)
        {
            var ident = BrowserCapsContext.SampleHeaders.AsNoTracking().FirstOrDefault(X => X.Id == ID);

            if (ident != null)
            {
                var m = BrowserList.FirstOrDefault(X => X.Id == ident.BrowserId);
                if (m != null)
                {
                    m.Samples.Remove(ident);
                    BrowserCapsContext.SampleHeaders.Remove(ident);
                    BrowserCapsContext.SaveChanges();
                    return Redirect(Url.Action("BrowserNode", "Home", new { ID = m.Id }));
                }
            }

            return NotFound();
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

        public IActionResult UserAgent()
        {
            return View("UserAgent");
        }

        public IActionResult UserAgentLookup(string UserAgent)
        {
            var DefaultBrowser = BrowserList.Where(x => x.Name == "Default").FirstOrDefault();

            Microsoft.AspNetCore.Http.IHeaderDictionary header = new Microsoft.AspNetCore.Http.HeaderDictionary();
            header.Append("User-Agent", UserAgent);
            ViewBag.Header = header;

            var Model = DefaultBrowser.Process(header);

            return View("UserAgent", Model);
        }

        public IActionResult UserAgentLookup2(long ID, long BrowserId)
        {
            var DefaultBrowser = BrowserList.Where(x => x.Id == BrowserId).FirstOrDefault();
            if (DefaultBrowser != null)
            {
                var sample = DefaultBrowser.Samples.FirstOrDefault(X => X.Id == ID);
                if (sample != null)
                {
                    Microsoft.AspNetCore.Http.IHeaderDictionary header = new Microsoft.AspNetCore.Http.HeaderDictionary();
                    header.Append("User-Agent", sample.Value);

                    ViewBag.Header = header;

                    DefaultBrowser = BrowserList.Where(x => x.Name == "Default").FirstOrDefault();

                    var Model = DefaultBrowser.Process(header);

                    return View("UserAgent", Model);
                }
            }


            return NotFound();
        }

        public IActionResult UserAgentLookup3(long ID)
        {
            ViewBag.Raw_ID = ID;
            var h = HeaderContext.Headers.Where(X => X.Raw_ID == ID).ToList();
            var DefaultBrowser = BrowserList.Where(x => x.Name == "Default").FirstOrDefault();

            if (DefaultBrowser != null)
            {
                Microsoft.AspNetCore.Http.IHeaderDictionary header = new Microsoft.AspNetCore.Http.HeaderDictionary();
                foreach (var x in h)
                {
                    header.Append(x.Name,x.Value);
                }

                ViewBag.Header = header;

                var Model = DefaultBrowser.Process(header);

                return View("UserAgent", Model);
            }


            return NotFound();
        }

        private List<SelectListItem> BrowserNodes
        {
            get
            {
                List<SelectListItem> item = new List<SelectListItem>();
                foreach (var node in BrowserList)
                {
                    item.Add(new SelectListItem() { Text = node.Name, Value = node.Id.ToString() });
                }

                return item;
            }
        }

        public IActionResult HeaderList(long Page = 1)
        {
            long Offset = (Page * 50) - 50;
            var Model = HeaderContext.Raw.FromSql($"Select * From RawHeadersData LIMIT 100 OFFSET {Offset}").ToList();

            return View(Model);
        }
    }
}
