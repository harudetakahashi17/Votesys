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

namespace Votesys.Pages.User.Votes
{
    public class IndexModel : PageModel
    {
        private readonly Votesys.AppDbContext _context;

        public IndexModel(Votesys.AppDbContext context)
        {
            _context = context;
        }

        public IList<VoteItemModel> VoteItemModel { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var sesi = base.HttpContext.Session.GetString("_Email");
            if(sesi == null)
            {
                return RedirectToPage("/UserLogin");
            }
            ViewData["Session"] = sesi;
            VoteItemModel = await _context.VotingItems.ToListAsync();
            return Page();
        }
    }
}
