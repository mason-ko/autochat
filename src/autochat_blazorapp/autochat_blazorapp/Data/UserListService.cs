using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace autochat_blazorapp.Data
{
    public class UserListService
    {
        public Task<string[]> GetUserListAsync()
        {
            //temp
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => "random:" + index).ToArray());
        }
    }
}
