using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	private int monHp = 30;
	private int deadnum;
//	private Transform mytran;
	private Animator anim;
	private Transform player;
	private EnemyControl ec;
	public AudioSource hitC;

	void Start(){
//		mytran = this.transform;
		anim = GetComponent<Animator> ();
		ec = GetComponent<EnemyControl> ();
		player = GameControl.Instance.Player;
		PlayerPrefs.SetInt ("DeadEnemy", 0);

		if (PlayerPrefs.GetInt ("Round") == 3) {
			monHp = 80;
		} else {
			monHp = 30;
		}
	}

	void OnTriggerEnter(Collider collider){
		if (collider.CompareTag ("Bullet")) {
			hitC.Play ();
			anim.SetTrigger ("hit");
			Destroy (collider.gameObject);
			monHp -= 10;
			PlayerInfo.Instance.AddScore (10);
			PlayerPrefs.SetInt ("Score", PlayerInfo.Instance.Score);
		} else if (collider.CompareTag ("Player")) {
			GetComponent<CapsuleCollider> ().enabled = false;
			GetComponent<SphereCollider> ().enabled = false;

			Destroy (collider.gameObject);

			ec.enabled = false;
			GameControl.Instance.RemoveEnemy (this.gameObject);
			StartCoroutine (DestroyEnemy ());
		}
		if (monHp == 0) {
			anim.SetTrigger ("dead");

			deadnum++;
			int num = PlayerPrefs.GetInt ("DeadEnemy") + 1;
			Debug.Log ("Num"+num);
			PlayerPrefs.SetInt ("DeadEnemy",num);
			Debug.Log (PlayerPrefs.GetInt ("DeadEnemy"));
			PlayerInfo.Instance.GetDeadEnemy (num);

			GetComponent<CapsuleCollider> ().enabled = false;
			GetComponent<SphereCollider> ().enabled = false;
			GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled = false;
			ec.enabled = false;
			GameControl.Instance.RemoveEnemy (this.gameObject);
			StartCoroutine (DestroyEnemy ());
		}
	}

	void Update(){

	}

	/*
	public void HitEnemy(){
		if (monHp <= 0) {
			return;
		}
		monHp -= 20;
		anim.SetTrigger ("beHit");
		if (monHp <= 0) {
			anim.SetTrigger ("isDead");
			ec.enabled = false;
			GetComponent<Collider> ().enabled = false;
			GameControl.Instance.RemoveEnemy (this.gameObject);
			Debug.Log ("Destroy");
		}
	}
*/

	IEnumerator DestroyEnemy(){
		yield return new WaitForSeconds (2.0f);
		Destroy (this.gameObject);
	}

	void Attack(){
		player.GetComponent<PlayerAttack> ().Damage ();
	}
}
