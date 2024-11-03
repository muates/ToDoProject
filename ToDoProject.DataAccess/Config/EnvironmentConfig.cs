using DotNetEnv;

namespace ToDoProject.DataAccess.Config;

public class EnvironmentConfig
{
    public static string PostgreSqlConnection => 
        $"Host={Environment.GetEnvironmentVariable("POSTGRES_TODO_PROJECT_HOST")};" +
        $"Port={Environment.GetEnvironmentVariable("POSTGRES_TODO_PROJECT_PORT")};" +
        $"Database={Environment.GetEnvironmentVariable("POSTGRES_TODO_PROJECT_DB")};" +
        $"Username={Environment.GetEnvironmentVariable("POSTGRES_TODO_PROJECT_USER")};" +
        $"Password={Environment.GetEnvironmentVariable("POSTGRES_TODO_PROJECT_PASSWORD")};";

    public static void LoadEnv()
    {
        try
        {
            var projectDir =
                Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", ".."));
            var envFilePath = Path.Combine(projectDir, "ToDoProject", "Docker", ".env");

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
}