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
    [Activity(Label = "@string/app_name", Icon = "@drawable/ic_launcher", Theme = "@android:style/Theme.Material.Light")]
    public class WriterActivity : Activity
    {
        private MobileServiceClient client;
        private IMobileServiceTable<WriterItem> writerTable;

        public List<WriterItem> writerItemList = new List<WriterItem>();

        //private CategoryAdapter adapter;
        private WriterItemAdapter adapter;

        private RecyclerView listViewWriter;

        //private AutoCompleteTextView actv;

        //private SearchAdapter searchAdapter;

        public string selectedItem;
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            selectedItem = Intent.Extras.GetString("selectedCategoryId");

            SetContentView(Resource.Layout.Writer_Activity);

            GAService.GetGASInstance().Initialize(this);

            //actv = (AutoCompleteTextView)FindViewById(Resource.Id.author_autocomplete_search);

            //actv.Threshold = 1;

            //searchAdapter = new SearchAdapter(this);

            adapter = new WriterItemAdapter(this, FindViewById<RecyclerView>(Resource.Id.listViewWriter));

            listViewWriter = (RecyclerView)FindViewById(Resource.Id.listViewWriter);

            listViewWriter.SetLayoutManager(new LinearLayoutManager(this));

            listViewWriter.SetAdapter(adapter);

            CurrentPlatform.Init();

            client = new MobileServiceClient(
                Constants.applicationURL,
                Constants.applicationKey);

            writerTable = client.GetTable<WriterItem>();

            //var tab = ActionBar.NewTab();
            //tab.SetText(Resources.GetString(Resource.String.category_tab_text));
            ////tab.SetIcon(Resource.Drawable.tab1_icon);
            //tab.TabSelected += (sender, args) =>
            //{

            //};
            //ActionBar.AddTab(tab);

            //tab = ActionBar.NewTab();
            //tab.SetText(Resources.GetString(Resource.String.author_tab_text));
            ////tab.SetIcon(Resource.Drawable.tab2_icon);
            //tab.TabSelected += async (sender, args) =>
            //{
            //    await RefreshAuthorItemsFromTableAsync();
            //};
            //ActionBar.AddTab(tab);

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
                writerItemList = await writerTable.Where(item => item.CategoryName == selectedItem).OrderBy(x => x.WriterName).ToListAsync();
                if(writerItemList.Count == 0)
                    writerItemList = await writerTable.Where(item => item.WriterName == selectedItem).OrderBy(x => x.WriterName).ToListAsync();
                adapter.Clear();

                foreach (WriterItem current in writerItemList)
                    adapter.Add(current);

                //searchAdapter.OriginalItems = writerItemList.Select(s => s.WriterName).ToArray();
                //var disp = WindowManager.DefaultDisplay;
                //var widht = disp.Width;
                //actv.DropDownWidth = widht;
                //actv.Adapter = searchAdapter;

                //actv.ItemClick += actv_ItemClick;
            }
            catch (Exception e)
            {
                CreateAndShowDialog(e, "Error");
            }
        }

        //private void actv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

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