using DotNetEnv;

namespace ToDoProject.Core.Config;

public static class EnvironmentConfig
{
    public static void LoadEnv()
    {
        try
        {
            var projectDir =
                Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", ".."));
            
            var envFilePath = Path.Combine(projectDir, "Docker", ".env");
            
            if (File.Exists(envFilePath))
            {
                Env.Load(envFilePath);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    
    public static string PostgreSqlConnection => 
        $"Host={Environment.GetEnvironmentVariable("POSTGRES_TODO_PROJECT_HOST")};" +
        $"Port={Environment.GetEnvironmentVariable("POSTGRES_TODO_PROJECT_PORT")};" +
        $"Database={Environment.GetEnvironmentVariable("POSTGRES_TODO_PROJECT_DB")};" +
        $"Username={Environment.GetEnvironmentVariable("POSTGRES_TODO_PROJECT_USER")};" +
        $"Password={Environment.GetEnvironmentVariable("POSTGRES_TODO_PROJECT_PASSWORD")};";

    public static string? JwtKey => Environment.GetEnvironmentVariable("JWT_TODO_PROJECT_KEY");
    public static string? JwtIssuer => Environment.GetEnvironmentVariable("JWT_TODO_PROJECT_ISSUER");
    public static string? JwtAudience => Environment.GetEnvironmentVariable("JWT_TODO_PROJECT_AUDIENCE");
}