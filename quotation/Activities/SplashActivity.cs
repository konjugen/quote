using System.Timers;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using System.Threading.Tasks;
using Android.Content;

namespace quotation.Activities
{
    [Activity(Theme = "@style/SplashTheme", MainLauncher = true, NoHistory = true, Label = "@string/app_name", Icon = "@drawable/icon")]
    public class SplashActivity : AppCompatActivity
    {
        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            //RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState, persistentState);
            //SetContentView(Resource.Drawable.splash_screen);
        }
        protected override void OnResume()
        {
            base.OnResume();

            Task startupWork = new Task(() => {
                //Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
                Task.Delay(2000);  // Simulate a bit of startup work.
                //Log.Debug(TAG, "Working in the background - important stuff.");
            });

            startupWork.ContinueWith(t => {
                //Log.Debug(TAG, "Work is finished - start Activity1.");
                StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            }, TaskScheduler.FromCurrentSynchronizationContext());

            startupWork.Start();
        }
    }
}