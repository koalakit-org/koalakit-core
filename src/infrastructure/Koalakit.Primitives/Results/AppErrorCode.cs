namespace Kamel.Primitives.Results;

public enum AppErrorCode
{
    GeneralError = 1,
    BadParameters,
    DataNotExists,
    NotAuthorized,
    AccessIsDenied,
    Language_NotExists,
    InvalidDisplayName,
    InvalidInput,
    InvalidOperation,
    InUse,
    InvalidEntityType,
    InvalidEntityId,

    Storage_UploadFailed = 100,
    Storage_DownloadFailed,
    Storage_DeleteFailed,
    Storage_FileNotFound,
    Storage_InvalidFileKey,
    Storage_InvalidSignature,
    Storage_UrlExpired,
    Storage_UnauthorizedAccess,
    Storage_FileTooLarge,
    Storage_InvalidContentType,
    Storage_InvalidCategory,
    Storage_InvalidOrExpiredToken,
}