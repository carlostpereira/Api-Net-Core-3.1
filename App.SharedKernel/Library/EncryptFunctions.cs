using System.Text;

namespace App.SharedKernel.Library
{
    public static class EncryptFunctions
    {
        #region Encrypting

        public static string EncryptPassword(string pass)
        {
            var secretKey = @"|vfZy?yAj^'pJQ3Tg3??_!fxN,[!mM)n&xEF8'eb])})6:^yyFaV+3PV,PPm*8a`x2WYe-<KmP[c\};uN4YGEkVtu´V7AC,u=Qgq`jY?+!4;zsPY<f/Ssn:NKy43As";

            if (!string.IsNullOrEmpty(pass))
            {
                var password = (pass += secretKey);
                System.Security.Cryptography.SHA512 encryptType = System.Security.Cryptography.SHA512.Create();
                byte[] data = encryptType.ComputeHash(Encoding.Default.GetBytes(password));
                StringBuilder sbString = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                    sbString.Append(data[i].ToString("x2"));

                var hash = sbString.ToString();
                return hash;
            }

            return string.Empty;
        }

        #endregion
    }
}
