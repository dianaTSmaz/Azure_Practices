using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Azure_DataBase_Conection.Controllers
{
    
    public class MoviesController : Controller
    {
        [HttpGet("Movies")]
        public async Task<IActionResult> Index()
        {
            var MoviesList = await Azure_DataBase_Conection.Movies.ToListAsync();
            return View(MoviwaLis);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Bind[""]Movie Movie) 
        {

            _db.Add(Movie);
           await Azure_DataBase_Conection.SaveChangesasync();
            Redirect(nameof(Index))

            return View(joke);

        }
    }
}
