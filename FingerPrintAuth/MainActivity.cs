using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Preferences;
using Android.Hardware.Fingerprints;
using Android.Support.V4.App;
using Android;
using Java.Security;
using Javax.Crypto;
using Android.Security.Keystore;

namespace FingerPrintAuth
{
    [Activity(Label = "@string/app_name", MainLauncher = true, LaunchMode = Android.Content.PM.LaunchMode.SingleTop, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        KeyguardManager keyguardManager;
        FingerprintManager fingerprintManager;
        private KeyStore keyStore;
        private Cipher cipher;
        private string KEY_NAME = "FingerPrintAuth";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //Set your main view here
            //SetContentView(Resource.Layout.main);

            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editor = prefs.Edit();

            keyguardManager = (KeyguardManager)GetSystemService(KeyguardService);
            fingerprintManager = (FingerprintManager)GetSystemService(FingerprintService);

            //check whether Finger Print Authentication is enabled
            if (prefs.GetBoolean("FingerPrintAuth", false))
            {
                SetContentView(Resource.Layout.fingerprint_layout);
                if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.UseFingerprint) != (int)Android.Content.PM.Permission.Granted)
                {
                    return;
                }
                if (!fingerprintManager.HasEnrolledFingerprints)
                {
                    Toast.MakeText(this, "You need to atleast register one finger print in your device settings", ToastLength.Long).Show();
                }
                else
                {
                    if (!keyguardManager.IsKeyguardSecure)
                        Toast.MakeText(this, "Lock screen security not enable in your device settings", ToastLength.Long).Show();
                    else
                        Genkey();

                    if (CipherInit())
                    {
                        FingerprintManager.CryptoObject cryptoObject = new FingerprintManager.CryptoObject(cipher);
                        FingerprintHandler helper = new FingerprintHandler(this);
                        helper.StartAuthentication(fingerprintManager, cryptoObject);
                    }
                }
            }
            else
            {
                this.StartActivity(new Intent(this, typeof(Normal_Auth_Activity)));
            }
        }

        private bool CipherInit()
        {
            try
            {
                cipher = Cipher.GetInstance(KeyProperties.KeyAlgorithmAes
                    + "/"
                    + KeyProperties.BlockModeCbc
                    + "/"
                    + KeyProperties.EncryptionPaddingPkcs7);
                keyStore.Load(null);
                IKey key = (IKey)keyStore.GetKey(KEY_NAME, null);
                cipher.Init(CipherMode.EncryptMode, key);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void Genkey()
        {
            keyStore = KeyStore.GetInstance("AndroidKeyStore");
            KeyGenerator keyGenerator = null;
            keyGenerator = KeyGenerator.GetInstance(KeyProperties.KeyAlgorithmAes, "AndroidKeyStore");
            keyStore.Load(null);
            keyGenerator.Init(new KeyGenParameterSpec.Builder(KEY_NAME, KeyStorePurpose.Encrypt | KeyStorePurpose.Decrypt)
                .SetBlockModes(KeyProperties.BlockModeCbc)
                .SetUserAuthenticationRequired(true)
                .SetEncryptionPaddings(KeyProperties.EncryptionPaddingPkcs7)
                .Build());
            keyGenerator.GenerateKey();
        }
    }
}

