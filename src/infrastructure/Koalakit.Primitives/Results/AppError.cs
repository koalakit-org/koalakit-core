namespace Koalakit.Primitives.Results;

public sealed class AppError
{
    public static readonly AppError None = new(AppErrorCode.GeneralError, string.Empty, ErrorCategory.None);

    public AppErrorCode Code { get; private set; }
    public string Message { get; private set; }
    public ErrorCategory Category { get; }


    private AppError(AppErrorCode code, string message, ErrorCategory category)
    {
        Code = code;
        Message = message;
        Category = category;
    }

    public static AppError BadRequest(AppErrorCode code)
    {
        return new AppError(code, code.ToString(), ErrorCategory.BadRequest);
    }

    public static AppError NotFound(AppErrorCode code)
    {
        return new AppError(code, code.ToString(), ErrorCategory.NotFound);
    }

    public static AppError Unauthorized(AppErrorCode code)
    {
        return new AppError(code, code.ToString(), ErrorCategory.Unauthorized);
    }

    public static AppError Forbidden(AppErrorCode code)
    {
        return new AppError(code, code.ToString(), ErrorCategory.Forbidden);
    }

    public static AppError ServerError(AppErrorCode code)
    {
        return new AppError(code, code.ToString(), ErrorCategory.ServerError);
    }

    public void SetMessage(string message)
    {
        Message = message;
    }

    public enum ErrorCategory
    {
        None = 0,
        BadRequest = 1,
        Unauthorized = 2,
        Forbidden = 3,
        NotFound = 4,
        ServerError = 5
    }
}