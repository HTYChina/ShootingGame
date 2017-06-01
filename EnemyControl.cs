using UnityEngine;
using System.Collections;
[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class EnemyControl : MonoBehaviour {
	private Animator animator;
	private Transform player;
	private Transform myTran;
	private UnityEngine.AI.NavMeshAgent monster;
	private float distance = 0;

	void Start(){
		GameControl.Instance.AddEnemy (this.gameObject);
		animator = GetComponent<Animator> ();
		player = GameControl.Instance.Player;
		myTran = this.transform;
		monster = this.GetComponent<UnityEngine.AI.NavMeshAgent> ();
		monster.SetDestination (player.position);
	}

	void Update(){
		transform.position = Vector3.Lerp (transform.position, player.transform.position, 0.1f * Time.deltaTime);
		distance = Vector3.Distance (myTran.position,player.position);
//		monster.SetDestination (player.position);
		if (distance < 20f && distance != 0) {
			myTran.LookAt (player);
			animator.SetTrigger ("attack");
//			monster.SetDestination (player.position);
		}
	}

//	IEnumerator SetColliderEnable(){
//		yield return new WaitForSeconds (2.0f);
//		this.GetComponent<Collider> ().enabled = true;
//	}

}
