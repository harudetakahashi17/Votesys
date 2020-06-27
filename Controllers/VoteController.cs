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
    public class VoteController : Controller
    {
        private readonly AppDbContext _db;

        public VoteController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Vote.ToListAsync() });
        }

        [HttpGet("{id}", Name = "GetVoteById")]
        public async Task<IActionResult> GetVoteById(int id)
        {
            var item = await _db.Vote.FirstOrDefaultAsync(u => u.VoteID == id);
            if (item == null)
            {
                return NotFound();
            }
            return Json(item);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _db.Vote.FirstOrDefaultAsync(u => u.VoteID == id);
            if (item == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _db.Vote.Remove(item);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Item has been deleted" });
        }
    }
}