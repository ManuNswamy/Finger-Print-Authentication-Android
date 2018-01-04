using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Android.Preferences;
using Android.Hardware.Fingerprints;

namespace FingerPrintAuth
{
    [Activity(Label = "Normal_Auth_Activity")]
    public class Normal_Auth_Activity : AppCompatActivity
    {
        FingerprintManager fingerprintManager;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.normal_auth_layout);

            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editor = prefs.Edit();

            EditText username = FindViewById<EditText>(Resource.Id.edit_text_username);
            EditText password = FindViewById<EditText>(Resource.Id.edit_text_password);
            Button button_login = FindViewById<Button>(Resource.Id.button_login);

            fingerprintManager = (FingerprintManager)GetSystemService(FingerprintService);
            AuthService authService = new AuthService();

            button_login.Click += delegate
            {
                if (authService.UserName == null && authService.Password ==null)
                {
                    
                    authService.SaveCredentials(username.Text, password.Text);
                    Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
                    Android.App.AlertDialog alert = dialog.Create();
                    alert.SetTitle("Finger Print Authentication");
                    alert.SetMessage("Do you want to enable finger print authentication ?");
                    alert.SetIcon(Resource.Drawable.fingerprint_icon);
                    alert.SetButton("Yes", (c, ev) =>
                    {
                        editor.PutBoolean("FingerPrintAuth", true);
                        editor.Commit();
                        this.StartActivity(new Intent(this, typeof(MainActivity)));
                    });
                    alert.SetButton2("No", (c, ev) => {
                        editor.PutBoolean("FingerPrintAuth", false);
                        this.StartActivity(new Intent(this, typeof(MainActivity)));
                    });

                    //check whether finger print sensor is present on the device
                    if (fingerprintManager.IsHardwareDetected)
                    {
                        //if Yes then display the message to enable finger print authentication
                        alert.Show();
                    }
                    else
                    {
                        Toast.MakeText(this, "Login Successful", ToastLength.Long).Show();                        
                    }
                }
                else
                {
                    if (authService.UserName == username.Text && authService.Password == password.Text)
                    {
                        Toast.MakeText(this, "Login Successful", ToastLength.Long).Show();
                    }
                    else
                    {
                        Toast.MakeText(this, "Invalid Login. Please Check the username and password and try again", ToastLength.Long).Show();
                    }
                }

            };
        }
    }
}