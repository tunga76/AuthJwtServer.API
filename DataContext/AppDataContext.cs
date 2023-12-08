using AuthJwtServer.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthJwtServer.API.DataContext
{
    public class AppDataContext : IdentityDbContext<AppUser>
    {
        public AppDataContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
                
        }
    }
}
