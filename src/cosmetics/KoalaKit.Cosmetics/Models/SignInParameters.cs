using System.Diagnostics.CodeAnalysis;

namespace KoalaKit.Cosmetics
{
    public struct SignInParameters : IEquatable<SignInParameters>
    {
        private string username;
        private string password;

        public SignInParameters(string token)
        {
            username = token.Split(":")[0];
            password = token.Split(":")[1];
        }

        public SignInParameters(string username, string password)
        {
            this.username = username.Trim();
            this.password = password;
        }

        public string Username
        {
            get { return username.NormalizeValue(); }
            set { username = value.NormalizeValue(); }
        }

        public string Password
        {
            get { return password.NormalizeValue(); }
            set { password = value.NormalizeValue(); }
        }

        public bool Equals(SignInParameters other)
        {
            return Username == other.Username && Password == other.Password;
        }

        public int GetHashCode([DisallowNull] SignInParameters obj) => base.GetHashCode();

        public override string ToString() => $"{Username}:{Password}";
    }
}
