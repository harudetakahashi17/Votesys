using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Votesys;
using Votesys.Models;

namespace Votesys.Pages.Admin.Categories
{
    public class CreateModel : PageModel
    {
        private readonly Votesys.AppDbContext _context;

        public CreateModel(Votesys.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
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
            return Page();
        }

        [BindProperty]
        public CategoryModel CategoryModel { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Categories.Add(CategoryModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
