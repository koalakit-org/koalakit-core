namespace KoalaKit.Cosmetics
{
    [Serializable]
    public struct FullName
    {
        ///TODO: Replace with serialize
        public FullName(string information)
        {
            FirstName = information.Split(":")[0];
            MiddleName = information.Split(":")[1];
            LastName = information.Split(":")[2];
        }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }


        public override string ToString() => $"{FirstName}:{MiddleName}:{LastName}";
    }
}
