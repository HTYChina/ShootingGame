using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour {
	private int bossHp = 500;
	private Animator anim;
	private Renderer render;
	public AudioSource hitB;
	public AudioSource deadB;

 //   private EnemyControl ec;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("bossHp",bossHp);
		anim = GetComponent<Animator> ();
 //       ec = GameObject.FindGameObjectWithTag("Monster").GetComponent<EnemyControl>();
		render = transform.Find ("rigbody/bodygroup/body").GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (bossHp == 0) {
			anim.SetTrigger ("isDead");
			deadB.Play ();
            StartCoroutine(DestoryBoss ());
            
        }
	}

	void OnTriggerEnter(Collider collider){
		if (collider.CompareTag ("Bullet") && PlayerInfo.Instance.Score >= 5) {
			hitB.Play ();
			bossHp -= 5;
			PlayerPrefs.SetInt ("bossHp", bossHp);
			PlayerInfo.Instance.AddScore (5);
			PlayerPrefs.SetInt ("Score", PlayerInfo.Instance.Score);
		}
	}

	IEnumerator DestoryBoss(){
		yield return new WaitForSeconds (3.0f);
		Destroy (this.gameObject);
		Destroy (GameObject.Find ("Background").gameObject);
		Destroy (GameObject.Find ("Canvas(Clone)").gameObject);
		PlayerPrefs.SetInt ("Round", 0);
		SceneManager.LoadScene ("Start");
	}

	IEnumerator Back(){
		yield return new WaitForSeconds (6.0f);
		Destroy (GameObject.Find ("Canvas(Clone)").gameObject);
		PlayerPrefs.SetInt ("Round", 0);
        PlayerPrefs.SetInt("bossHp", 500);
		SceneManager.LoadScene ("Start");
	}
}
