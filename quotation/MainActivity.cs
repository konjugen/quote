using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Microsoft.WindowsAzure.MobileServices;
using quotation.DTO;
using quotation.Adapters;
using System.Threading.Tasks;
using Android.Gms.Ads;
using Android.Support.V7.Widget;
using Android.Graphics;
using Firebase.Database;
using static Android.App.ActionBar;
using Button = Android.Widget.Button;

namespace quotation
{
	[Activity(MainLauncher = false, Label = "@string/app_name", Icon = "@drawable/icon", Theme = "@style/AppTheme")]
	public class MainActivity : Activity
	{
		private MobileServiceClient _client;
		private IMobileServiceTable<CategoryItem> _categoryTable;

		public List<CategoryItem> CategoryItemList = new List<CategoryItem>();
		//public IReadOnlyCollection<FirebaseObject<List<Category>>> CategoryList = new List<FirebaseObject<List<Category>>>();
		//public IReadOnlyCollection<FirebaseObject<List<Writer>>> WriterList = new List<FirebaseObject<List<Writer>>>();

		private CategoryAdapter adapter;
		private CategoryItemAdapter _adapter;
		//private WriterAdapter _writerAdapter;
		private SearchAdapter _searchAdapter;

		private AutoCompleteTextView _actv;

		private RecyclerView _listViewCategory;

		private Tab _categoryTab, _authorTab;

		private Button _dailyButton;
		private ProgressDialog _progressDialog;
		private TextView _textView;
        protected AdView mAdView;
        //private IEnumerable<string> _dailyText;
        //private IEnumerable<string> _dailyContent;

        //private Dictionary<string, int> alphaIndex;
        //private string[] sections;
        //private Java.Lang.Object[] sectionsObjects;


        protected override async void OnCreate(Bundle bundle)
		{
			ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
			base.OnCreate(bundle);     

			SetContentView(Resource.Layout.Main_Activity);

            mAdView = FindViewById<AdView>(Resource.Id.adView);
            var adRequest = new AdRequest.Builder().Build();
            mAdView.LoadAd(adRequest);

            GAService.GetGASInstance().Initialize(this);

			_textView = (TextView)FindViewById(Resource.Id.searchBytext);

			_progressDialog = new ProgressDialog(this);
			_progressDialog.Indeterminate = true;
			_progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
			_progressDialog.SetCancelable(false);

			_progressDialog.SetMessage("Loading 75k+ Quotes...");

			var searcText = (AutoCompleteTextView)FindViewById(Resource.Id.category_autocomplete_search);                     
					   
			//var firebase = new FirebaseClient("https://best-quotes-24f45.firebaseio.com/");
			//CategoryList = await firebase
			// .Child("Category")
			// .OnceAsync<List<Category>>();

			//WriterList = await firebase
			// .Child("Writer")
			// .OnceAsync<List<Writer>>();

			//await CreateRealFileAsync();

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

			await GetItems();

			//var itess = CategoryItemList.Where(q => q.CategoryName != null).Select(s => s.CategoryName).ToList();

			//alphaIndex = new Dictionary<string, int>();
			//for (var i = 0; i < itess.Count; i++)
			//{ // loop through items
			//    var key = itess[i];
			//    if (!alphaIndex.ContainsKey(key))
			//        alphaIndex.Add(key, i); // add each 'new' letter to the index
			//}
			//sections = new string[alphaIndex.Keys.Count];
			//alphaIndex.Keys.CopyTo(sections, 0); // convert letters list to string[]
			//                                     // Interface requires a Java.Lang.Object[], so we create one here
			//sectionsObjects = new Java.Lang.Object[sections.Length];
			//for (int i = 0; i < sections.Length; i++)
			//{
			//    sectionsObjects[i] = new Java.Lang.String(sections[i]);
			//}

			_categoryTab = ActionBar.NewTab();
			_authorTab = ActionBar.NewTab();
			_categoryTab.SetText(Resources.GetString(Resource.String.category_tab_text));
			//tab.SetIcon(Resource.Drawable.tab1_icon);
			_categoryTab.TabSelected += (sender, args) =>
			{
				searcText.Text = "";
				_categoryTab.SetText(Resources.GetString(Resource.String.category_tab_text));
				RefreshItemsFromTableAsync();
			};
			ActionBar.AddTab(_categoryTab);


			_authorTab.SetText(Resources.GetString(Resource.String.author_tab_text));
			//tab.SetIcon(Resource.Drawable.tab2_icon);
			_authorTab.TabSelected += (sender, args) =>
			{
				searcText.Text = "";
				_authorTab.SetText(Resources.GetString(Resource.String.author_tab_text));
				RefreshItemsFromTableAsync();
			};
			ActionBar.AddTab(_authorTab);

			_dailyButton = FindViewById<Button>(Resource.Id.dailyButton);

			//foreach (var item in WriterList)
			//{
			//    _dailyText = item.Object.Where(q => q.IsDaily).Select(s => s.WriterName);
			//    _dailyContent = item.Object.Where(q => q.IsDaily).Select(s => s.DailyContent);
			//}
			var text = CategoryItemList.Where(q => q.IsDaily).Select(s => s.WriterName);
			_dailyButton.Text = text.FirstOrDefault();
			//_dailyButton.Text = string.Join(",", _dailyText) + " " + string.Join(",", _dailyContent); 
			_dailyButton.Click += (sender, args) =>
			{
				RefreshDailyItems();
			};
	}

		private void RefreshDailyItems()
		{
			StartActivity(typeof(DailyActivity));
		}

		public async Task GetItems()
		{
			_progressDialog.Show();
			CategoryItemList = await _categoryTable.Where(item => item.Id != null).ToListAsync();
		}

		public void RefreshItemsFromTableAsync()
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
					imageView.SetBackgroundResource(Resource.Drawable.crowded);
					imageView.SetScaleType(ImageView.ScaleType.FitCenter);
					_searchAdapter.OriginalItems = CategoryItemList.Where(q => q.WriterName != null)
						.Select(s => s.WriterName).ToArray();
					//CategoryItemList = await _categoryTable.Where(item => item.WriterName != null).OrderBy(x => x.WriterName).ToListAsync();                 
					//foreach (var item in WriterList)
					//{
					//    _searchAdapter.OriginalItems = item.Object.Select(s => s.WriterName).ToArray();
					//}                  
					_actv.Adapter = _searchAdapter;
					_adapter.Clear();
				}
				else
				{
					_textView.Text = "Search By Category";

					//CategoryItemList = await _categoryTable.Where(item => item.CategoryName != null).OrderBy(x => x.CategoryName).ToListAsync(); 
					//foreach (var item in CategoryList)
					//{
					//    _searchAdapter.OriginalItems = item.Object.Select(s => s.CategoryName).ToArray();
					//}             
					_searchAdapter.OriginalItems = CategoryItemList.Where(q => q.CategoryName != null)
						.Select(s => s.CategoryName).ToArray();
					_actv.Adapter = _searchAdapter;
					_adapter.Clear();
					foreach (var current in CategoryItemList.Where(q => q.CategoryName != null))
						_adapter.Add(current);
                    _progressDialog.Dismiss();
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

        //private static string CreateDatabase(string path)
        //{
        //    try
        //    {
        //        var connection = new SQLiteAsyncConnection(path);
        //        {
        //            connection.CreateTableAsync<CategoryItem>();
        //            return "Database created";
        //        }
        //    }
        //    catch (SQLiteException ex)
        //    {
        //        return ex.Message;
        //    }
        //}
    }
}