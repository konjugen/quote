package md5b6c11488fbfd5810396a4e2814746856;


public class CategoryAdapter
	extends android.widget.BaseAdapter
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getView:(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;:GetGetView_ILandroid_view_View_Landroid_view_ViewGroup_Handler\n" +
			"n_getItemId:(I)J:GetGetItemId_IHandler\n" +
			"n_getCount:()I:GetGetCountHandler\n" +
			"n_getItem:(I)Ljava/lang/Object;:GetGetItem_IHandler\n" +
			"";
		mono.android.Runtime.register ("quotation.Adapters.CategoryAdapter, quotation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", CategoryAdapter.class, __md_methods);
	}


	public CategoryAdapter () throws java.lang.Throwable
	{
		super ();
		if (getClass () == CategoryAdapter.class)
			mono.android.TypeManager.Activate ("quotation.Adapters.CategoryAdapter, quotation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public CategoryAdapter (android.app.Activity p0, int p1) throws java.lang.Throwable
	{
		super ();
		if (getClass () == CategoryAdapter.class)
			mono.android.TypeManager.Activate ("quotation.Adapters.CategoryAdapter, quotation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.App.Activity, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1 });
	}


	public android.view.View getView (int p0, android.view.View p1, android.view.ViewGroup p2)
	{
		return n_getView (p0, p1, p2);
	}

	private native android.view.View n_getView (int p0, android.view.View p1, android.view.ViewGroup p2);


	public long getItemId (int p0)
	{
		return n_getItemId (p0);
	}

	private native long n_getItemId (int p0);


	public int getCount ()
	{
		return n_getCount ();
	}

	private native int n_getCount ();


	public java.lang.Object getItem (int p0)
	{
		return n_getItem (p0);
	}

	private native java.lang.Object n_getItem (int p0);

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
