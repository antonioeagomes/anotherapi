using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Another.Api.Data
{
    public class IdentityAppDbContext : IdentityDbContext
    {
        public IdentityAppDbContext(DbContextOptions<IdentityAppDbContext> options) : base(options) { }
    }
}
