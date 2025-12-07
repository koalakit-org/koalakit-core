using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Koalakit.Primitives.Results;

public class AppResult
{
    [JsonIgnore]
    public bool IsSuccess { get; }
    [JsonIgnore]
    public bool IsFailure => !IsSuccess;
    public bool Succeeded => IsSuccess;
    public AppError[] Errors { get; }

    protected internal AppResult(bool isSuccess, params AppError[] errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public static implicit operator bool(AppResult result) => result.IsSuccess;
    public static implicit operator AppResult(AppError error) => Failure(error);
    public static implicit operator AppResult(AppError[] errors) => Failure(errors);
    public static AppResult Success() => new(true);
    public static AppResult<T> Success<T>(T data) => new(data, true);
    public static AppResult Failure(params AppError[] errors) => new(false, errors);
    public static AppResult<T> Failure<T>(params AppError[] errors) => new(default, false, errors);
}


public sealed class AppResult<T> : AppResult
{
    readonly T? _data;
    internal AppResult(
        T? data,
        bool isSuccess,
        params AppError[] errors)
        : base(isSuccess, errors)
    {
        _data = data;
    }

    public T? Data => _data;

    public bool TryGetData([NotNullWhen(true)] out T? data)
    {
        if (!IsSuccess || _data == null)
        {
            data = default;
            return false;
        }

        data = _data;
        return true;
    }

    public static implicit operator AppResult<T>(T data) => Success(data);
    public static implicit operator AppResult<T>(AppError[] errors) => Failure<T>(errors);
    public static implicit operator AppResult<T>(AppError error) => Failure<T>(error);
    public static implicit operator bool(AppResult<T> result) => result.IsSuccess;
}