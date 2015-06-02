using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SSI
{
    class PasswordHash
    {
        public PasswordHash()
        {
        }
        public string hashPassword(string sPassword)
        {
            byte[] tmpSource;
            byte[] tmpHash;

            tmpSource = ASCIIEncoding.ASCII.GetBytes(sPassword);

            tmpHash = new SHA512CryptoServiceProvider().ComputeHash(tmpSource);
            tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpHash);

            return ByteArrayToString(tmpHash);
        }
        static string ByteArrayToString(byte[] arrInput)
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length - 1; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }
    }
}
