using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerAttack : MonoBehaviour {
 
    private GameObject bullet0;
//	private GameObject bullet2;
//	private GameObject bullet1;

//	private Transform myTran;
//	private float radius = 20f;
	private Transform shotPos;
    private float createTime = 4;
	public AudioSource shotAudio;
	void Start(){
		shotPos = transform.Find ("shotPos");
//		shotAudio = GameObject.FindGameObjectWithTag ("Bullet").GetComponent<AudioSource> ();
//		myTran = this.transform;
	}

	void Update(){
		InitBullet ();
	}

	void InitBullet(){ 
		if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger)) {
            createTime -= Time.deltaTime * 50;
            OVRInput.SetControllerVibration(0.5f, 1, OVRInput.Controller.RTouch);

            if (createTime <= 0)
            {
                bullet0 = Instantiate(Resources.Load<GameObject>("Prefabs/bullet"), shotPos.position, Quaternion.identity);
                bullet0.GetComponent<Rigidbody>().velocity = shotPos.forward * 200f;
                shotAudio.Play();
                createTime = 4;
            }
           
			/*
			List<GameObject> enemyList = GetEnemy ();
			if (enemyList.Count > 0) {
				foreach (var item in enemyList) {
					item.GetComponent<EnemyHealth> ().HitEnemy ();
				}
			}
			*/
			StartCoroutine (BulletDestroy ());
		}
	}

	IEnumerator BulletDestroy(){
		yield return new WaitForSeconds (100.0f);
		Destroy (bullet0.gameObject);
	}

	public void Damage(){
		if (PlayerInfo.Instance.Hp <= 0) {
			return;
		}
		float randomDam = Random.Range (0,1f);
		if (randomDam < 0.4f) {
			PlayerInfo.Instance.ReduceHp (20);
			PlayerPrefs.SetInt ("Hp",PlayerInfo.Instance.Hp);
		}

		if (PlayerInfo.Instance.Hp <= 0) {
			foreach (GameObject enemy in GameControl.Instance.EnemyList) {
				enemy.GetComponent<EnemyControl> ().enabled = false;
			}
		}

	}
	/*
	List<GameObject> GetEnemy(){
		List<GameObject> rangeEnemy = new List<GameObject> ();
		List<GameObject> enemyList = new List<GameObject> ();

		Vector3 startPos = myTran.position;
		startPos.y += 0.5f;
		RaycastHit[] enemyColliders = Physics.RaycastAll (startPos, myTran.forward, radius, 1 << LayerMask.NameToLayer ("Enemy"));
		if (enemyColliders.Length > 0) {
			foreach (var item in enemyColliders) {
				enemyList.Add (item.collider.gameObject);
			}
		}

		foreach (var item in enemyList) {
			Vector3 pos = myTran.InverseTransformPoint (item.transform.position);
			if (pos.z > 0) {
				float distance = Vector3.Distance (Vector3.zero, pos);
				if (distance < 15f) {
					rangeEnemy.Add (item);
				}
			}
		}
		return rangeEnemy;
	}
	*/
}
