using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SUDLife_SecruityMechanism
{
    public class ClsSecurityMech
    {
       
        public ClsSecurityMech()
        {
          
        }
        public string Encrypt(string PlainText, string key)
        {
            try
            {
                RijndaelManaged aes = new RijndaelManaged();
                aes.BlockSize = 128;
                aes.KeySize = 256;

                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                byte[] keyArr = Convert.FromBase64String(key);
                byte[] KeyArrBytes32Value = new byte[32];
                Array.Copy(keyArr, KeyArrBytes32Value, 24);

                byte[] ivArr = { 1, 2, 3, 4, 5, 6, 6, 5, 4, 3, 2, 1, 7, 7, 7, 7 };
                byte[] IVBytes16Value = new byte[16];
                Array.Copy(ivArr, IVBytes16Value, 16);

                aes.Key = KeyArrBytes32Value;
                aes.IV = IVBytes16Value;

                ICryptoTransform encrypto = aes.CreateEncryptor();

                byte[] plainTextByte = ASCIIEncoding.UTF8.GetBytes(PlainText);
                byte[] CipherText = encrypto.TransformFinalBlock(plainTextByte, 0, plainTextByte.Length);
                return Convert.ToBase64String(CipherText);
            }
            catch (Exception ex)
            {
                throw;
            }


        }

        public string Decrypt(string CipherText, string key)
        {
            try
            {
                RijndaelManaged aes = new RijndaelManaged();
                aes.BlockSize = 128;
                aes.KeySize = 256;

                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                byte[] keyArr = Convert.FromBase64String(key);
                byte[] KeyArrBytes32Value = new byte[32];
                Array.Copy(keyArr, KeyArrBytes32Value, 24);

                // Initialization vector.   
                // It could be any value or generated using a random number generator.
                byte[] ivArr = { 1, 2, 3, 4, 5, 6, 6, 5, 4, 3, 2, 1, 7, 7, 7, 7 };
                byte[] IVBytes16Value = new byte[16];
                Array.Copy(ivArr, IVBytes16Value, 16);

                aes.Key = KeyArrBytes32Value;
                aes.IV = IVBytes16Value;

                ICryptoTransform decrypto = aes.CreateDecryptor();

                byte[] encryptedBytes = Convert.FromBase64CharArray(CipherText.ToCharArray(), 0, CipherText.Length);
                byte[] decryptedData = decrypto.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                return ASCIIEncoding.UTF8.GetString(decryptedData);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
