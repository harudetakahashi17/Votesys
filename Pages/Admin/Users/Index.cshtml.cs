﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Votesys;
using Votesys.Models;

namespace Votesys.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        private readonly Votesys.AppDbContext _context;

        public IndexModel(Votesys.AppDbContext context)
        {
            _context = context;
        }

        public IList<UserModel> UserModel { get;set; }

        public async Task OnGetAsync()
        {
            UserModel = await _context.Users.ToListAsync();
        }
    }
}