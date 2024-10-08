﻿using Microsoft.EntityFrameworkCore;
using PTGKDotNetCoreBath4.RestApiWithNlayer.ConsoleApp.Dto;
using PTGKDotNetCoreBath4.RestApiWithNlayer.ConsoleApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTGKDotNetCoreBath4.RestApiWithNlayer.ConsoleApp.EfcoreExample
{
    internal class AppDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        }
        public DbSet<CustomerDto> Customer { get; set; }
    }
}
