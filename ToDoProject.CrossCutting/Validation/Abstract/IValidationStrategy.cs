namespace ToDoProject.CrossCutting.Validation.Abstract;

public interface IValidationStrategy<in TRequest>
{
    void Validate(TRequest request);
}
