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
    [Activity(MainLauncher = true, Label = "@string/app_name", Icon = "@drawable/ic_launcher", Theme = "@android:style/Theme.Material.Light")]
    public class MainActivity : Activity
    {
        private MobileServiceClient client;
        private IMobileServiceTable<CategoryItem> categoryTable;

        public List<CategoryItem> categoryItemList = new List<CategoryItem>();

        //private CategoryAdapter adapter;
        private CategoryItemAdapter adapter;
        private SearchAdapter searchAdapter;

        private AutoCompleteTextView actv;

        private RecyclerView listViewCategory;

        private ActionBar.Tab categoryTab, authorTab;

        Button dailyButton;

        protected override async void OnCreate(Bundle bundle)
        {
            
            base.OnCreate(bundle);

            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            SetContentView(Resource.Layout.Main_Activity);

            GAService.GetGASInstance().Initialize(this);  

            //adapter = new CategoryAdapter(this, Resource.Layout.Row_List_Category);

            //listViewCategory = FindViewById<ListView>(Resource.Id.listViewCategory);

            //listViewCategory.Adapter = adapter;  


            actv = (AutoCompleteTextView)FindViewById(Resource.Id.category_autocomplete_search);

            actv.Threshold = 1;

            var disp = WindowManager.DefaultDisplay;
            var widht = disp.Width;
            actv.DropDownWidth = widht;

            actv.ItemClick += actv_ItemClick;

            adapter = new CategoryItemAdapter(this, FindViewById<RecyclerView>(Resource.Id.listViewCategory));

            listViewCategory = (RecyclerView)FindViewById(Resource.Id.listViewCategory);

            listViewCategory.SetLayoutManager(new LinearLayoutManager(this));

            listViewCategory.SetAdapter(adapter);

            CurrentPlatform.Init();

            client = new MobileServiceClient(
                Constants.applicationURL,
                Constants.applicationKey);

            categoryTable = client.GetTable<CategoryItem>();

            categoryTab = ActionBar.NewTab();
            authorTab = ActionBar.NewTab();
            categoryTab.SetText(Resources.GetString(Resource.String.category_tab_text));
            //tab.SetIcon(Resource.Drawable.tab1_icon);
            categoryTab.TabSelected += (sender, args) =>
            {
                categoryTab.SetText(Resources.GetString(Resource.String.category_tab_text));
                RefreshItemsFromTableAsync();
            };
            ActionBar.AddTab(categoryTab);


            authorTab.SetText(Resources.GetString(Resource.String.author_tab_text));
            //tab.SetIcon(Resource.Drawable.tab2_icon);
            authorTab.TabSelected += (sender, args) =>
            {
                authorTab.SetText(Resources.GetString(Resource.String.author_tab_text));
                RefreshItemsFromTableAsync();
            };
            ActionBar.AddTab(authorTab);

            dailyButton = FindViewById<Button>(Resource.Id.dailyButton);
            var text = await categoryTable.Where(q => q.IsDaily).Select(s => s.WriterName).ToListAsync();
            dailyButton.Text = text[0];
            dailyButton.Click += (sender, args) =>
            {
                RefreshDailyItems();
            };
        }

        private void RefreshDailyItems()
        {
            StartActivity(typeof(DailyActivity));
        }

        //async Task RefreshAuthorItemsFromTableAsync()
        //{
        //    try
        //    {
        //        // Get the items that weren't marked as completed and add them in the adapter
        //        categoryItemList = await categoryTable.Where(item => item.WriterName != null).OrderBy(x => x.WriterName).ToListAsync();
        //        adapter.Clear();

        //        foreach (CategoryItem current in categoryItemList)
        //            adapter.Add(current);

        //        searchAdapter = new SearchAdapter(this);

        //        searchAdapter.OriginalItems = categoryItemList.Select(s => s.WriterName).ToArray();
        //        var disp = WindowManager.DefaultDisplay;
        //        var widht = disp.Width;
        //        actv.DropDownWidth = widht;
        //        actv.Adapter = searchAdapter;

        //        actv.ItemClick += actv_ItemClick;
        //    }
        //    catch (Exception e)
        //    {
        //        CreateAndShowDialog(e, "Error");
        //    }
        //}

        public async void RefreshItemsFromTableAsync()
        {
            // TODO:: Uncomment the following code when using a mobile service
            try
            {
                // Get the items that weren't marked as completed and add them in the adapter
                searchAdapter = new SearchAdapter(this);
                if (ActionBar.SelectedTab == authorTab)
                {                   
                    categoryItemList = await categoryTable.Where(item => item.WriterName != null).OrderBy(x => x.WriterName).ToListAsync();
                    searchAdapter.OriginalItems = categoryItemList.Select(s => s.WriterName).ToArray();
                    actv.Adapter = searchAdapter;
                    adapter.Clear();
                }
                else
                {
                    categoryItemList = await categoryTable.Where(item => item.CategoryName != null).OrderBy(x => x.CategoryName).ToListAsync();
                    searchAdapter.OriginalItems = categoryItemList.Select(s => s.CategoryName).ToArray();
                    actv.Adapter = searchAdapter;
                    adapter.Clear();

                    foreach (CategoryItem current in categoryItemList)
                        adapter.Add(current);
                }
            }
            catch (Exception e)
            {
                CreateAndShowDialog(e, "Error");
            }
        }

        private void actv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent intent = new Intent(listViewCategory.Context, typeof(WriterActivity));
            //var id = ((View)sender).Id;
            intent.PutExtra("selectedCategoryId", e.View.Tag.ToString());
            listViewCategory.Context.StartActivity(intent);
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