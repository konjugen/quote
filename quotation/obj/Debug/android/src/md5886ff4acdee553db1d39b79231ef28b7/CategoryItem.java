package md5886ff4acdee553db1d39b79231ef28b7;


public class CategoryItem
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("quotation.DTO.CategoryItem, quotation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", CategoryItem.class, __md_methods);
	}


	public CategoryItem () throws java.lang.Throwable
	{
		super ();
		if (getClass () == CategoryItem.class)
			mono.android.TypeManager.Activate ("quotation.DTO.CategoryItem, quotation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

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
