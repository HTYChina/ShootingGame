using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {
	
	private static GameControl _instance;
	public static GameControl Instance{
		get{
			if (_instance == null) {
				_instance = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameControl> ();
			}
			return _instance;
		}
	}

	public Transform[] enemySpwans;
	private GameObject enemyPre;
	GameObject enemy;

	private GameControl(){
	}

	private Transform player;
	public Transform Player{
		get{
			if (player == null) {
				player = GameObject.FindGameObjectWithTag ("Player").transform;
			}
			return player;
		}
	}

	private List<GameObject> enemyList = new List<GameObject> ();
	public List<GameObject> EnemyList{
		get{
			return enemyList;
		}
	}

	public bool isSpwan = false;
	public int createCount;
	private ShowInfo showInfo;

	void Start(){
		string pre = PlayerPrefs.GetString ("EnemyPre");
		switch (pre) {
		case "Carbon":
			enemyPre = Resources.Load<GameObject> ("Prefabs/CarbonMain");
			break;
//		case "Boss":
//			enemyPre = Resources.Load<GameObject> ("Prefabs/BossMain");
//			break;
		}
		showInfo = GameObject.Find ("Hp").GetComponent<ShowInfo> ();
		if (enemyPre != null) {
			for (int i = 0; i < createCount; i++) {
                /*
				if (PlayerPrefs.GetInt ("Round") == 3) {
					enemy = Instantiate (enemyPre, enemySpwans [i].position, Quaternion.Euler (0, 180, 0)) as GameObject;
					enemy.transform.localScale = new Vector3 (200.0f, 200.0f, 200.0f);
				} else {
					enemy = Instantiate (enemyPre, enemySpwans [i].position, Quaternion.Euler (0, 180, 0)) as GameObject;
					enemy.transform.localScale = new Vector3 (100.0f, 100.0f, 100.0f);
				}
                */
                if(PlayerPrefs.GetInt("Round") != 3)
                {
                    enemy = Instantiate(enemyPre, enemySpwans[i].position, Quaternion.Euler(0, 180, 0)) as GameObject;
                    enemy.transform.localScale = new Vector3(100.0f, 100.0f, 100.0f);
                }
           
            }
		}
	}

	void CreateEnemyInLevel(int num){
		for (int i = 0; i < num; i++) {
			StartCoroutine (WaitForCreate ());
		}
	}

	void Update(){
		float t = -0.5f;
		if (PlayerPrefs.GetInt ("Round") != 3) {
			if (enemyList.Count == 0 && PlayerPrefs.GetInt ("DeadEnemy") != 0) {
				if (t - Time.deltaTime < 0) {
					ChangeLevel ();
					t = Time.deltaTime - t;
				}
			}
		}
	}

//	void CreateEnemy(){
//		bossAni.SetTrigger ("isAttack");
//		enemy = Instantiate (enemyPre, shotMonPos.position, Quaternion.Euler (0, 180, 0)) as GameObject;
//		enemy.GetComponent<Rigidbody> ().velocity = shotMonPos.up * 100;
//		enemy.transform.localScale = new Vector3 (100.0f, 100.0f, 100.0f);
//	}

	IEnumerator WaitForCreate(){
		int randPos = Random.Range (0, enemySpwans.Length);
		enemy = Instantiate (enemyPre, enemySpwans [randPos].position, Quaternion.Euler (0, 180, 0)) as GameObject;
		enemy.transform.localScale = new Vector3 (100.0f, 100.0f, 100.0f);
		yield return new WaitForSeconds (3.0f);
	}
		
	void ChangeLevel(){
		if (PlayerPrefs.GetInt ("Round") == 1) {
			if (PlayerPrefs.GetInt ("DeadEnemy") == 3) {
                Debug.Log("win");
//				showInfo.ShowWin ();
				PlayerInfo.Instance.AddLevel ();
//				PlayerInfo.Instance.DeadEn = 0;
//				PlayerPrefs.SetInt ("DeadEnemy", 0);
				PlayerPrefs.SetInt ("Level", 2);
				CreateEnemyInLevel (4);
			} else if (PlayerPrefs.GetInt ("DeadEnemy") == 4) {
                //				showInfo.ShowWin ();
                Debug.Log("win");

                PlayerInfo.Instance.AddLevel ();
//				PlayerInfo.Instance.DeadEn = 0;
//				PlayerPrefs.SetInt ("DeadEnemy", 0);
				PlayerPrefs.SetInt ("Level", 3);
				CreateEnemyInLevel (5);
			} else if (PlayerPrefs.GetInt ("DeadEnemy") == 5) {
				PlayerInfo.Instance.AddLevel ();
                Debug.Log("win");

                //				showInfo.ShowWin ();
                StartCoroutine(ToThree ());
			}
//		} else if (PlayerPrefs.GetInt ("Round") == 3) {
//			if (PlayerPrefs.GetInt ("DeadEnemy") == 5) {
//				PlayerInfo.Instance.AddLevel ();
//				PlayerPrefs.SetInt ("Level", 2);
//				CreateEnemyInLevel (7);
////				showInfo.ShowWin ();
//			} else if (PlayerPrefs.GetInt ("DeadEnemy") == 7) {
//				PlayerInfo.Instance.AddLevel ();
//				PlayerPrefs.SetInt ("Level", 3);
//				CreateEnemyInLevel (8);
////				showInfo.ShowWin ();
//			} else if (PlayerPrefs.GetInt ("DeadEnemy") == 8) {
//				PlayerInfo.Instance.AddLevel ();
//				showInfo.ShowWin ();
//				StartCoroutine (Back ());
//			}
		} 
	}

	IEnumerator ToThree(){
		yield return new WaitForSeconds (3.0f);
		Destroy (GameObject.Find ("Canvas(Clone)").gameObject);
		PlayerPrefs.SetInt ("Round", 3);
		PlayerPrefs.SetString ("EnemyPre", "Boss");
		SceneManager.LoadScene ("SceneThree");
	}

//	IEnumerator Back(){
//		yield return new WaitForSeconds (3.0f);
//		Destroy (GameObject.Find ("Canvas(Clone)").gameObject);
//		PlayerPrefs.SetInt ("Round", 0);
//		SceneManager.LoadScene ("Start");
//	}

	#region EnemyList
	public void AddEnemy(GameObject enemy){
		enemyList.Add (enemy);
	}

	public void RemoveEnemy(GameObject enemy){
		enemyList.Remove (enemy);
	}
		
	public void EnemySpwan(){
		int index = Random.Range (0,enemySpwans.Length);
		if (enemyPre != null) {
			enemy = Instantiate (enemyPre, enemySpwans [index].position, Quaternion.Euler (0, 180, 0)) as GameObject;
			enemy.transform.localScale = new Vector3 (5f, 5f, 5f);
		}
	}
	#endregion
}
