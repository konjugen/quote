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

namespace quotation.DTO
{
    public class Writer
    {
        public int PkWriterId { get; set; }
        public string WriterName { get; set; }
        public string DailyContent { get; set; }
        public bool IsDaily { get; set; }
    }
}