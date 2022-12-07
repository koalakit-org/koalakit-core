using System.ComponentModel;

namespace System
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Return enum description of given enum vlaue
        /// </summary>
        /// <param name="value">enum value</param>
        /// <returns>String description of given enum value</returns>
        public static string GetDescription(this Enum value)
        {
            if (value == null)
                return string.Empty;

            var fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null)
                return string.Empty;

            var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return ((DescriptionAttribute)attributes[0]).Description;
            else
                return value.ToString();
        }
    }
}
