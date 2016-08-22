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
    public class MainActivity : Activity
    {
        private MobileServiceClient client;	
        private IMobileServiceTable<CategoryItem> categoryTable;

        public List<CategoryItem> categoryItemList = new List<CategoryItem>();

        //private CategoryAdapter adapter;
        private CategoryItemAdapter adapter;

        private RecyclerView listViewCategory;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            SetContentView(Resource.Layout.Main_Activity);

            //adapter = new CategoryAdapter(this, Resource.Layout.Row_List_Category);

            //listViewCategory = FindViewById<ListView>(Resource.Id.listViewCategory);

            //listViewCategory.Adapter = adapter;

            adapter = new CategoryItemAdapter(this, FindViewById<RecyclerView>(Resource.Id.listViewCategory));

            listViewCategory = (RecyclerView)FindViewById(Resource.Id.listViewCategory);

            listViewCategory.SetLayoutManager(new LinearLayoutManager(this));

            listViewCategory.SetAdapter(adapter);

            CurrentPlatform.Init();

            client = new MobileServiceClient(
                Constants.applicationURL,
                Constants.applicationKey);

            categoryTable = client.GetTable<CategoryItem>();

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
                categoryItemList = await categoryTable.Where(item => item.CategoryName != null).OrderBy(x => x.CategoryName).ToListAsync();
                adapter.Clear();

                foreach (CategoryItem current in categoryItemList)
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