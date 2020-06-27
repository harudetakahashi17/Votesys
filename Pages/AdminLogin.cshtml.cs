using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Votesys.Models;

namespace Votesys.Pages
{
    public class AdminLoginModel : PageModel
    {
        private readonly Votesys.AppDbContext _context;
        const string SessionUser = "_Email";

        public AdminLoginModel(Votesys.AppDbContext context)
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

            if (Regex.IsMatch(UserModel.Email, mailRgx))
            {
                if (Regex.IsMatch(UserModel.Password, passRgx))
                {
                    var admin = await _context.Users.FirstOrDefaultAsync(u => u.Email == "admin@example.co.id");
                    if(admin == null)
                    {
                        return base.Content("<script>alert('Admin is not exist, create it from DB'); window.location.href = \"./Create\"</script>", "text/html", Encoding.UTF8);
                    }

                    if (admin.Email == UserModel.Email && admin.Password == UserModel.Password)
                    {
                        base.HttpContext.Session.SetString(SessionUser, UserModel.Email);
                        return RedirectToPage("/Admin/VoteItems/Index");
                    }
                    else
                    {
                        return RedirectToPage("./AdminLogin");
                    }
                }
                else
                {
                    return base.Content("<script>alert('Password at least 8 characters long and contin uppercase, lowercase and numeric'); window.location.href = \"./AdminLogin\"</script>", "text/html", Encoding.UTF8);
                }
            }
            else
            {
                return base.Content("<script>alert('Email is not valid'); window.location.href = \"./AdminLogin\"</script>", "text/html", Encoding.UTF8);
            }
        }
    }
}