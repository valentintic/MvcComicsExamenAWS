using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.CommandLine;
using MvcComicsExamen.Models;
using MvcComicsExamen.Repositories;

namespace MvcComicsExamen.Controllers
{
    public class ComicController : Controller
    {

        private RepositoryComics repo;
        public ComicController(RepositoryComics repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<Comic> comics = await this.repo.GetComics();
            return View(comics);
        }

        public async Task<IActionResult> Details(int id)
        {
            Comic comic = await this.repo.GetComic(id);
            if (comic == null)
            {
                return NotFound();
            }
            return View(comic);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Comic comic)
        {
            await this.repo.AddComic(comic);
            return RedirectToAction("Index");
            return View(comic);
        }
    }
}
