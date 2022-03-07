using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace WS_IT.Util
{
    public class GlobalUtil
    {
        public static string EncryptPassword(string password)
        {
            password = CRYPT.Encrypt<TripleDESCryptoServiceProvider>(password);
            password = password.ToString().Replace('+', ' ');
            return password;
        }
    }
}