using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace KoalaKit.Cosmetics
{
    public struct MobileNumber : IEquatable<MobileNumber>
    {
        private string countryCode;
        private string number;


        public MobileNumber()
        {
            countryCode = string.Empty;
            number = string.Empty;
        }

        public MobileNumber(string value)
        {
            this.countryCode = value.Split(":").First();
            number = value.Split(":").Last();
        }

        public string Number
        {
            get { return number; }
            set { number = value.NormalizeValue(); }
        }


        [JsonIgnore]
        public string NumberWithZero
        {
            get
            {
                if (string.IsNullOrWhiteSpace(number))
                    return "";

                return $"0{number}";
            }
        }

        [JsonIgnore]
        public string NumberCountryCode
        {
            get
            {
                if (string.IsNullOrWhiteSpace(number))
                    return "";

                return $"{countryCode}{number}";
            }
        }

        public string[] GetContacts()
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                return new string[0];
            }
            return new[] { Number, NumberCountryCode, NumberWithZero };
        }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(number);
        }

        public bool IsValidOrEmpty()
        {
            if (string.IsNullOrWhiteSpace(number)) return true;
            return number.Length == 9 && Regex.Match(number, "^[0-9]*$").Success;
        }

        public string Normalize()
        {
            if (string.IsNullOrWhiteSpace(number)) return "";

            var result = number.Trim().ToLower();
            if (result.Length > 9)
            {
                return result.Substring(result.Length - 9);
            }
            return result;
        }

        public bool Equals(MobileNumber other)
        {
            return !string.IsNullOrEmpty(number)  && this.number == other.number;
        }

        public override string ToString() => $"{countryCode}:{number}";
    }
}
