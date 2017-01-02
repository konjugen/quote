using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Microsoft.WindowsAzure.MobileServices;
using quotation.DTO;
using quotation.Adapters;
using System.Threading.Tasks;
using Android.Gms.Ads;
using Android.Support.V7.Widget;

namespace quotation
{
    [Activity(MainLauncher = false, Label = "@string/app_name", Icon = "@drawable/icon", Theme = "@style/AppTheme")]
    public class WriterActivity : Activity
    {
        private MobileServiceClient client;
        private IMobileServiceTable<WriterItem> writerTable;
        public List<WriterItem> writerItemList = new List<WriterItem>();
        //private CategoryAdapter adapter;
        private WriterItemAdapter adapter;

        private RecyclerView listViewWriter;
        private ProgressDialog _progressDialog;

        //private AutoCompleteTextView actv;

        //private SearchAdapter searchAdapter;

        public string selectedItem;
        protected AdView mAdView;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            selectedItem = Intent.Extras.GetString("selectedCategoryId");

            SetContentView(Resource.Layout.Writer_Activity);

            mAdView = FindViewById<AdView>(Resource.Id.adViewWriter);
            var adRequest = new AdRequest.Builder().Build();
            mAdView.LoadAd(adRequest);

            GAService.GetGASInstance().Initialize(this);

            _progressDialog = new ProgressDialog(this);
            _progressDialog.Indeterminate = true;
            _progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
            _progressDialog.SetCancelable(false);

            _progressDialog.SetMessage("Loading...");


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
            _progressDialog.Show();
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
                //var firebase = new FirebaseClient("https://best-quotes-24f45.firebaseio.com/");
                //ContentList = await firebase
                // .Child("Content")
                // .OnceAsync<List<Content>>();

                //List<Content> _contentByCategory, _contentByWriter;

                //foreach (var item in ContentList)
                //{
                //    _contentByCategory = item.Object.Where(q => q.FkCategoryId.ToString() == selectedItem).OrderBy(x => x.WriterName).ToList();
                //}

                //foreach (var item in ContentList)
                //{
                //    _contentByWriter =
                //        item.Object.Where(q => q.FkWriterId.ToString() == selectedItem).OrderBy(x => x.WriterName).ToList();
                //}
                // Get the items that weren't marked as completed and add them in the adapter
                writerItemList = await writerTable.Where(item => item.CategoryName == selectedItem).OrderBy(x => x.WriterName).ToListAsync();
                if (writerItemList.Count == 0)
                    writerItemList = await writerTable.Where(item => item.WriterName == selectedItem).OrderBy(x => x.WriterName).ToListAsync();
                adapter.Clear();

                foreach (var current in writerItemList)
                    adapter.Add(current);
                _progressDialog.Dismiss();

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

        protected override void OnPause()
        {
            mAdView?.Pause();
            base.OnPause();
        }

        protected override void OnResume()
        {
            base.OnResume();
            mAdView?.Resume();
        }

        protected override void OnDestroy()
        {
            mAdView?.Destroy();
            base.OnDestroy();
        }
    }
}