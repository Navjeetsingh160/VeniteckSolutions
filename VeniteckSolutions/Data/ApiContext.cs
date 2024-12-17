using Microsoft.EntityFrameworkCore;
using VeniteckSolutions.Models;

namespace VeniteckSolutions.Data
{
    public class ApiContext : DbContext

    {
        public ApiContext(DbContextOptions<ApiContext> options) :base(options)
        
        { 
        
            
        }

        public DbSet<UserDTO> userDTOs { get; set; }
    }
}
