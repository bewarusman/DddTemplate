namespace Application.Common.Messaging;

public class Result
{
    public bool Succeeded => ErrorMessage == null;
    public string ErrorMessage { get; private set; } = null;
    public object Value { get; private set; }
    public string SuccessMessage { get; private set; }

    public static Result Success<TValue>(TValue value, string successMessage = "SUCCESS") =>
        new Result
        {
            Value = value,
            SuccessMessage = successMessage,
        };

    public static Result Failed(string error) =>
        new Result { ErrorMessage = error };

    public TModel GetValue<TModel>()
        where TModel : class, new()
    {
        if (Value is null)
            return null;
        var model = Value as TModel;
        return model;
    }
}