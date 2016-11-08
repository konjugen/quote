using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Microsoft.WindowsAzure.MobileServices;
using quotation.DTO;
using quotation.Adapters;
using System.Threading.Tasks;
using Android.Support.V7.Widget;
using Android.Graphics;
using Android.Views;
using Android.Views.InputMethods;
using static Android.App.ActionBar;
using Button = Android.Widget.Button;
using ProgressBar = Android.Widget.ProgressBar;

namespace quotation
{
    [Activity(MainLauncher = false, Label = "@string/app_name", Icon = "@drawable/icon", Theme = "@style/AppTheme")]
    public class MainActivity : Activity
    {
        private MobileServiceClient _client;
        private IMobileServiceTable<CategoryItem> _categoryTable;

        public List<CategoryItem> CategoryItemList = new List<CategoryItem>();

        //private CategoryAdapter adapter;
        private CategoryItemAdapter _adapter;
        private SearchAdapter _searchAdapter;

        private AutoCompleteTextView _actv;

        private RecyclerView _listViewCategory;

        private Tab _categoryTab, _authorTab;

        private Button _dailyButton;
        private ProgressDialog _progressDialog;
        private TextView _textView;

        protected override async void OnCreate(Bundle bundle)
        {
            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            base.OnCreate(bundle);     

            SetContentView(Resource.Layout.Main_Activity);

            GAService.GetGASInstance().Initialize(this);

            _textView = (TextView)FindViewById(Resource.Id.searchBytext);

            _progressDialog = new ProgressDialog(this);
            _progressDialog.Indeterminate = true;
            _progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
            _progressDialog.SetCancelable(false);

            _progressDialog.SetMessage("Loading...");

            _actv = (AutoCompleteTextView)FindViewById(Resource.Id.category_autocomplete_search);
          
            _actv.Threshold = 1;

            var disp = WindowManager.DefaultDisplay;
            var widht = disp.Width;
            
            _actv.DropDownWidth = widht;

            _actv.ItemClick += actv_ItemClick;

            _adapter = new CategoryItemAdapter(this, FindViewById<RecyclerView>(Resource.Id.listViewCategory));

            _listViewCategory = (RecyclerView)FindViewById(Resource.Id.listViewCategory);

            _listViewCategory.SetLayoutManager(new LinearLayoutManager(this));

            _listViewCategory.SetAdapter(_adapter);

            CurrentPlatform.Init();

            _client = new MobileServiceClient(
                Constants.applicationURL,
                Constants.applicationKey);

            _categoryTable = _client.GetTable<CategoryItem>();

            _categoryTab = ActionBar.NewTab();
            _authorTab = ActionBar.NewTab();
            _categoryTab.SetText(Resources.GetString(Resource.String.category_tab_text));
            //tab.SetIcon(Resource.Drawable.tab1_icon);
            _categoryTab.TabSelected += (sender, args) =>
            {
                _categoryTab.SetText(Resources.GetString(Resource.String.category_tab_text));
                RefreshItemsFromTableAsync();
            };
            ActionBar.AddTab(_categoryTab);


            _authorTab.SetText(Resources.GetString(Resource.String.author_tab_text));
            //tab.SetIcon(Resource.Drawable.tab2_icon);
            _authorTab.TabSelected += (sender, args) =>
            {
                _authorTab.SetText(Resources.GetString(Resource.String.author_tab_text));
                RefreshItemsFromTableAsync();
            };
            ActionBar.AddTab(_authorTab);

            _dailyButton = FindViewById<Button>(Resource.Id.dailyButton);
            var text = await _categoryTable.Where(q => q.IsDaily).Select(s => s.WriterName).ToListAsync();
            _dailyButton.Text = text[0];
            _dailyButton.Click += (sender, args) =>
            {
                RefreshDailyItems();
            };
    }

        private void RefreshDailyItems()
        {
            StartActivity(typeof(DailyActivity));
        }

        public async void RefreshItemsFromTableAsync()
        {
            // TODO:: Uncomment the following code when using a mobile service
            try
            {
                // Get the items that weren't marked as completed and add them in the adapter
                _searchAdapter = new SearchAdapter(this);
                if (ActionBar.SelectedTab == _authorTab)
                {
                    _textView.Text = "Search By Author";
                    var layout = (LinearLayout)FindViewById(Resource.Id.LinearLayout1);
                    layout.SetBackgroundColor(Color.White);
                    var imageView = (ImageView)FindViewById(Resource.Id.authorBackGroundImageView);
                    imageView.SetBackgroundResource(Resource.Drawable.icon);
                    
                    _progressDialog.Show();
                    CategoryItemList = await _categoryTable.Where(item => item.WriterName != null).OrderBy(x => x.WriterName).ToListAsync();
                    _progressDialog.Dismiss();
                    _searchAdapter.OriginalItems = CategoryItemList.Select(s => s.WriterName).ToArray();
                    _actv.Adapter = _searchAdapter;
                    _adapter.Clear();
                }
                else
                {
                    _textView.Text = "Search By Category";

                    _progressDialog.Show();
                    CategoryItemList = await _categoryTable.Where(item => item.CategoryName != null).OrderBy(x => x.CategoryName).ToListAsync();
                    _progressDialog.Dismiss();
                    _searchAdapter.OriginalItems = CategoryItemList.Select(s => s.CategoryName).ToArray();
                    _actv.Adapter = _searchAdapter;
                    _adapter.Clear();

                    foreach (var current in CategoryItemList)
                        _adapter.Add(current);
                }
            }
            catch (Exception e)
            {
                CreateAndShowDialog(e, "Error");
            }
        }

        private void actv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent intent = new Intent(_listViewCategory.Context, typeof(WriterActivity));
            //var id = ((View)sender).Id;
            intent.PutExtra("selectedCategoryId", e.View.Tag.ToString());
            _listViewCategory.Context.StartActivity(intent);
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