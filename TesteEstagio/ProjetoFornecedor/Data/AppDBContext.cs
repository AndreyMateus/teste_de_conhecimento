using Microsoft.EntityFrameworkCore;
using ProjetoFornecedor.Models;

namespace ProjetoFornecedor.Data;

public class AppDBContext : DbContext
{
    public DbSet<FornecedorModel> Fornecedores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=ANDREA\\SQLEXPRESS;Database=Fornecedores2;Trusted_Connection=True;");
    }
    
}