
namespace KoalaKit.Cosmetics
{
    [Serializable]
    public class KoalaEnvelope
    {
        protected List<KoalaError> errors;

        public KoalaEnvelope()
        {
            errors = new List<KoalaError>();
        }
        public KoalaEnvelope(params KoalaError[] codes) : this()
        {
            foreach (var code in codes)
            {
                errors.Add(code);
            }
        }

        public IEnumerable<KoalaError> Errors => errors;
        public bool Succeeded => !Errors.Any();
    }

    [Serializable]
    public class KoalaEnvelope<T> : KoalaEnvelope
    {
        public KoalaEnvelope() : base() { }

        public KoalaEnvelope(T data) : this()
        {
            Data = data;
        }

        public KoalaEnvelope(params KoalaError[] codes) : base(codes) { }

        public KoalaEnvelope(T data, params KoalaError[] errors) : base(errors)
        {
            Data = data;
        }

        public T? Data { get; set; }
    }
}
