
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Excelsior.Infrastructure.Utilities
{
    public static class Security
    {
        static byte[] bytes1Short = ASCIIEncoding.ASCII.GetBytes("EyeKor20");
        static byte[] bytes2Short = ASCIIEncoding.ASCII.GetBytes("EyeKor12");
        //private static HashAlgorithm passwordHasher = System.Security.Cryptography.HashAlgorithm.Create("SHA1");

        static byte[] bytes1New = ASCIIEncoding.ASCII.GetBytes("EyeKor$$Excelsior$$Api$$");
        static byte[] bytes2New = ASCIIEncoding.ASCII.GetBytes("EyeKor$$");

        public static string Desencript(string password, string passwordSalt)
        {
            using (var algorithm = SHA1.Create())
            {
                //var user = GlobalApplication.Database.Users.FirstOrDefault(u => u.UserName == username);
                //if (user == null) return false;
                byte[] saltBytes = Convert.FromBase64String(passwordSalt);
                byte[] passwordBytes = Encoding.Unicode.GetBytes(password);
                byte[] bytesToHash = new byte[saltBytes.Length + passwordBytes.Length];
                saltBytes.CopyTo(bytesToHash, 0);
                passwordBytes.CopyTo(bytesToHash, saltBytes.Length);
                byte[] hash = algorithm.ComputeHash(bytesToHash);
                string base64Hash = Convert.ToBase64String(hash);
                return base64Hash;
            }

        }

        public static string EncodePIN(string pin, int passwordFormat, string salt)
        {
            if (passwordFormat == 0) // MembershipPasswordFormat.Clear
                return pin;

            byte[] bIn = Encoding.Unicode.GetBytes(pin);
            byte[] bSalt = Convert.FromBase64String(salt);
            byte[] bRet = null;

            if (passwordFormat == 1)
            { // MembershipPasswordFormat.Hashed 
                HashAlgorithm hm = HashAlgorithm.Create("HMACSHA256");
                if (hm is KeyedHashAlgorithm)
                {
                    KeyedHashAlgorithm kha = (KeyedHashAlgorithm)hm;
                    if (kha.Key.Length == bSalt.Length)
                    {
                        kha.Key = bSalt;
                    }
                    else if (kha.Key.Length < bSalt.Length)
                    {
                        byte[] bKey = new byte[kha.Key.Length];
                        Buffer.BlockCopy(bSalt, 0, bKey, 0, bKey.Length);
                        kha.Key = bKey;
                    }
                    else
                    {
                        byte[] bKey = new byte[kha.Key.Length];
                        for (int iter = 0; iter < bKey.Length;)
                        {
                            int len = Math.Min(bSalt.Length, bKey.Length - iter);
                            Buffer.BlockCopy(bSalt, 0, bKey, iter, len);
                            iter += len;
                        }
                        kha.Key = bKey;
                    }
                    bRet = kha.ComputeHash(bIn);
                }
                else
                {
                    byte[] bAll = new byte[bSalt.Length + bIn.Length];
                    Buffer.BlockCopy(bSalt, 0, bAll, 0, bSalt.Length);
                    Buffer.BlockCopy(bIn, 0, bAll, bSalt.Length, bIn.Length);
                    bRet = hm.ComputeHash(bAll);
                }
            }

            return Convert.ToBase64String(bRet);
        }

        public static string EncodePassword(string pass, int passwordFormat, string salt, bool setHashAlgorithm = false)
        {
            if (passwordFormat == 0) // MembershipPasswordFormat.Clear
                return pass;

            byte[] bIn = Encoding.Unicode.GetBytes(pass);
            byte[] bSalt = Convert.FromBase64String(salt);
            byte[] bAll = new byte[bSalt.Length + bIn.Length];
            byte[] bRet = null;

            Buffer.BlockCopy(bSalt, 0, bAll, 0, bSalt.Length);
            Buffer.BlockCopy(bIn, 0, bAll, bSalt.Length, bIn.Length);
            if (passwordFormat == 1)
            { // MembershipPasswordFormat.Hashed
              //HashAlgorithm s = HashAlgorithm.Create(setHashAlgorithm ? "SHA1" : Membership.HashAlgorithmType);
              //HashAlgorithm s = HashAlgorithm.Create("SHA1");
                var algorithm = SHA1.Create();
                bRet = algorithm.ComputeHash(bAll);


                //bRet = s.ComputeHash(bAll);
            }
            else
            {
                //bRet = EncryptPassword(bAll);
            }

            return Convert.ToBase64String(bRet);
        }

        //public string UnEncodePassword(string pass, int passwordFormat)
        //{
        //    switch (passwordFormat)
        //    {
        //   case 0: // MembershipPasswordFormat.Clear:
        //  return pass;
        //   case 1: // MembershipPasswordFormat.Hashed:
        //  throw new ProviderException(SR.GetString(SR.Provider_can_not_decode_hashed_password));
        //   default:
        //  byte[] bIn = Convert.FromBase64String(pass);
        //  byte[] bRet = DecryptPassword(bIn);
        //  if (bRet == null)
        // return null;
        //  return Encoding.Unicode.GetString(bRet, 16, bRet.Length - 16);
        //    }
        //}

        /// <summary>
        /// Encrypt a string.
        /// </summary>
        /// <param name="originalString">The original string.</param>
        /// <returns>The encrypted string.</returns>
        /// <exception cref="ArgumentNullException">This exception will be thrown when the original string is null or empty.</exception>
        public static string Encrypt(string originalString)
        {
            if (String.IsNullOrEmpty(originalString))
            {
                throw new ArgumentNullException("The string which needs to be encrypted can not be null.");
                //return originalString;
            }

            try
            {

                //DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();

                var cryptoProvider = TripleDES.Create().CreateEncryptor(bytes1New, bytes2New);

                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider, CryptoStreamMode.Write);

                StreamWriter writer = new StreamWriter(cryptoStream);
                writer.Write(originalString);
                writer.Flush();
                cryptoStream.FlushFinalBlock();
                writer.Flush();

                ArraySegment<byte> stream;
                bool res = memoryStream.TryGetBuffer(out stream);
                if (res)
                {
                    return Convert.ToBase64String(stream.Array, 0, (int)memoryStream.Length);
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                return originalString;
            }
        }

        /// <summary>
        /// Decrypt a crypted string.
        /// </summary>
        /// <param name="cryptedString">The crypted string.</param>
        /// <returns>The decrypted string.</returns>

        public static string Decrypt(string cryptedString)
        {
            if (String.IsNullOrEmpty(cryptedString))
            {
                return null;
            }

            try
            {
                //DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(cryptedString));
                CryptoStream cryptoStream = new CryptoStream(memoryStream, TripleDES.Create().CreateDecryptor(bytes1New, bytes2New), CryptoStreamMode.Read);
                StreamReader reader = new StreamReader(cryptoStream);
                return reader.ReadToEnd();
            }
            catch
            {
                return cryptedString;
            }
        }


        public static string EncryptShort(string originalString)
        {
            if (String.IsNullOrEmpty(originalString))
            {
                throw new ArgumentNullException("The string which needs to be encrypted can not be null.");
            }

            try
            {
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(bytes1Short, bytes2Short), CryptoStreamMode.Write);

                StreamWriter writer = new StreamWriter(cryptoStream);
                writer.Write(originalString);
                writer.Flush();
                cryptoStream.FlushFinalBlock();
                writer.Flush();

                return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
            }
            catch
            {
                return originalString;
            }
        }


        public static string DecryptShort(string cryptedString)
        {
            if (String.IsNullOrEmpty(cryptedString))
            {
                return null;
            }

            try
            {
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(cryptedString));
                CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(bytes1Short, bytes2Short), CryptoStreamMode.Read);
                StreamReader reader = new StreamReader(cryptoStream);

                return reader.ReadToEnd();
            }
            catch
            {
                return cryptedString;
            }
        }

        public static string GenerateSalt()
        {
            byte[] buf = new byte[16];
            (new RNGCryptoServiceProvider()).GetBytes(buf);
            return Convert.ToBase64String(buf);
        }
        public static bool ValidateParameter(string param, bool checkForNull, bool checkIfEmpty, bool checkForCommas, int maxSize)
        {
            if (param == null)
            {
                return !checkForNull;
            }

            param = param.Trim();
            if ((checkIfEmpty && param.Length < 1) ||
            (maxSize > 0 && param.Length > maxSize) ||
            (checkForCommas && param.Contains(",")))
            {
                return false;
            }

            return true;
        }
        public static bool IsValidEmail(string strIn)
        {
            try
            {
                return Regex.IsMatch(strIn, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            }
            catch (Exception)
            {
                return false;
            }

        }
        public enum MembershipPasswordFormat
        {
            //Passwords are not encrypted.
            Clear = 0,
            //Passwords are encrypted one-way using the SHA1 hashing algorithm.
            Hashed = 1,
            //Passwords are encrypted using the encryption settings determined by the machineKey
            //Element (ASP.NET Settings Schema) element configuration.
            Encrypted = 2
        }

        public enum MembershipCreateStatus
        {
            [StringValue("The user was successfully created.")]
            Success = 0,
            [StringValue("The user name was not found in the database.")]
            InvalidUserName = 1,
            [StringValue("The password is not formatted correctly.")]
            InvalidPassword = 2,
            [StringValue("The password question is not formatted correctly.")]
            InvalidQuestion = 3,
            [StringValue("The password answer is not formatted correctly.")]
            InvalidAnswer = 4,
            [StringValue("The e-mail address is not formatted correctly.")]
            InvalidEmail = 5,
            [StringValue("The user name already exists in the database for the application.")]
            DuplicateUserName = 6,
            [StringValue("The e-mail address already exists in the database for the application.")]
            DuplicateEmail = 7,
            [StringValue("The user was not created, for a reason defined by the provider.")]
            UserRejected = 8,
            [StringValue("The provider user key is of an invalid type or format.")]
            InvalidProviderUserKey = 9,
            [StringValue("The provider user key already exists in the database for the application.")]
            DuplicateProviderUserKey = 10,
            [StringValue("The provider returned an error that is not described by other System.Web.Security.MembershipCreateStatus")]
            ProviderError = 11,
            [StringValue("Password must contain 7 characters at minimum.")]
            MinimunPassword = 12,
            [StringValue("Password contains at most 50 characters at maximum.")]
            MaximunPassword = 13,
            [StringValue("Password must contain at least one special character (i.e. @, #, $, %).")]
            InvalidFormatPassword = 14,
        }
    }
}
