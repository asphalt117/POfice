//using System;
//using System.Text;
//using System.Security.Cryptography;
//using System.IO;
//namespace Domain.Engine
//{
//    public static class MyCrypto
//    {
//        private static string pass = "password";
//        private static string sol = "doberman";
//        private static string cryptographicAlgorithm = "SHA1";
//        private static int passIter = 2;
//        private static string initVec = "a8doSuDitOz1hZe#";
//        private static int keySize = 256;

//        //метод шифрования строки
//        public static string Shifrovka(string ishText)
//        {
//            if (string.IsNullOrEmpty(ishText))
//                return "";

//            byte[] initVecB = Encoding.ASCII.GetBytes(initVec);
//            byte[] solB = Encoding.ASCII.GetBytes(sol);
//            byte[] ishTextB = Encoding.UTF8.GetBytes(ishText);

//            PasswordDeriveBytes derivPass = new PasswordDeriveBytes(pass, solB, cryptographicAlgorithm, passIter);
//            byte[] keyBytes = derivPass.GetBytes(keySize / 8);
//            RijndaelManaged symmK = new RijndaelManaged();
//            symmK.Mode = CipherMode.CBC;

//            byte[] cipherTextBytes = null;

//            using (ICryptoTransform encryptor = symmK.CreateEncryptor(keyBytes, initVecB))
//            {
//                using (MemoryStream memStream = new MemoryStream())
//                {
//                    using (CryptoStream cryptoStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write))
//                    {
//                        cryptoStream.Write(ishTextB, 0, ishTextB.Length);
//                        cryptoStream.FlushFinalBlock();
//                        cipherTextBytes = memStream.ToArray();
//                        memStream.Close();
//                        cryptoStream.Close();
//                    }
//                }
//            }

//            symmK.Clear();
//            return Convert.ToBase64String(cipherTextBytes);
//        }

//        //метод дешифрования строки
//        public static string DeShifrovka(string ciphText)
//        {
//            if (string.IsNullOrEmpty(ciphText))
//                return "";

//            byte[] initVecB = Encoding.ASCII.GetBytes(initVec);
//            byte[] solB = Encoding.ASCII.GetBytes(sol);
//            byte[] cipherTextBytes = Convert.FromBase64String(ciphText);

//            PasswordDeriveBytes derivPass = new PasswordDeriveBytes(pass, solB, cryptographicAlgorithm, passIter);
//            byte[] keyBytes = derivPass.GetBytes(keySize / 8);

//            RijndaelManaged symmK = new RijndaelManaged();
//            symmK.Mode = CipherMode.CBC;

//            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
//            int byteCount = 0;

//            using (ICryptoTransform decryptor = symmK.CreateDecryptor(keyBytes, initVecB))
//            {
//                using (MemoryStream mSt = new MemoryStream(cipherTextBytes))
//                {
//                    using (CryptoStream cryptoStream = new CryptoStream(mSt, decryptor, CryptoStreamMode.Read))
//                    {
//                        byteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
//                        mSt.Close();
//                        cryptoStream.Close();
//                    }
//                }
//            }

//            symmK.Clear();
//            return Encoding.UTF8.GetString(plainTextBytes, 0, byteCount);
//        }
//    }
//}