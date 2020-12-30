using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthSystem.Areas.Identity.Data;
using AuthSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthSystem.Data
{
    public class AuthSystemContext : IdentityDbContext<AuthSystemUser>
    {

        public AuthSystemContext(DbContextOptions<AuthSystemContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<ImageModel> Images { get; set; }
        public DbSet<AboutModel> About { get; set; }
        public DbSet<PaymentModel> Payment { get; set; }
        public DbSet<Notes> Notepad { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
