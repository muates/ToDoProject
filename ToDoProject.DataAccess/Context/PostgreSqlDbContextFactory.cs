using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ToDoProject.DataAccess.Config;

namespace ToDoProject.DataAccess.Context;

public class PostgreSqlDbContextFactory : IDesignTimeDbContextFactory<PostgreSqlDbContext>
{
    public PostgreSqlDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PostgreSqlDbContext>();

        optionsBuilder.UseNpgsql(EnvironmentConfig.PostgreSqlConnection);

        return new PostgreSqlDbContext(optionsBuilder.Options);
    }
}