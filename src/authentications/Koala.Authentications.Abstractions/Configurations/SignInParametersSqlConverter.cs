using KoalaKit.Cosmetics;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Koala.Authentications
{
    public class SignInParametersSqlConverter
        : ValueConverter<SignInParameters, string>
    {
        public SignInParametersSqlConverter()
            : base(information => information.ToString(), str => new SignInParameters(str))
        {

        }
    }
}
