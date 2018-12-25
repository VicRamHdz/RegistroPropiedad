using System;
namespace RegistroPropiedad.Helper
{
    public static class NullValidators
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
    }
}
