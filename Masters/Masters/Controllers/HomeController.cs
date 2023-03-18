using Masters.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Masters.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<IdentityUser> signInManager;
		private readonly FurnitureContext _context;
        private readonly UserManager<IdentityUser> userManager;

		public HomeController(ILogger<HomeController> logger , FurnitureContext context, SignInManager<IdentityUser> signInManager , UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var stores = _context.Stores.Where(obj=>obj.StatusPublish=="ok").ToList();
            return View(stores);
        }
        public IActionResult SpecificStoreCategory(int id)
        {
            //var category=_context.Categories.Where(obj=>obj.Id==id).ToList();
            // var category1 = _context.CategoryStores.Include(obj => obj.Cat).Where(obj => obj.StoreId == id).ToList(); 

            var category = _context.CategoryStores.Where(obj => obj.StoreId == id).Include(obj => obj.Cat).ToList();
            return View(category);  
        }

        public ActionResult AllProducts(int id)
        {
            var product = _context.Products.Where(obj => obj.Id == id).ToList();

            return View(product); 
        }

        public ActionResult SingleProduct(int id)
        {
            if (userManager.GetUserId(HttpContext.User) != null)
            {
                if (id != 0)
                {
                    var single = _context.Products.FirstOrDefault(obj => obj.Id == id);
                    return View(single);
                }
                else
                    return NotFound();
            }
            else
            {
                string returnUrl = Url.Content("~/Identity/Account/Login");

                return LocalRedirect(returnUrl);
            }
            return NotFound();


        }
        public IActionResult About()
        {
            return View();
        }
		public IActionResult Contact()
		{
			return View();
		}
        public IActionResult cart()
        {
            var all = _context.Carts.Include(obj=>obj.Product).ToList();
            return View(all);
        }
        public IActionResult AddToCart(int id)
        {
            var userId = userManager.GetUserId(HttpContext.User);
            Cart cart = new Cart();
            cart.ProductId = id;
            cart.UserId = userId;
            cart.Quantity = 1;
            _context.Carts.Add(cart);
            _context.SaveChanges();
            return RedirectToAction("cart","Home");
        }
        public IActionResult Remove(int id)
        {
            var cart = _context.Carts.Find(id);
            _context.Remove(cart);
            _context.SaveChanges();
            return RedirectToAction("cart", "Home");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        

        public async Task<IActionResult> LogOut()
        {
			await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
		}
    }
}