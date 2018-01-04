using System;
using Android.Hardware.Fingerprints;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android;
using Android.Widget;

namespace FingerPrintAuth
{
    internal class FingerprintHandler:FingerprintManager.AuthenticationCallback
    {
        private Context context;

        public FingerprintHandler(Context context)
        {
            this.context = context;
        }

        internal void StartAuthentication(FingerprintManager fingerprintManager, FingerprintManager.CryptoObject cryptoObject)
        {
            CancellationSignal cenCancellationSignal = new CancellationSignal();
            if (ActivityCompat.CheckSelfPermission(context, Manifest.Permission.UseFingerprint) != (int)Android.Content.PM.Permission.Granted)
                return;
            fingerprintManager.Authenticate(cryptoObject, cenCancellationSignal, 0, this, null);
        }

        public override void OnAuthenticationFailed()
        {
            context.StartActivity(new Intent(context, typeof(Normal_Auth_Activity)));
            Toast.MakeText(context, "Fingerprint Authentication Failed !", ToastLength.Long).Show();
        }

        public override void OnAuthenticationSucceeded(FingerprintManager.AuthenticationResult result)
        {            
            Toast.MakeText(context, "Fingerprint Authentication Successful.", ToastLength.Long).Show();
           
        }

    }
}