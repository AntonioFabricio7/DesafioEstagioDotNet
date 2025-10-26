using Microsoft.EntityFrameworkCore;
using GestaoFornecedores.Models;

namespace GestaoFornecedores.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Fornecedor> Fornecedores { get; set; }
    }
}