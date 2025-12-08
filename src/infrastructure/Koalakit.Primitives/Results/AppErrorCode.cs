using Koalakit.Primitives.Enumerations;

namespace Koalakit.Primitives.Results;

public sealed class AppErrorCode : AppEnumeration
{
    // General errors (1-99)
    public static readonly AppErrorCode GeneralError = new(1, nameof(GeneralError));
    public static readonly AppErrorCode BadParameters = new(2, nameof(BadParameters));
    public static readonly AppErrorCode DataNotExists = new(3, nameof(DataNotExists));
    public static readonly AppErrorCode NotAuthorized = new(4, nameof(NotAuthorized));
    public static readonly AppErrorCode AccessIsDenied = new(5, nameof(AccessIsDenied));
    public static readonly AppErrorCode LanguageNotExists = new(6, nameof(LanguageNotExists));
    public static readonly AppErrorCode InvalidDisplayName = new(7, nameof(InvalidDisplayName));
    public static readonly AppErrorCode InvalidInput = new(8, nameof(InvalidInput));
    public static readonly AppErrorCode InvalidOperation = new(9, nameof(InvalidOperation));
    public static readonly AppErrorCode InUse = new(10, nameof(InUse));
    public static readonly AppErrorCode InvalidEntityType = new(11, nameof(InvalidEntityType));
    public static readonly AppErrorCode InvalidEntityId = new(12, nameof(InvalidEntityId));

    // Storage errors (100-199)
    public static readonly AppErrorCode StorageUploadFailed = new(100, nameof(StorageUploadFailed));
    public static readonly AppErrorCode StorageDownloadFailed = new(101, nameof(StorageDownloadFailed));
    public static readonly AppErrorCode StorageDeleteFailed = new(102, nameof(StorageDeleteFailed));
    public static readonly AppErrorCode StorageFileNotFound = new(103, nameof(StorageFileNotFound));
    public static readonly AppErrorCode StorageInvalidFileKey = new(104, nameof(StorageInvalidFileKey));
    public static readonly AppErrorCode StorageInvalidSignature = new(105, nameof(StorageInvalidSignature));
    public static readonly AppErrorCode StorageUrlExpired = new(106, nameof(StorageUrlExpired));
    public static readonly AppErrorCode StorageUnauthorizedAccess = new(107, nameof(StorageUnauthorizedAccess));
    public static readonly AppErrorCode StorageFileTooLarge = new(108, nameof(StorageFileTooLarge));
    public static readonly AppErrorCode StorageInvalidContentType = new(109, nameof(StorageInvalidContentType));
    public static readonly AppErrorCode StorageInvalidCategory = new(110, nameof(StorageInvalidCategory));
    public static readonly AppErrorCode StorageInvalidOrExpiredToken = new(111, nameof(StorageInvalidOrExpiredToken));

    private AppErrorCode(int id, string name) : base(id, name)
    {
    }

    public string GetCategory()
    {
        return Id switch
        {
            >= 1 and < 100 => "General",
            >= 100 and < 200 => "Storage",
            _ => "Unknown"
        };
    }

    public bool IsStorageError() => Id >= 100 && Id < 200;
    public bool IsGeneralError() => Id >= 1 && Id < 100;
}

