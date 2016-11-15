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
using Microsoft.WindowsAzure.MobileServices;
using quotation.DTO;
using quotation.Adapters;
using Android.Support.V7.Widget;
using System.Threading.Tasks;

namespace quotation
{
    [Activity(MainLauncher = false, Label = "@string/app_name", Icon = "@drawable/icon", Theme = "@style/AppTheme")]
    public class DailyActivity : Activity
    {
        private MobileServiceClient client;
        private IMobileServiceTable<WriterItem> writerTable;

        public List<WriterItem> writerItemList = new List<WriterItem>();

        private DailyAdapter adapter;

        private RecyclerView listViewDaily;

        public string selectedItem;
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Daily_Activity);

            GAService.GetGASInstance().Initialize(this);

            adapter = new DailyAdapter(this, FindViewById<RecyclerView>(Resource.Id.listViewDaily));

            listViewDaily = (RecyclerView)FindViewById(Resource.Id.listViewDaily);

            listViewDaily.SetLayoutManager(new LinearLayoutManager(this));

            listViewDaily.SetAdapter(adapter);

            CurrentPlatform.Init();

            client = new MobileServiceClient(
                Constants.applicationURL,
                Constants.applicationKey);

            writerTable = client.GetTable<WriterItem>();

            await RefreshItemsFromTableAsync();
        }

        async Task RefreshItemsFromTableAsync()
        {
            try
            {
                writerItemList = await writerTable.Where(q => q.IsDaily).ToListAsync();
                adapter.Clear();

                foreach (WriterItem current in writerItemList)
                    adapter.Add(current);
            }
            catch (Exception e)
            {
                CreateAndShowDialog(e, "Error");
            }
        }

        void CreateAndShowDialog(Exception exception, String title)
        {
            CreateAndShowDialog(exception.Message, title);
        }

        void CreateAndShowDialog(string message, string title)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);

            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.Create().Show();
        }
    }
}