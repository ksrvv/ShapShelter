using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AnimalController : Controller
    {
        private ApplicationContext _context;

        private readonly ILogger<AnimalController> _logger;
        IWebHostEnvironment _appEnvironment;

        public AnimalController(ILogger<AnimalController> logger, ApplicationContext context, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _context = context;//db
            _appEnvironment = appEnvironment;
        }
        //Animals
        public async Task<IActionResult> Animals()
        {
            return View(await _context.Animals.ToListAsync());
        }
        public IActionResult CreateAnimal()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAnimal(Animal animal, IFormFile uploadedFile)
        {

            string fileName = uploadedFile.FileName;
            if (uploadedFile != null)
            {

                string path = "/images/animals/" + fileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

            }
            animal.Image = fileName;
            _context.Animals.Add(animal);
            await _context.SaveChangesAsync();
            return RedirectToAction("Animals");
        }
        public async Task<IActionResult> EditAnimal(int? id)
        {
            if (id != null)
            {
                Animal animal = await _context.Animals.FirstOrDefaultAsync(p => p.Id == id);
                if (animal != null)
                    return View(animal);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditAnimal(Animal animal, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                string fileName = uploadedFile.FileName;
                if (animal.Image != fileName)
                {
                    animal.Image = fileName;
                    string path = "/images/animals/" + fileName;
                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }
                }

            }

            _context.Animals.Update(animal);
            await _context.SaveChangesAsync();
            return RedirectToAction("Animals");
        }
        [HttpGet]
        [ActionName("DeleteAnimal")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Animal animal = await _context.Animals.FirstOrDefaultAsync(p => p.Id == id);
                if (animal != null)
                    return View(animal);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAnimal(int? id)
        {
            if (id != null)
            {
                Animal user = new Animal { Id = id.Value };
                _context.Entry(user).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return RedirectToAction("Animals");
            }
            return NotFound();
        }


    }
}
