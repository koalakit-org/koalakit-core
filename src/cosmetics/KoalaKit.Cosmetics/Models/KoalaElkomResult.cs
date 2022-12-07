namespace KoalaKit.Cosmetics
{
    public class KoalaElkomResult
    {
        private List<string> errors;
        private List<string> messages;


        public KoalaElkomResult()
        {
            errors = new List<string>();
            messages = new List<string>();
        }
        public KoalaElkomResult(params string[] codes) : this()
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


    public class KoalaElkomResult<T> : KoalaElkomResult
    {
        public KoalaElkomResult(params string[] codes) : base(codes)
        { }
        public KoalaElkomResult(T? data) : base()
        {
            Data = data;
        }

        public KoalaElkomResult(T? data, params string[] codes)
            : base(codes)
        {
            Data = data;
        }

        public T? Data { get; set; }
    }
}
