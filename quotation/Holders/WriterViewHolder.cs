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
    public class WriterViewHolder : RecyclerView.ViewHolder
    {
        public TextView contentText { get; set; }
        public TextView writerText { get; set; }

        public WriterViewHolder(View itemView) : base(itemView)
        {
            contentText = itemView.FindViewById<TextView>(Resource.Id.contentTextView);
            writerText = itemView.FindViewById<TextView>(Resource.Id.writerTextView);
        }
    }
}