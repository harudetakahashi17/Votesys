using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Votesys;
using Votesys.Models;

namespace Votesys.Pages.Admin.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Votesys.AppDbContext _context;

        public IndexModel(Votesys.AppDbContext context)
        {
            _context = context;
        }

        public IList<CategoryModel> CategoryModel { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var sesi = base.HttpContext.Session.GetString("_Email");
            if (sesi == null)
            {
                return RedirectToPage("/AdminLogin");
            }
            if (sesi != "admin@example.co.id")
            {
                return RedirectToPage("/Index");
            }
            ViewData["Session"] = sesi;
            CategoryModel = await _context.Categories.ToListAsync();
            return Page();
        }
    }
}
