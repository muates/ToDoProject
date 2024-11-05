using ToDoProject.Core.Model.Response;
using ToDoProject.CrossCutting.Ex;
using ApplicationException = System.ApplicationException;

namespace ToDoProject.Api.Middleware;

public class ErrorHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
    
    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var errorMessage = $"Error occurred: {ex.Message}";

        var response = ex switch
        {
            ValidationException => new OperationResponse<object>(400, null!, errorMessage),
            InvalidCredentialException => new OperationResponse<object>(401, null!, errorMessage),
            NotFoundException => new OperationResponse<object>(404, null!, errorMessage),
            AlreadyExistException => new OperationResponse<object>(409, null!, errorMessage),
            ApplicationException => new OperationResponse<object>(500, null!, errorMessage),
            _ => new OperationResponse<object>(500, null!, errorMessage)
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = response.StatusCode;

        return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
    }
}