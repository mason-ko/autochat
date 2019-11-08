using autochat_blazorapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace autochat_blazorapp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AccountContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Account.Any())
            {
                return;   // DB has been seeded
            }

            var accounts= new Account[]
            {
                new Account{ Id="1", UserName="2", Password="3" }
            };
            foreach (var s in accounts)
            {
                context.Account.Add(s);
            }
            context.SaveChanges();
        }
    }
}
