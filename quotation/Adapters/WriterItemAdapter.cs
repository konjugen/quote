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


namespace quotation.Adapters
{
    public class WriterItemAdapter : RecyclerView.Adapter
    {
        private WriterActivity writerActivity;
        private RecyclerView listViewWriter;
        private List<WriterItem> items = new List<WriterItem>();
        private ProgressDialog _progressDialog;
        private ImageView shareButtonImageView;

        public WriterItemAdapter(WriterActivity writerActivity, RecyclerView listViewWriter)
        {
            this.writerActivity = writerActivity;
            this.listViewWriter = listViewWriter;
        }

        public override int ItemCount
        {
            get { return items.Count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var ch = holder as WriterViewHolder;
            ch.contentText.Text = items[position].Content;
            ch.writerText.Text = items[position].WriterName;
            holder.ItemView.Id = Convert.ToInt32(items[position].FkCategoryId);
            holder.ItemView.Tag = items[position].Id;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            _progressDialog = new ProgressDialog(Application.Context);
            _progressDialog.Indeterminate = true;
            _progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
            _progressDialog.SetCancelable(false);
            _progressDialog.SetMessage("Loading...");

            var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ContentCardView, parent, false);
            var ch = new WriterViewHolder(itemView);

            itemView.Click += ÝtemView_Click;
            return ch;
        }
        private void ÝtemView_Click(object sender, EventArgs e)
        {
            var intent = new Intent(Intent.ActionSend);
            var id = ((View)sender).Tag.ToString();
            var itemText = items.FirstOrDefault(x => x.Id == id);
            intent.SetType("text/plain");
            if (itemText != null)
                intent.PutExtra(Intent.ExtraText, "“" + itemText.Content + "”" + " -" + itemText.WriterName);

            intent.SetFlags(ActivityFlags.ClearTop);
            intent.SetFlags(ActivityFlags.NewTask);
            var chooserIntent = Intent.CreateChooser(intent, "Paylaþ");
            chooserIntent.SetFlags(ActivityFlags.ClearTop);
            chooserIntent.SetFlags(ActivityFlags.NewTask);
            Application.Context.StartActivity(chooserIntent);
        }
        public void Add(WriterItem item)
        {
            items.Add(item);
            NotifyDataSetChanged();
        }
        public void Clear()
        {
            items.Clear();
            NotifyDataSetChanged();
        }
    }
}