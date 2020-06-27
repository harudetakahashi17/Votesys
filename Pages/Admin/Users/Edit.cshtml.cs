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

namespace Votesys.Pages.Admin.Users
{
    public class EditModel : PageModel
    {
        private readonly Votesys.AppDbContext _context;

        public EditModel(Votesys.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserModel UserModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
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
            if (id == null)
            {
                return NotFound();
            }

            UserModel = await _context.Users.FirstOrDefaultAsync(m => m.UserID == id);

            if (UserModel == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(UserModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserModelExists(UserModel.UserID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UserModelExists(int id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }
    }
}
