using App.Common.Config;
using App.Common.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace TShirtShop.DataAccess
{
    public class TShirtShopContext : DataContext<TShirtShopDbContext>, IAppContext
    {
        public TShirtShopContext(ConfigOptions<ConnectionStringOptions> config)
        {
            if (config != null)
            {
                var optionsBuilder = new DbContextOptionsBuilder<TShirtShopDbContext>();
                optionsBuilder.UseSqlServer(config.Options.DefaultConnection);
                context = new TShirtShopDbContext(optionsBuilder.Options);
                //// context.Database.SetCommandTimeout(TimeSpan.FromMinutes(10));
                //// context.Database.Migrate();
            }
        }
    }
}
