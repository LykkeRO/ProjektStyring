using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjektStyring.Models
{
    public class DataContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Category> Categorys { get; set; }

    }
}