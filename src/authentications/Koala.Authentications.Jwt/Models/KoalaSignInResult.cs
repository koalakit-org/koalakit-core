namespace Koala.Authentications.Jwt
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
            Succeeded = true;
            Code= string.Empty;
            Token = token;
            RefreshToken = refreshToken;
        }

        public bool Succeeded { get; set; }
        public string Code { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
