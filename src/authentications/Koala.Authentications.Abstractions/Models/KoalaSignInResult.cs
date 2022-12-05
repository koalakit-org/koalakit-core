namespace Koala.Authentications
{
    public struct KoalaSignInResult
    {
        public KoalaSignInResult(string code)
        {
            Code = code;
            Token = string.Empty;
            RefreshToken = string.Empty;
        }

        public KoalaSignInResult(string token, string refreshToken)
        {
            Code = string.Empty;
            Token = token;
            RefreshToken = refreshToken;
        }

        public bool Succeeded => string.IsNullOrEmpty(Code);
        public string Code { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
