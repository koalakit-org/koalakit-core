namespace Koala.Authentications.Jwt
{
    public struct IdentityAddParameters
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string[] Identifiers { get; set; }
        public KeyValuePair<string, string>[] Claims { get; set; }
    }
}
