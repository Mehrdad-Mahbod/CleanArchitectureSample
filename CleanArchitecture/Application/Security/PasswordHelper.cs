using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Application.Security
{
    public static class PasswordHelper
    {
        public static string EncodePasswordMd5(string Pass) //Encrypt using MD5   
        {
            Byte[] OriginalBytes;
            Byte[] EncodedBytes;
            MD5 Md5;
            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)   
            Md5 = new MD5CryptoServiceProvider();
            OriginalBytes = ASCIIEncoding.Default.GetBytes(Pass);
            EncodedBytes = Md5.ComputeHash(OriginalBytes);
            //Convert encoded bytes back to a 'readable' string   
            return BitConverter.ToString(EncodedBytes);
        }
    }
}
