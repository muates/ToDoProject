using System.ComponentModel.DataAnnotations;
using ToDoProject.CrossCutting.Validation.Abstract;
using ToDoProject.Model.Dto.User.Request;
using ValidationException = ToDoProject.CrossCutting.Ex.ValidationException;

namespace ToDoProject.CrossCutting.Validation.Concrete;

public class RegisterRequestValidator : IValidationStrategy<RegisterRequest>
{
    public void Validate(RegisterRequest request)
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(request);

        if (!Validator.TryValidateObject(request, validationContext, validationResults, true))
        {
            throw new ValidationException("Validation failed");
        }
        
        if (!request.Email.Contains('@'))
        {
            throw new ValidationException("Invalid email format.");
        }
        
        if (string.IsNullOrWhiteSpace(request.Password) || 
            !request.Password.Any(char.IsUpper) || 
            !request.Password.Any(char.IsLower) || 
            !request.Password.Any(char.IsDigit))
        {
            throw new ValidationException("Password must contain at least one uppercase letter, one lowercase letter, and one digit.");
        }
    }
}