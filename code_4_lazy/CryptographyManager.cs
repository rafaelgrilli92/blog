using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace code_4_lazy
{
    public class CryptographyManager
    {
        private byte[] _keyByte = { };
        //Default Key
        private static string _key = WebConfigurationManager.AppSettings["hashKey"].ToString();
        //Default initial vector
        private byte[] _ivByte = { 0x01, 0x12, 0x23, 0x34, 0x45, 0x56, 0x67, 0x78 };

        public CryptographyManager()
        {
        }

        /// <summary> 
        /// Encrypt text by key with initialization vector 
        /// </summary> 
        /// <param name="value">plain text</param> 
        /// <param name="key"> string key</param> 
        /// <param name="iv">initialization vector</param> 
        /// <returns>encrypted text</returns> 
        public string Encrypt(string value)
        {
            string encryptValue = string.Empty;
            MemoryStream ms = null;
            CryptoStream cs = null;
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    if (!string.IsNullOrEmpty(_key))
                    {
                        _keyByte = Encoding.UTF8.GetBytes
                                (_key.Substring(0, 8));
                    }
                    else
                    {
                        _keyByte = Encoding.UTF8.GetBytes(_key);
                    }
                    using (DESCryptoServiceProvider des =
                            new DESCryptoServiceProvider())
                    {
                        byte[] inputByteArray =
                        Encoding.UTF8.GetBytes(value);
                        ms = new MemoryStream();
                        cs = new CryptoStream(ms, des.CreateEncryptor
                        (_keyByte, _ivByte), CryptoStreamMode.Write);
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        encryptValue = Convert.ToBase64String(ms.ToArray());
                    }
                }
                catch
                {
                    //TODO: write log 
                }
                finally
                {
                    cs.Dispose();
                    ms.Dispose();
                }
            }
            return encryptValue;
        }

        /// <summary> 
        /// Decrypt text by key with initialization vector 
        /// </summary> 
        /// <param name="value">encrypted text</param> 
        /// <param name="key"> string key</param> 
        /// <param name="iv">initialization vector</param> 
        /// <returns>encrypted text</returns> 
        public string Decrypt(string value)
        {
            string decrptValue = string.Empty;
            if (!string.IsNullOrEmpty(value))
            {
                MemoryStream ms = null;
                CryptoStream cs = null;
                value = value.Replace(" ", "+");
                byte[] inputByteArray = new byte[value.Length];
                try
                {
                    if (!string.IsNullOrEmpty(_key))
                    {
                        _keyByte = Encoding.UTF8.GetBytes
                                (_key.Substring(0, 8));
                    }
                    else
                    {
                        _keyByte = Encoding.UTF8.GetBytes(_key);
                    }
                    using (DESCryptoServiceProvider des =
                            new DESCryptoServiceProvider())
                    {
                        inputByteArray = Convert.FromBase64String(value);
                        ms = new MemoryStream();
                        cs = new CryptoStream(ms, des.CreateDecryptor
                        (_keyByte, _ivByte), CryptoStreamMode.Write);
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        Encoding encoding = Encoding.UTF8;
                        decrptValue = encoding.GetString(ms.ToArray());
                    }
                }
                catch (Exception ex)
                {
                    //TODO: write log 
                }
                finally
                {
                    if (cs != null)
                        cs.Dispose();

                    if (ms != null)
                        ms.Dispose();
                }
            }
            return decrptValue;
        }

    }
}