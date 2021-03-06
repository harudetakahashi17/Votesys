﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Data.SqlClient;
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

        [HttpPost]
        public ActionResult GetVote(string[] voteId)
        {
            var res = false;
            var sesi = HttpContext.Session.GetString("_Email");
            if(sesi != null)
            {
                var stringConn = "Server=localhost\\SQLEXPRESS; Initial Catalog=Votesys; User ID=admin; Password=Passwd123;";
                SqlConnection conn = new SqlConnection(stringConn);
                conn.Open();
                for (var i = 0;i < voteId.Length; i++)
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("EXEC [dbo].[USP_Vote_Insert] @Voter, @VoteItem", conn);
                        cmd.Parameters.AddWithValue("@Voter", sesi);
                        cmd.Parameters.AddWithValue("@VoteItem", voteId[i]);
                        
                        SqlDataReader reader = cmd.ExecuteReader();
                        reader.Read();
                        if (reader[0].ToString() == "1")
                        {
                            res = true;
                        }
                        reader.Close();
                        cmd.Dispose();

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                conn.Close();
                return Json(res);
            } else
            {
                return Json(res);
            }
        }
    }
}