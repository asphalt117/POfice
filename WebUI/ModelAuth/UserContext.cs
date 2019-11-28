using System.Data.Entity;

namespace WebUI.ModelAuth
{
    public class UserContext:DbContext
    {
        public UserContext() :
              base("AuthConnection")
        { }

        public DbSet<User> Users { get; set; }

    }
}