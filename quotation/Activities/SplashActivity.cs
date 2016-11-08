using System.Timers;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace quotation.Activities
{
    [Activity(MainLauncher = true, Label = "@string/app_name", Icon = "@drawable/icon", Theme = "@style/AppTheme")]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.splash_screen);

            var timer = new Timer
            {
                Interval = 5000,
                AutoReset = false
            };
            // 3 sec.
            // Do not reset the timer after it's elapsed
            timer.Elapsed += (sender, e) =>
            {
                StartActivity(typeof(MainActivity));
            };
            timer.Start();
        }
    };
}