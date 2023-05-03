using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Data
{
    public class DbConnected : DbContext
    {
        public DbConnected(DbContextOptions<DbConnected> options)
                : base(options) { }
        
        public DbSet<Employees> Employee => Set<Employees>();

    }

}