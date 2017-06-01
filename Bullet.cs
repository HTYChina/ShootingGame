using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider){
        if (PlayerPrefs.GetInt ("Round") == 0) {
			if (collider.CompareTag ("Carbon")) {
				PlayerPrefs.SetInt ("Round", 1);
				PlayerPrefs.SetInt ("Level", 1);
				PlayerPrefs.SetString ("EnemyPre", "Carbon");
				StartCoroutine (ToSceneOne ());
			} else if (collider.CompareTag ("Boss")) {
				PlayerPrefs.SetInt ("Round", 3);
				PlayerPrefs.SetString ("EnemyPre", "Boss");
				StartCoroutine (ToSceneThree ());
			}
		} 
	}

	IEnumerator ToSceneOne(){
		yield return new WaitForSeconds (1.0f);
		SceneManager.LoadScene ("SceneOne");
	}

	IEnumerator ToSceneTwo(){
		yield return new WaitForSeconds (4.0f);
		SceneManager.LoadScene ("SceneTwo");
	}

	IEnumerator ToSceneThree(){
		yield return new WaitForSeconds (1.0f);
		SceneManager.LoadScene ("SceneThree");
	}

}
