using Microsoft.EntityFrameworkCore;

namespace API.Models;

public class AppDataContext : DbContext
{
    //Entity Framework Code First
    //Quais classes vão representar as tabelas no banco
    public DbSet<Produto> Produtos { get; set; }

    //Isto é um metodo de configuração para adicioanr tabelas ao banco de dados
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Configuração da string de conexão
        optionsBuilder.UseSqlite("Data Source=app.db");
    }
}
