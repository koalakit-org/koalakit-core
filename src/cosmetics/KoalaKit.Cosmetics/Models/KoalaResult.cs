namespace KoalaKit.Cosmetics
{
    [Obsolete($"Obsolete implementation, use KoalaEnvelope in replace", true)]
    public class KoalaResult
    {
        private List<string> errors;
        private List<string> messages;


        public KoalaResult()
        {
            errors = new List<string>();
            messages = new List<string>();
        }
        public KoalaResult(params string[] codes) : this()
        {
            foreach (var code in codes)
            {
                errors.Add(code);
            }
        }
        public string[] Errors => errors.ToArray();
        public string[] Messages => GetMessages();
        public bool Succeeded => !errors.Any();

        public string[] GetMessages()
        {
            foreach (var error in errors)
            {
                messages.Add(error);
            }

            return messages.ToArray();
        }
    }


    public class KoalaResult<T> : KoalaResult
    {
        public KoalaResult(params string[] codes) : base(codes)
        { }
        public KoalaResult(T? data) : base()
        {
            Data = data;
        }

        public KoalaResult(T? data, params string[] codes)
            : base(codes)
        {
            Data = data;
        }

        public T? Data { get; set; }
    }
}
