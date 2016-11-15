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
using Android.Support.V7.Widget;
using quotation.DTO;
using quotation.Holders;
using Firebase.Database;

namespace quotation.Adapters
{
    public class CategoryItemAdapter : RecyclerView.Adapter
    {
        private MainActivity categoryActivity;
        private readonly RecyclerView _listViewCategory;
        private List<CategoryItem> items = new List<CategoryItem>();
        private readonly List<FirebaseObject<List<Category>>> _categoryItems = new List<FirebaseObject<List<Category>>>();
        private ProgressDialog _progressDialog;

        public override int ItemCount
        {
            get { return items.Count; }
        }

        public CategoryItemAdapter(MainActivity categoryActivity, RecyclerView listViewCategory)
        {
            this.categoryActivity = categoryActivity;
            this._listViewCategory = listViewCategory;
        }

        //public override int ItemCount => _categoryItems.Count == 0 ? 0 : _categoryItems[0].Object.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            CategoryViewHolder ch = holder as CategoryViewHolder;
            if (items[position].CategoryName != null)
            {
                ch.text.Text = items[position].CategoryName;
                ch.ItemView.Tag = items[position].CategoryName;
                ch.text.Text = items[position].PkCategoryId + ". " + items[position].CategoryName;
                ch.ItemView.Tag = items[position].CategoryName;
            }

            //foreach (var item in _categoryItems)
            //{
            //    ch.text.Text = item.Object[position].PkCategoryId + ". " + item.Object[position].CategoryName;
            //    ch.ItemView.Tag = item.Object[position].CategoryName;
            //    holder.ItemView.Id = Convert.ToInt32(item.Object[position].PkCategoryId);
            //}

            else
            {
                //ch.text.Text = items[position].WriterName;
                //ch.ItemView.Tag = items[position].WriterName;
                var text = items.Where(q => q.IsDaily).ToList();
                ch.text.Text = text[0].WriterName;
                ch.ItemView.Tag = text[0].WriterName;

            }
            holder.ItemView.Id = Convert.ToInt32(items[position].PkCategoryId);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Row_List_Category, parent, false);
            CategoryViewHolder ch = new CategoryViewHolder(itemView);
            itemView.Click += ÝtemView_Click;
            return ch;
        }
        private void ÝtemView_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(_listViewCategory.Context, typeof(WriterActivity));
            var id = ((View)sender).Tag;
            intent.PutExtra("selectedCategoryId", id.ToString());
            _listViewCategory.Context.StartActivity(intent);
        }

        public void Add(CategoryItem item)
        {
            items.Add(item);
            NotifyDataSetChanged();
        }
        public void Clear()
        {
            items.Clear();
            NotifyDataSetChanged();
        }

        //internal void Add(FirebaseObject<List<Category>> current)
        //{
        //    _categoryItems.Add(current);
        //    NotifyDataSetChanged();
        //}

        //internal void Clear()
        //{
        //    _categoryItems.Clear();
        //    NotifyDataSetChanged();
        //}
    }
}