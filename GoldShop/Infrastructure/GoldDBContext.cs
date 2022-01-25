using JewelleryShop.JewellaryEntity.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldShop.Infrastructure
{
    public class GoldDBContext : DbContext
    {
        public GoldDBContext(DbContextOptions<GoldDBContext> options) : base(options)
        {

        }

        // Tables
        public DbSet<Users> Users { get; set; }

    }
}
