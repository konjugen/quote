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

namespace quotation.Adapters
{
    public class SearchAdapter : BaseAdapter, IFilterable
    {
        string[] items;
        ArrayFilterr filterr;
        public string[] OriginalItems
        {
            get { return this.items; }
            set { this.items = value; }
        }
        Context context;

        public SearchAdapter(Context context)
        {
            this.context = context;
        }
        public override int Count
        {
            get { return items.Length; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return items[position];
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            convertView = View.Inflate(context, Resource.Layout.AutoComplateSearch, null);
            var text = convertView.FindViewById<TextView>(Resource.Id.txt_view);

            text.Text = items[position];
            convertView.Tag = items[position];

            return convertView;
        }

        public Filter Filter
        {
            get
            {
                if (filterr == null)
                {
                    filterr = new ArrayFilterr();
                    filterr.OriginalData = this.OriginalItems;
                    filterr.SAdapter = this;
                }
                return filterr;
            }
        }
    }

    public class ArrayFilterr : Filter

    {
        string[] originalData;
        public string[] OriginalData
        {
            get { return this.originalData; }
            set { this.originalData = value; }
        }

        SearchAdapter adapter;
        public SearchAdapter SAdapter
        {
            get { return adapter; }
            set { this.adapter = value; }
        }

        protected override Filter.FilterResults PerformFiltering(Java.Lang.ICharSequence constraint)
        {

            FilterResults oreturn = new FilterResults();
            if (constraint == null || constraint.Length() == 0)
            {
                oreturn.Values = this.OriginalData;
                oreturn.Count = this.OriginalData.Length;
            }
            else
            {
                string[] actualResults = new string[this.originalData.Length];
                int i = 0;
                foreach (string str in this.originalData)
                {
                    if (str.ToUpperInvariant().Contains(constraint.ToString().ToUpperInvariant()))
                    {
                        actualResults[i] = str;
                        i++;
                    }
                }
                oreturn.Values = actualResults;
                oreturn.Count = actualResults.Length;
            }

            return oreturn;
        }

        protected override void PublishResults(Java.Lang.ICharSequence constraint, Filter.FilterResults results)
        {
            if (results.Count == 0)
                this.SAdapter.NotifyDataSetInvalidated();
            else
            {
                SAdapter.OriginalItems = ((string[])results.Values).Where(q => !string.IsNullOrEmpty(q)).ToArray();
                SAdapter.NotifyDataSetChanged();
            }
        }
    }
}
