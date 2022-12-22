namespace KoalaKit.Cosmetics
{

    [Serializable]
    public class KoalaError<TErrorCode> where TErrorCode : struct, IConvertible
    {
        public KoalaError(TErrorCode code, string message)
        {
            Code = code;
            Message = message;
        }

        public TErrorCode Code { get; }

        public string Message { get; }
    }

    [Serializable]
    public class KoalaError : KoalaError<int>
    {
        public KoalaError(int code, string message) : base(code, message)
        {
        }
    }
}
