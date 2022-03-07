using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helpers
{
    public static class CRYPT
    {
        private static string passwordcodify = "CRYPT4X3S0DNP";
        private static string charscodify = "MEEBAKSOFT";

        public static string Encrypt<T>(string value)
          where T : SymmetricAlgorithm, new()
        {
            DeriveBytes rgb = new Rfc2898DeriveBytes(passwordcodify, Encoding.Unicode.GetBytes(charscodify));

            SymmetricAlgorithm algorithm = new T();

            byte[] rgbKey = rgb.GetBytes(algorithm.KeySize >> 3);
            byte[] rgbIV = rgb.GetBytes(algorithm.BlockSize >> 3);

            ICryptoTransform transform = algorithm.CreateEncryptor(rgbKey, rgbIV);

            using (MemoryStream buffer = new MemoryStream())
            {
                using (CryptoStream stream = new CryptoStream(buffer, transform, CryptoStreamMode.Write))
                {
                    using (StreamWriter writer = new StreamWriter(stream, Encoding.Unicode))
                    {
                        writer.Write(value);
                    }
                }

                return Convert.ToBase64String(buffer.ToArray());
            }
        }

        /// <summary>
        /// Desencriptar cadena de texto.
        /// </summary>
        /// <typeparam name="T">RijndaelManaged | TripleDESCryptoServiceProvider </typeparam>
        /// <param name="text">Texto encriptado</param>
        /// <returns>Texto desencriptado</returns>
        public static string Decrypt<T>(string text)
           where T : SymmetricAlgorithm, new()
        {
            DeriveBytes rgb = new Rfc2898DeriveBytes(passwordcodify, Encoding.Unicode.GetBytes(charscodify));

            SymmetricAlgorithm algorithm = new T();

            byte[] rgbKey = rgb.GetBytes(algorithm.KeySize >> 3);
            byte[] rgbIV = rgb.GetBytes(algorithm.BlockSize >> 3);

            ICryptoTransform transform = algorithm.CreateDecryptor(rgbKey, rgbIV);

            using (MemoryStream buffer = new MemoryStream(Convert.FromBase64String(text)))
            {
                using (CryptoStream stream = new CryptoStream(buffer, transform, CryptoStreamMode.Read))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.Unicode))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }
        /*-------------------------------------------------------*/
        // EJEMPLO DE USO:
        //  String encrypted = CRYPT.Encrypt<TripleDESCryptoServiceProvider>("valor_encriptar");
        //  String decrypted = CRYPT.Encrypt<RijndaelManaged>("texto_desencriptar");
        /*-------------------------------------------------------*/

    }
}
