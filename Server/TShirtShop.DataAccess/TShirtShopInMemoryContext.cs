using App.Common.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;

namespace TShirtShop.DataAccess
{
    public class TShirtShopInMemoryContext : DataContext<TShirtShopDbContext>, IAppContext
    {
        public TShirtShopInMemoryContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TShirtShopDbContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            context = new TShirtShopDbContext(optionsBuilder.Options);
        }
    }
}
