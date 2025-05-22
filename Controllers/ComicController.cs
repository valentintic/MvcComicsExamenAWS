using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.CommandLine;
using MvcComicsExamen.Models;
using MvcComicsExamen.Repositories;
using MvcComicsExamen.Services;

namespace MvcComicsExamen.Controllers
{
    public class ComicController : Controller
    {

        private RepositoryComics repo;
        private ServiceStorageS3 serviceS3;

        public ComicController(RepositoryComics repo, ServiceStorageS3 serviceS3)
        {
            this.repo = repo;
            this.serviceS3 = serviceS3;

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
        public async Task<IActionResult> Create(Comic comic, IFormFile ImageFile)
        {
            try
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    string fileName = Path.GetFileName(ImageFile.FileName);
                    using (var stream = ImageFile.OpenReadStream())
                    {
                        await this.serviceS3.UploadFileAsync(fileName, stream);
                    }
                    comic.Imagen = fileName;
                }

                await this.repo.AddComic(comic);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error uploading image: " + ex.Message);
                return View(comic);
            }
        }


    }
}
