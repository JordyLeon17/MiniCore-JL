using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniCore.Models;

namespace MiniCore.Data
{
    public class MiniCoreContext : DbContext
    {
        public MiniCoreContext (DbContextOptions<MiniCoreContext> options)
            : base(options)
        {
        }

        public DbSet<MiniCore.Models.Cliente> Cliente { get; set; } = default!;

        public DbSet<MiniCore.Models.Contrato> Contrato { get; set; }
    }
}
