using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Votesys;
using Votesys.Models;

namespace Votesys.Pages.Admin.VoteItems
{
    public class CreateModel : PageModel
    {
        private readonly Votesys.AppDbContext _context;

        public CreateModel(Votesys.AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var cat = await _context.Categories.ToListAsync();
            if (cat == null)
            {
                cat = new List<CategoryModel>();
            }
            ViewData["cat"] = cat;
            return Page();
        }

        [BindProperty]
        public VoteItemModel VoteItemModel { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.VotingItems.Add(VoteItemModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
