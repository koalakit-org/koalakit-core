
namespace KoalaKit.Cosmetics
{
    [Serializable]
    public class KoalaEnvelope<T>
    {
        private List<KoalaError> errors;

        public KoalaEnvelope()
        {
            errors = new List<KoalaError>();
        }

        public KoalaEnvelope(T data) : this()
        {
            Data = data;
        }

        public KoalaEnvelope(params KoalaError[] codes) : this()
        {
            foreach (var code in codes)
            {
                errors.Add(code);
            }
        }
        public KoalaEnvelope(T data, params KoalaError[] errors) : this()
        {
            Data = data;
        }

        public T? Data { get; set; }
        public IEnumerable<KoalaError> Errors => errors;
        public bool Succeeded => !Errors.Any();
    }
}
