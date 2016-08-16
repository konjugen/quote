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

namespace quotation.Holders
{
    public class CategoryViewHolder : RecyclerView.ViewHolder
    {
        public TextView text { get; set; }
        public LinearLayout layout { get; set; }

        public CategoryViewHolder(View itemView) : base(itemView)
        {
            layout = itemView.FindViewById<LinearLayout>(Resource.Id.categoryLayout);
            text = itemView.FindViewById<TextView>(Resource.Id.categoryText);
        }
    }
}