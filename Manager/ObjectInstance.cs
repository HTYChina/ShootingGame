using UnityEngine;
using System.Collections;

public class ObjectInstance<T> : MonoBehaviour {
	private static T instance;

	public static T Instance{
		get{
			return instance;
		}
	}

	public virtual void SetInstance(T t){
		instance = t;
	}
}
