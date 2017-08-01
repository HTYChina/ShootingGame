using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControl : MonoBehaviour {
	private Transform bossTran;
	private Transform shotMonPos;
	private Animator bossAni;

	private GameObject enPre;
	private GameObject monster;
//	private Transform player;

	private bool isGround;
	private float createTime = 4;
//	private ShowInfo showInfo;

	public AudioSource bossAttack;

	/*
	private static List<Vector3> EquationAngle(int currentCount, int currentAngle){
		List<Vector3> PosInfoCurrent = new List<Vector3> ();
		float currentTan = Mathf.Tan (Mathf.PI / 180 * currentAngle);//斜率
		float currentOffset = 1.0F / (currentCount - 1);//偏移
		if (currentAngle > 0) {
			for (int i = 0; i < currentCount; i++) {
				PosInfoCurrent.Add (new Vector3 (i * currentOffset, 0, currentTan * i * currentOffset));
			}
		} else if (currentAngle < 0) {
			for (int i = 0; i < currentCount; i++) {
				PosInfoCurrent.Add (new Vector3 (i * currentOffset, 0, currentTan * (i * currentOffset - 1)));
			}
		}
		return PosInfoCurrent;
	}
*/
	private static BossControl _instance;
	public static BossControl Instance{
		get{
			if (_instance == null) {
				_instance = GameObject.FindGameObjectWithTag ("Boss").GetComponent<BossControl> ();
			}
			return _instance;
		}
	}

	// Use this for initialization
	void Start () {
		isGround = false;
		bossAni = this.GetComponent<Animator> ();
		bossTran = this.transform;
		enPre = Resources.Load<GameObject>("Prefabs/CarbonMain");
		shotMonPos = transform.Find ("shotMonPos");
//		showInfo = GameObject.Find ("Hp").GetComponent<ShowInfo> ();
	}

//	void OnCollisionEnter(Collision collision){
//		Debug.Log (enemy);
//		enemy = Instantiate (enPre, shotMonPos.position, Quaternion.Euler (0, 180, 0)) as GameObject;
//		enemy.transform.localScale = new Vector3 (100.0f, 100.0f, 100.0f);
//	}


	void OnCollisionStay(Collision collision){
		if (collision.collider.CompareTag ("Ground")) {
			isGround = true;
			Debug.Log ("isGround");
		}
	}

	void OnCollisionExit(Collision collision){
		if (collision.collider.CompareTag ("Ground")) {
			isGround = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
//		if (GameControl.Instance.EnemyList.Count == 0 && PlayerPrefs.GetInt ("DeadEnemy") != 0) {

//		}
		if (isGround) {
			createTime -= Time.deltaTime ;
			if (createTime <= 0 && isGround) {
				bossAni.SetTrigger ("isAttack");
				bossAttack.Play ();
				int createNum = Random.Range (1,4);
				for (int i = 0; i < createNum; i++){
					monster = Instantiate (enPre, shotMonPos.position, Quaternion.Euler (0, 180, 0)) as GameObject;
					monster.transform.localScale = new Vector3 (100.0f, 100.0f, 100.0f);
				}
				createTime = 4;
			}
		}

//		createTime -= Time.deltaTime;
//		if (createTime <= 0 && isGround) {
//			CreateEnemy ();
//			createTime = 4;
//		}
	}
		
	IEnumerator CreateEnemy(){
		bossAni.SetTrigger ("isAttack");
//		List<Vector3> test = EquationAngle (3, 20);
//		List<Vector3> cur = new List<Vector3> ();
//		foreach(Vector3 pos in test){
//		for(int i=0;i<3;i++){
//			cur.Add (pos);
		monster = Instantiate (enPre, shotMonPos.position, Quaternion.Euler (0, 180, 0)) as GameObject;
//			enemy = Instantiate (enPre, new Vector3(pos.x,pos.y + 88f,pos.z+300f), Quaternion.Euler (0, 180, 0)) as GameObject;
		monster.transform.localScale = new Vector3 (100.0f, 100.0f, 100.0f);
//		}
		yield return new WaitForSeconds(5f);

	}
}



