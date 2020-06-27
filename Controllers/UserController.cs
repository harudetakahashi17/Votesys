using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Votesys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly AppDbContext _db;

        public UserController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Users.ToListAsync() });
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var item = await _db.Users.FirstOrDefaultAsync(u => u.UserID == id);
            if (item == null)
            {
                return NotFound();
            }
            return Json(item);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _db.Users.FirstOrDefaultAsync(u => u.UserID == id);
            if (item == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _db.Users.Remove(item);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Item has been deleted" });
        }
    }
}