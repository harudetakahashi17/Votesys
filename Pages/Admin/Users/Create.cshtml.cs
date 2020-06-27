using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Votesys;
using Votesys.Models;

namespace Votesys.Pages.Admin.Users
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
            return Page();
        }

        [BindProperty]
        public UserModel UserModel { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var mailRgx = @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$";
            var passRgx = @"(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9]).{8,}";
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Regex.IsMatch(UserModel.Email, mailRgx))
            {
                if(Regex.IsMatch(UserModel.Password, passRgx))
                {
                    var list = _context.Users.ToList();
                    var i = 0;
                    while(i < list.Count() && UserModel.Email != list[i].Email)
                    {
                        i++;
                    }
                    if(i == list.Count())
                    {
                        _context.Users.Add(UserModel);
                        await _context.SaveChangesAsync();

                        return RedirectToPage("./Index");
                    } else
                    {
                        return base.Content("<script>alert('Email already exist, choose different email'); window.location.href = \"./Create\"</script>", "text/html", Encoding.UTF8);
                    }
                } else
                {
                    return base.Content("<script>alert('Password at least 8 characters long and contin uppercase, lowercase and numeric'); window.location.href = \"./Create\"</script>", "text/html", Encoding.UTF8);
                }
            } else
            {
                return base.Content("<script>alert('Email is not valid'); window.location.href = \"./Create\"</script>", "text/html", Encoding.UTF8);
            }
        }
    }
}
