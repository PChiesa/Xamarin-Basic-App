using System;
using System.Security.Cryptography;

namespace BasicApp.Totp
{
    public class InMemoryKey : IKeyProvider
    {
        #region platform supported

        static bool platformSupportTested = false;
        static bool platformSupported = false;
        static readonly object platformSupportSync = new object();

        static bool IsPlatformSupported()
        {
            // if you want to force in memory protection over the application working, uncomment the return true;
            // If the platform isn't actually supported but true is returned, the application won't work but it also
            // won't store plaintext keys.
            // return true;
            lock (platformSupportSync)
            {
                if (platformSupportTested)
                    return platformSupported;
                else
                {
                    try
                    {
                        var dummyData = new byte[16];
                        //ProtectedMemory.Protect(dummyData, MemoryProtectionScope.SameProcess);
                        platformSupported = true;
                    }
                    catch (PlatformNotSupportedException)
                    {
                        platformSupported = false;
                    }
                    finally
                    {
                        platformSupportTested = true;
                    }
                    return platformSupported;
                }
            }
        }

        #endregion platform supported

        readonly object stateSync = new object();
        readonly byte[] protectedKeyData;
        readonly int keyLength;
        bool isProtected;
        //readonly MemoryProtectionScope scope;

        #region static initializers

        /// <summary>
        /// Static method to create a protected key from a plaintext key.  It will wipe the reference that was passed in once the protected key instance is initialized
        /// </summary>
        /// <param name="plaintextKey">The key</param>
        /// <returns>A protected key instance from the provided key</returns>
        public static InMemoryKey CreateProtectedKeyAndDestroyPlaintextKey(byte[] plaintextKey)
        {
            var key = new InMemoryKey(plaintextKey);
            // the protected key creates a copy of the key and pads it as needed for in memory protection.
            // Thus the reference that was passed in isn't needed.  Overwrite it with random garbage.
            KeyUtilities.Destroy(plaintextKey);
            return key;
        }

        /// <summary>
        /// Creates an instance of the protected key from a byte array that has already been protected using the ProtectedMemory.Protect method call.
        /// </summary>
        /// <param name="preProtectedKey">Pre-protected key data</param>
        /// <param name="keyLength">The length of the plaintext key (protected memory may need to be padded)</param>
        /// <param name="scope">The memory protection scope that was used to protect the memory</param>
        /// <returns>A protected key instance from the provided key</returns>
        //public static InMemoryKey CreateProtectedKeyFromPreProtectedMemory(byte[] preProtectedKey, int keyLength, MemoryProtectionScope scope)
        //{
        //    return new InMemoryKey(preProtectedKey, keyLength, scope);
        //}

        #endregion

        /// <summary>
        /// Creates an instance of a protected key.
        /// </summary>
        /// <param name="key">Plaintext key data</param>
        public InMemoryKey(byte[] key)
        {
            if (!(key != null))
                throw new ArgumentNullException("key");
            if (!(key.Length > 0))
                throw new ArgumentException("The key must not be empty");

            this.keyLength = key.Length;
            int paddedKeyLength = (int)Math.Ceiling((decimal)key.Length / (decimal)16) * 16;
            this.protectedKeyData = new byte[paddedKeyLength];
            Array.Copy(key, this.protectedKeyData, key.Length);

            //if (IsPlatformSupported())
            //{
            //    this.scope = MemoryProtectionScope.SameProcess;
            //    ProtectedMemory.Protect(this.protectedKeyData, this.scope);

            //    this.isProtected = true;
            //}
        }

        //private InMemoryKey(byte[] preProtectedKey, int keyLength, MemoryProtectionScope scope)
        //{
        //    if (!(preProtectedKey != null))
        //        throw new ArgumentNullException("preProtectedKey");
        //    if (!(preProtectedKey.Length > 0))
        //        throw new ArgumentException("The key must not be empty");
        //    if (!(keyLength > 0))
        //        throw new ArgumentException("The key must not be empty");

        //    this.keyLength = keyLength;
        //    this.isProtected = true;
        //    this.protectedKeyData = preProtectedKey;
        //    //this.scope = scope;
        //}

        /// <summary>
        /// Gets a copy of the plaintext key
        /// </summary>
        /// <remarks>
        /// This is internal rather than protected so that the tests can use this method
        /// </remarks>
        /// <returns>Plaintext Key</returns>
        internal byte[] GetCopyOfKey()
        {
            var plainKey = new byte[this.keyLength];
            lock (this.stateSync)
            {
                try
                {
                    //if (this.isProtected)
                    //{
                    //    ProtectedMemory.Unprotect(this.protectedKeyData, this.scope);
                    //    this.isProtected = false;
                    //}
                    Array.Copy(this.protectedKeyData, plainKey, this.keyLength);
                }
                finally
                {
                    if (!this.isProtected && IsPlatformSupported())
                    {
                        //ProtectedMemory.Protect(this.protectedKeyData, this.scope);
                        this.isProtected = true;
                    }
                }
            }
            return plainKey;
        }

        /// <summary>
        /// Uses the key to get an HMAC using the specified algorithm and data
        /// </summary>
        /// <param name="mode">The HMAC algorithm to use</param>
        /// <param name="data">The data used to compute the HMAC</param>
        /// <returns>HMAC of the key and data</returns>
        public byte[] ComputeHmac(OtpHashMode mode, byte[] data)
        {
            byte[] hashedValue = null;
            using (HMAC hmac = CreateHmacHash(mode))
            {
                byte[] key = this.GetCopyOfKey();
                try
                {
                    hmac.Key = key;
                    hashedValue = hmac.ComputeHash(data);
                }
                finally
                {
                    KeyUtilities.Destroy(key);
                }
            }

            return hashedValue;
        }

        /// <summary>
        /// Create an HMAC object for the specified algorithm
        /// </summary>
        private static HMAC CreateHmacHash(OtpHashMode otpHashMode)
        {
            HMAC hmacAlgorithm = null;
            switch (otpHashMode)
            {
                case OtpHashMode.Sha256:
                    hmacAlgorithm = new HMACSHA256();
                    break;
                case OtpHashMode.Sha512:
                    hmacAlgorithm = new HMACSHA512();
                    break;
                default: //case OtpHashMode.Sha1:
                    hmacAlgorithm = new HMACSHA1();
                    break;
            }
            return hmacAlgorithm;
        }
    }
}
