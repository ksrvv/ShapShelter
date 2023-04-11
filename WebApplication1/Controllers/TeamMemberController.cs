using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Composition;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TeamMemberController : Controller
    {
        private ApplicationContext _context;
        IWebHostEnvironment _appEnvironment;
      
        private readonly ILogger<TeamMemberController> _logger;

        public TeamMemberController(ILogger<TeamMemberController> logger, ApplicationContext context, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _context = context;//db
            _appEnvironment = appEnvironment;
        }
        //Team 
        public async Task<IActionResult> Team()
        {
            return View(await _context.TeamMembers.ToListAsync());
        }
        public IActionResult CreateTeamMember()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateTeamMember(TeamMember teamMember, IFormFile uploadedFile)
        { 
            string fileName= uploadedFile.FileName;
            if (uploadedFile != null)
            {

                string path = "/images/team/" + fileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

            }
            teamMember.Image = fileName;
            _context.TeamMembers.Add(teamMember);
            await _context.SaveChangesAsync();
            return RedirectToAction("Team");
        }
        public async Task<IActionResult> EditTeamMember(int? id)
        {
            if (id != null)
            {
                TeamMember teamMember = await _context.TeamMembers.FirstOrDefaultAsync(p => p.Id == id);

                

                if (teamMember != null)
                    return View(teamMember);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditTeamMember(TeamMember teamMember, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                string fileName = uploadedFile.FileName;
                if (teamMember.Image != fileName)
                {
                    teamMember.Image = fileName;
                    string path = "/images/team/" + fileName;
                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }
                }

            }
            _context.TeamMembers.Update(teamMember);

            await _context.SaveChangesAsync();
            return RedirectToAction("Team");
        }
        [HttpGet]
        [ActionName("DeleteTeamMember")]
        public async Task<IActionResult> ConfirmDeleteTeamMember(int? id)
        {
            if (id != null)
            {
                TeamMember teamMember = await _context.TeamMembers.FirstOrDefaultAsync(p => p.Id == id);
                if (teamMember != null)
                    return View(teamMember);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTeamMember(int? id)
        {
            if (id != null)
            {
                TeamMember teamMember = new TeamMember { Id = id.Value };
                _context.Entry(teamMember).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return RedirectToAction("Team");
            }
            return NotFound();
        }

       

    }
}
