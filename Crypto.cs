using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace ATBMTT
{
    internal class Crypto
    {
        public static byte[] getEncryptor(string strKey, byte[] textByte, string strSalt)
        {
            byte[] keyByte = Encoding.UTF8.GetBytes(strKey);
            byte[] saltByte = null;
            if (string.IsNullOrEmpty(strSalt))
            {
                saltByte = new byte[] { 0x11, 0x00, 0x56, 0x43, 0x11, 0x77, 0x13, 0x88, 0x30, 0x04, 0x99, 0x54, 0x41, 0x83, 0x69, 0x51 };
            }
            else
            {
                saltByte = Encoding.UTF8.GetBytes(strSalt);
            }
            byte[] encryptedFile = null;

            try
            {
                using (var memory = new MemoryStream())
                {
                    using (var aes = new RijndaelManaged())
                    {
                        aes.Mode = CipherMode.CBC;
                        aes.Padding = PaddingMode.PKCS7;
                        aes.KeySize = 256;
                        aes.BlockSize = 128;

                        var key = new Rfc2898DeriveBytes(keyByte, saltByte, 2000);
                        aes.Key = key.GetBytes(aes.KeySize / 8);
                        aes.IV = key.GetBytes(aes.BlockSize / 8);

                        using (var ICrypto = new CryptoStream(memory, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            ICrypto.Write(textByte, 0, textByte.Length);
                            ICrypto.FlushFinalBlock();
                            ICrypto.Close();

                        }
                    }
                    encryptedFile = memory.ToArray();
                    memory.Close();
                }
                return encryptedFile;
            }
            catch
            {
                return null;
            }
        }

        public static byte[] getDecryptor(string strKey, byte[] textByte, string strSalt)
        {
            byte[] keyByte = Encoding.UTF8.GetBytes(strKey);
            byte[] saltByte = null;
            if (string.IsNullOrEmpty(strSalt))
            {
                saltByte = new byte[] { 0x11, 0x00, 0x56, 0x43, 0x11, 0x77, 0x13, 0x88, 0x30, 0x04, 0x99, 0x54, 0x41, 0x83, 0x69, 0x51 };
            }
            else
            {
                saltByte = Encoding.UTF8.GetBytes(strSalt);
            }
            byte[] encryptedFile = null;

            try
            {
                using (var memory = new MemoryStream())
                {
                    using (var aes = new RijndaelManaged())
                    {
                        aes.Mode = CipherMode.CBC;
                        aes.Padding = PaddingMode.PKCS7;
                        aes.KeySize = 256;
                        aes.BlockSize = 128;

                        var key = new Rfc2898DeriveBytes(keyByte, saltByte, 2000);
                        aes.Key = key.GetBytes(aes.KeySize / 8);
                        aes.IV = key.GetBytes(aes.BlockSize / 8);

                        using (var ICrypto = new CryptoStream(memory, aes.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            ICrypto.Write(textByte, 0, textByte.Length);
                            ICrypto.FlushFinalBlock();
                            ICrypto.Close();

                        }
                    }
                    encryptedFile = memory.ToArray();
                    memory.Close();
                }
                return encryptedFile;
            }
            catch
            {
                return null;
            }
        }

    }
}
