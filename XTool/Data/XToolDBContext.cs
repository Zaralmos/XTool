﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XTool.Models.DBModels;
using XTool.Models.Roles;

namespace XTool.Data
{
    public class XToolDBContext : IdentityDbContext<XToolUser, XToolRole, int>
    {
        public XToolDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Conclusion> Conclusions { get; set; }

        public DbSet<Evaluation> Evaluations { get; set; }

        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AdminRole>();
            builder.Entity<ExpertRole>();
            base.OnModelCreating(builder);
        }
    }
}