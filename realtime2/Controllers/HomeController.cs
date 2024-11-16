using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using realtime2.Models;
using System.Diagnostics;

namespace realtime2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DemoContext _context;
        private readonly IHubContext<myhub> _hubContext;
        public HomeController(ILogger<HomeController> logger,DemoContext context, IHubContext<myhub> hubContext)
        {
            _logger = logger;
            _context = context;
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Product() { 
            var model = _context.Products;
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var model = _context.Products.Find(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Product prod) 
        {
            var model = _context.Products.Find(prod.Id);
            if (model != null)
            {
                   model.Stock = prod.Stock;
                    _context.SaveChanges();

                    
                   _hubContext.Clients.All.SendAsync("ReceiveQuantityUpdate",model.Id,model.Stock);
                

                return RedirectToAction("Product");
            }
            return View(prod);
        }

        public string Tong()
        {
            return "Hello";
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
