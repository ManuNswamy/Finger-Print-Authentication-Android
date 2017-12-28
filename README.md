# Finger-Print-Authentication-Android

Step 1 > USE_FINGERPRINT - Permission

Request the permission to use the touch sensor and the fingerprint authentication. 
So in the Manifest.xml, we add:

<uses-permission android:name="android.permission.USE_FINGERPRINT" />

Step 2> KeyGuardManager and FingerprintManager

// Keyguard Manager
KeyguardManager keyguardManager = (KeyguardManager)
                  GetSystemService(KeyguardService);

// Fingerprint Manager
FingerprintManager fingerprintManager = (FingerprintManager) 
                 GetSystemService(FingerprintService);
                 

Step 2A> Check whether the finger print sensor is available

 if (!fingerprintManager.IsHardwareDetected) {     
    //Fingerprint authentication not supported
   }

Step 2B> Check whether the user has atleast one finger print registered in the device settings

if (!fingerprintManager.HasEnrolledFingerprints) {
     //No fingerprint configured     
   }
   
Step 2C> Check whether the lock screen is enabled

   if (!keyguardManager.IsKeyguardSecure) {
      //Secure lock screen not enabled"   
   }
   
Step 3> Get the reference and initialize KeyStore and KeyGenerator

keyStore = KeyStore.GetInstance("AndroidKeyStore");

// Key generator to generate the key
keyGenerator = KeyGenerator.GetInstance(KeyProperties.KeyAlgorithmAes, "AndroidKeyStore");

//we specify the usage of the key: for encryption and decryption
 keyGenerator.Init(new KeyGenParameterSpec.Builder(KEY_NAME, KeyStorePurpose.Encrypt | KeyStorePurpose.Decrypt)
                .SetBlockModes(KeyProperties.BlockModeCbc)
                .SetUserAuthenticationRequired(true)
                .SetEncryptionPaddings(KeyProperties.EncryptionPaddingPkcs7)
                .Build());

Step 4> Generate the key

keyGenerator.GenerateKey();

Step 5> Create Cipher

 try 
 {
    Cipher cipher = Cipher.getInstance(KeyProperties.KEY_ALGORITHM_AES + "/"
     + KeyProperties.BLOCK_MODE_CBC 
     +"/"
     + KeyProperties.ENCRYPTION_PADDING_PKCS7);
     SecretKey key = (SecretKey) keyStore.getKey(KEY_NAME,
             null);
     cipher.init(Cipher.ENCRYPT_MODE, key);
     return cipher;
  }
  catch (Exception)
  {
    
  }
  
Step 6> Pass the Object of FingerprintManager.CryptoObject and FingerprintManager to the helper class to authenticate the user.  
   
   
