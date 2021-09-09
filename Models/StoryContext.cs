using Microsoft.EntityFrameworkCore;

namespace storyshare_backend_dotnet_v3.Models
{
  public class StoryContext : DbContext
  {
    public StoryContext(DbContextOptions<StoryContext> options) : base(options)
    {
    }
    public DbSet<Story> Stories {get; set;}
  }
}
