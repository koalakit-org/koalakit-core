namespace KoalaKit.Cosmetics
{
    [Serializable]
    public readonly struct KoalaEnvelope
    {
        public KoalaEnvelope()
        {
            Errors = new List<KoalaError>();
        }
        public KoalaEnvelope(params KoalaError[] errors) : this()
        {
            foreach (var code in errors)
            {
                Errors.Add(code);
            }
        }

        public ICollection<KoalaError> Errors {get ; }
        public bool Succeeded => !Errors.Any();
    }

    [Serializable]
    public readonly struct KoalaEnvelope<T>
    {
        public KoalaEnvelope()
        {
            Errors = new List<KoalaError>();
        }

        public KoalaEnvelope(params KoalaError[] errors) : this()
        {
            foreach (var error in errors)
            {
                Errors.Add(error);
            }
        }

        public KoalaEnvelope(T data) : this()
        {
            Data = data;
        }

        public KoalaEnvelope(T data, params KoalaError[] errors) : this(errors)
        {
            Data = data;
        }

        public ICollection<KoalaError> Errors { get; }
        public bool Succeeded => !Errors.Any();
        public T? Data { get; }
    }
}