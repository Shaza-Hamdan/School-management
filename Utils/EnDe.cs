using System.Text;

namespace EncryptDecrypt
{
    public static class EnDePassword
    {
        public static string Key = "asdf@@tyui@";
        public static string ConvertToEncrypt(string Password)
        {
            if (string.IsNullOrEmpty(Password)) return "";
            Password += Key;
            var passwordBytes = Encoding.UTF8.GetBytes(Password);
            return Convert.ToBase64String(passwordBytes);
        }

        public static string ConvertToDecrypt(string base64EncodeData)
        {
            if (string.IsNullOrEmpty(base64EncodeData)) return "";
            var base64EncodeBytes = Convert.FromBase64String(base64EncodeData);
            var result = Encoding.UTF8.GetString(base64EncodeBytes);
            result = result.Substring(0, result.Length - Key.Length);
            return result;
        }
    }
}