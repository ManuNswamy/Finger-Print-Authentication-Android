package md5e8dab7e953c280244f88148124df3701;


public class Normal_Auth_Activity
	extends android.support.v7.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("FingerPrintAuth.Normal_Auth_Activity, FingerPrintAuth, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Normal_Auth_Activity.class, __md_methods);
	}


	public Normal_Auth_Activity ()
	{
		super ();
		if (getClass () == Normal_Auth_Activity.class)
			mono.android.TypeManager.Activate ("FingerPrintAuth.Normal_Auth_Activity, FingerPrintAuth, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
