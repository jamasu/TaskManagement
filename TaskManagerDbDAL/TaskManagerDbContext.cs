using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.Extensions.Options;

namespace TaskManagerDbDAL
{
    public class TaskManagerDbContext : DbContext
    {


        public TaskManagerDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public TaskManagerDbContext(IOptions<TaskSettings> accessor)
            : base(accessor.Value.ConnectionString)
        {
        }

        public DbSet<TaskHandler> Tasks { get; set; }

    
    }
}
