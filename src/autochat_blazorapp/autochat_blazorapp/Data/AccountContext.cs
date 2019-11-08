using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace autochat_blazorapp.Data
{
    public class AccountContext : DbContext
    {
        public AccountContext (DbContextOptions<AccountContext> options)
            : base(options)
        {
        }

        public DbSet<autochat_blazorapp.Models.Account> Account { get; set; }
    }
}
