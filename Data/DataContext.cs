using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace to_due.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Expense> Expenses => Set<Expense>();
    }
}