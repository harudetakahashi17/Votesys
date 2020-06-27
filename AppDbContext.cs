using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Votesys.Models;

namespace Votesys
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<VoteItemModel> VotingItems { get; set; }
        public DbSet<VoteModel> Vote { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
    }
}
