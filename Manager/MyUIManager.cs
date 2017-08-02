using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MyUIManager : ObjectInstance<MyUIManager>
{
	public static MyUIManager myinstance;

	void Awake(){
        myinstance = this;
		SetInstance (this);
	}

	public override void SetInstance(MyUIManager t){
		base.SetInstance (t);
	}
}
