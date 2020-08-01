using BasicArticles.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicArticles.Server.Data.Category
{
    public class CategoryDbContext : DbContext
    {
        public CategoryDbContext(DbContextOptions<CategoryDbContext> options)
            : base(options)
        {
        }

        public DbSet<CategoryModel> Categories { get; set; }
    }
}
