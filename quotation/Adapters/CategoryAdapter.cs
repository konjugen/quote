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
using quotation.DTO;

namespace quotation.Adapters
{
    public class CategoryAdapter : BaseAdapter<CategoryItem>
    {
        Activity activity;
        int layoutResourceId;
        List<CategoryItem> items = new List<CategoryItem>();

        LinearLayout layout;
        TextView text;

        public CategoryAdapter(Activity activity, int layoutResourceId)
        {
            this.activity = activity;
            this.layoutResourceId = layoutResourceId;
        }

        public override View GetView(int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
        {

            var row = convertView;
            var currentItem = this[position];

            if (row == null)
            {
                var inflater = activity.LayoutInflater;
                row = inflater.Inflate(layoutResourceId, null);

                layout = row.FindViewById<LinearLayout>(Resource.Id.categoryLayout);
                text = layout.FindViewById<TextView>(Resource.Id.categoryText);
            }
            else
            {
                layout = row.FindViewById<LinearLayout>(Resource.Id.categoryLayout);
            }

            text.Text = currentItem.CategoryName;
            text.Enabled = true;

            return layout;
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

        public void Remove(CategoryItem item)
        {
            items.Remove(item);
            NotifyDataSetChanged();
        }

        #region implemented abstract members of BaseAdapter

        public override long GetItemId(int position)
        {
            return position;
        }

        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            throw new NotImplementedException();
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override CategoryItem this[int position]
        {
            get { return items[position]; }
        }

        #endregion
    }
}