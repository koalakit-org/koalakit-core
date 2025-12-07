using Koalakit.Primitives.Enumerations;

namespace Koalakit.Primitives.Results;

public sealed class ErrorCode : AppEnumeration
{
    // General errors (1-99)
    public static readonly ErrorCode GeneralError = new(1, nameof(GeneralError));
    public static readonly ErrorCode BadParameters = new(2, nameof(BadParameters));
    public static readonly ErrorCode DataNotExists = new(3, nameof(DataNotExists));
    public static readonly ErrorCode NotAuthorized = new(4, nameof(NotAuthorized));
    public static readonly ErrorCode AccessIsDenied = new(5, nameof(AccessIsDenied));
    public static readonly ErrorCode LanguageNotExists = new(6, nameof(LanguageNotExists));
    public static readonly ErrorCode InvalidDisplayName = new(7, nameof(InvalidDisplayName));
    public static readonly ErrorCode InvalidInput = new(8, nameof(InvalidInput));
    public static readonly ErrorCode InvalidOperation = new(9, nameof(InvalidOperation));
    public static readonly ErrorCode InUse = new(10, nameof(InUse));
    public static readonly ErrorCode InvalidEntityType = new(11, nameof(InvalidEntityType));
    public static readonly ErrorCode InvalidEntityId = new(12, nameof(InvalidEntityId));

    // Storage errors (100-199)
    public static readonly ErrorCode StorageUploadFailed = new(100, nameof(StorageUploadFailed));
    public static readonly ErrorCode StorageDownloadFailed = new(101, nameof(StorageDownloadFailed));
    public static readonly ErrorCode StorageDeleteFailed = new(102, nameof(StorageDeleteFailed));
    public static readonly ErrorCode StorageFileNotFound = new(103, nameof(StorageFileNotFound));
    public static readonly ErrorCode StorageInvalidFileKey = new(104, nameof(StorageInvalidFileKey));
    public static readonly ErrorCode StorageInvalidSignature = new(105, nameof(StorageInvalidSignature));
    public static readonly ErrorCode StorageUrlExpired = new(106, nameof(StorageUrlExpired));
    public static readonly ErrorCode StorageUnauthorizedAccess = new(107, nameof(StorageUnauthorizedAccess));
    public static readonly ErrorCode StorageFileTooLarge = new(108, nameof(StorageFileTooLarge));
    public static readonly ErrorCode StorageInvalidContentType = new(109, nameof(StorageInvalidContentType));
    public static readonly ErrorCode StorageInvalidCategory = new(110, nameof(StorageInvalidCategory));
    public static readonly ErrorCode StorageInvalidOrExpiredToken = new(111, nameof(StorageInvalidOrExpiredToken));

    private ErrorCode(int id, string name) : base(id, name)
    {
    }

    /// <summary>
    /// Gets the error category based on the Id range.
    /// </summary>
    public string GetCategory()
    {
        return Id switch
        {
            >= 1 and < 100 => "General",
            >= 100 and < 200 => "Storage",
            _ => "Unknown"
        };
    }

    /// <summary>
    /// Checks if this is a storage-related error.
    /// </summary>
    public bool IsStorageError() => Id >= 100 && Id < 200;

    /// <summary>
    /// Checks if this is a general error.
    /// </summary>
    public bool IsGeneralError() => Id >= 1 && Id < 100;
}

