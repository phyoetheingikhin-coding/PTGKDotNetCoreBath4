using Microsoft.AspNetCore.Mvc;

namespace PTGKDotNetCoreBath4.Ajax.Controllers
{
    public class BlogCotroller : Controller
    {
        [ActionName("Index")]
        public IActionResult BlogEntity()
        {
            return View("BlogEntity");
        }
    }
}
