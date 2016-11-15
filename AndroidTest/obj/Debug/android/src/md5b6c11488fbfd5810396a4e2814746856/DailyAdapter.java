package md5b6c11488fbfd5810396a4e2814746856;


public class DailyAdapter
	extends android.support.v7.widget.RecyclerView.Adapter
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getItemCount:()I:GetGetItemCountHandler\n" +
			"n_onBindViewHolder:(Landroid/support/v7/widget/RecyclerView$ViewHolder;I)V:GetOnBindViewHolder_Landroid_support_v7_widget_RecyclerView_ViewHolder_IHandler\n" +
			"n_onCreateViewHolder:(Landroid/view/ViewGroup;I)Landroid/support/v7/widget/RecyclerView$ViewHolder;:GetOnCreateViewHolder_Landroid_view_ViewGroup_IHandler\n" +
			"";
		mono.android.Runtime.register ("quotation.Adapters.DailyAdapter, quotation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", DailyAdapter.class, __md_methods);
	}


	public DailyAdapter () throws java.lang.Throwable
	{
		super ();
		if (getClass () == DailyAdapter.class)
			mono.android.TypeManager.Activate ("quotation.Adapters.DailyAdapter, quotation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public DailyAdapter (md5905b39261ce1e271f13ef34e4d812a7c.DailyActivity p0, android.support.v7.widget.RecyclerView p1) throws java.lang.Throwable
	{
		super ();
		if (getClass () == DailyAdapter.class)
			mono.android.TypeManager.Activate ("quotation.Adapters.DailyAdapter, quotation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "quotation.DailyActivity, quotation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null:Android.Support.V7.Widget.RecyclerView, Xamarin.Android.Support.v7.RecyclerView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0, p1 });
	}


	public int getItemCount ()
	{
		return n_getItemCount ();
	}

	private native int n_getItemCount ();


	public void onBindViewHolder (android.support.v7.widget.RecyclerView.ViewHolder p0, int p1)
	{
		n_onBindViewHolder (p0, p1);
	}

	private native void n_onBindViewHolder (android.support.v7.widget.RecyclerView.ViewHolder p0, int p1);


	public android.support.v7.widget.RecyclerView.ViewHolder onCreateViewHolder (android.view.ViewGroup p0, int p1)
	{
		return n_onCreateViewHolder (p0, p1);
	}

	private native android.support.v7.widget.RecyclerView.ViewHolder n_onCreateViewHolder (android.view.ViewGroup p0, int p1);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
