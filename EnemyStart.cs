using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStart : MonoBehaviour {
	private Animator anim;
	public AudioSource background;
	public AudioSource hitC;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("Round", 0);
		anim = this.GetComponent<Animator> ();

		background.Play ();
		DontDestroyOnLoad (background);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider){
		if (collider.CompareTag ("Bullet")) {
			anim.SetTrigger ("hit");
			hitC.Play ();

//			Destroy (collider.gameObject);
		}
	}
}
