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
using System.Threading.Tasks;
using Android.Support.V7.Widget;

namespace quotation
{
    [Activity(MainLauncher = true,
              Icon = "@drawable/ic_launcher", Label = "@string/app_name",
              Theme = "@style/AppTheme")]
    public class WriterActivity : Activity
    {
        private MobileServiceClient client;
        private IMobileServiceTable<WriterItem> writerTable;

        public List<WriterItem> writerItemList = new List<WriterItem>();

        //private CategoryAdapter adapter;
        private WriterItemAdapter adapter;

        private RecyclerView listViewWriter;

        public string selectedItem;
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            selectedItem = Intent.Extras.GetString("selectedCategoryId");

            SetContentView(Resource.Layout.Writer_Activity);

            adapter = new WriterItemAdapter(this, FindViewById<RecyclerView>(Resource.Id.listViewWriter));

            listViewWriter = (RecyclerView)FindViewById(Resource.Id.listViewWriter);

            listViewWriter.SetLayoutManager(new LinearLayoutManager(this));

            listViewWriter.SetAdapter(adapter);

            CurrentPlatform.Init();

            client = new MobileServiceClient(
                Constants.applicationURL,
                Constants.applicationKey);

            writerTable = client.GetTable<WriterItem>();

            var tab = ActionBar.NewTab();
            tab.SetText(Resources.GetString(Resource.String.category_tab_text));
            //tab.SetIcon(Resource.Drawable.tab1_icon);
            tab.TabSelected += (sender, args) =>
            {

            };
            ActionBar.AddTab(tab);

            tab = ActionBar.NewTab();
            tab.SetText(Resources.GetString(Resource.String.author_tab_text));
            //tab.SetIcon(Resource.Drawable.tab2_icon);
            tab.TabSelected += async (sender, args) =>
            {
                await RefreshAuthorItemsFromTableAsync();
            };
            ActionBar.AddTab(tab);

            await RefreshItemsFromTableAsync();
        }

        private Task RefreshAuthorItemsFromTableAsync()
        {
            throw new NotImplementedException();
        }

        async Task RefreshItemsFromTableAsync()
        {
            // TODO:: Uncomment the following code when using a mobile service
            try
            {
                // Get the items that weren't marked as completed and add them in the adapter
                writerItemList = await writerTable.Where(item => item.FkCategoryId == selectedItem).ToListAsync();
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