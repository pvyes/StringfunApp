using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace StringFunApp.Droid
{
    [Activity(Label = "Splashscreenactivity", Icon = "@drawable/iconm",
              Theme = "@style/MainTheme.Splash", MainLauncher = true,
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class Splashscreenactivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            StartActivity(new Intent(this, typeof(MainActivity)));
        }
    }
}