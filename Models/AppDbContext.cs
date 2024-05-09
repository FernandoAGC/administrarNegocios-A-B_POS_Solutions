using Microsoft.EntityFrameworkCore;

namespace administrarNegocios_A_B_POS_Solutions.Models
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}