using UnityEngine;
using System.Collections;

public enum InfoType{
	Hp,
	TotalHp,
	Score,
	Level,
	DeadEn,
	All
}

public class PlayerInfo : MonoBehaviour {
	private static PlayerInfo _instance;
	public static PlayerInfo Instance{
		get{
			if (_instance == null) {
				_instance = GameObject.FindGameObjectWithTag ("GameController").GetComponent<PlayerInfo> ();
			}
			return _instance;
		}
	}

	private PlayerInfo(){
	}

	private int hp = 0;
	private int score = 0;
	private int deadEn = 0;
	private int totalHp = 0;
	private int level = 1;

	public int Hp{
		get{
			return hp;
		}
		set{
			hp = value;
		}
	}

	public int DeadEn{
		get{
			return deadEn;
		}
		set{
			deadEn = value;
		}
	}

	public int Level{
		get{
			return level;
		}
		set{
			level = value;
		}
	}

	public int TotalHp{
		get{
			return totalHp;
		}
	}

	public int Score{
		get{
			return score;
		}
		set{
			score = value;
		}
	}

	public delegate void OnInfoChanged(InfoType type);
	public static event OnInfoChanged InfoEvent;

	void Start(){
		InitInfo ();
	}

	void InitInfo(){
		int curRound = PlayerPrefs.GetInt ("Round");
		switch (curRound) {
		case 1:
			InitRoundOne ();
			break;
		case 3:
			Debug.Log ("Three");
			this.hp = 100;
			PlayerPrefs.SetInt ("Hp", 100);
			this.level = 1;
			this.score = 0;
			this.deadEn = 0;
			this.totalHp = this.hp;
			GameObject canvas_One = Instantiate (Resources.Load<GameObject> ("Prefabs/Canvas"), new Vector3 (129f, 104f, 900f), Quaternion.identity) as GameObject;		
			GameControl.Instance.createCount = 1;				
			break;
		case 0:
			Debug.Log ("Restart");
			this.hp = 100;
			this.level = 1;
			this.score = 0;
			this.deadEn = 0;
			this.totalHp = this.hp;
			GameControl.Instance.createCount = 3;
			break;
		}
		InfoEvent (InfoType.All);
	}

//	void InitRoundTwo(){
//		this.deadEn = 0;
//		this.totalHp = this.hp;
//		this.hp = 100;
//		this.level = 1;
//		PlayerPrefs.SetInt ("Hp", 100);
//		this.score = 0;
//		GameObject canvas_One = Instantiate (Resources.Load<GameObject> ("Prefabs/Canvas"), new Vector3 (129f, 104f, 900f), Quaternion.identity) as GameObject;		
//		GameControl.Instance.createCount = 6;	
//	}

	void InitRoundOne(){
        Debug.Log("One");
		this.deadEn = 0;
		this.totalHp = this.hp;
		this.hp = 100;
		this.level = 1;
		PlayerPrefs.SetInt ("Hp", 100);
		this.score = 0;
		GameObject canvas_One = Instantiate (Resources.Load<GameObject> ("Prefabs/Canvas"), new Vector3 (129f, 104f, 900f), Quaternion.identity) as GameObject;		
		GameControl.Instance.createCount = 3;	
	}
		 
	public void AddScore(int value){
		score += value;
		InfoEvent (InfoType.Score);
	}

	public void ReduceHp(int value){
		if (hp > value) {
			hp -= value;
		} else {
			hp = 0;
		}
		InfoEvent (InfoType.Hp);
	}

	public void AddLevel(){
		level = PlayerPrefs.GetInt ("Level");
		InfoEvent (InfoType.Level);
	}


	public void GetDeadEnemy(int value){
		deadEn = PlayerPrefs.GetInt ("DeadEnemy");
		InfoEvent (InfoType.DeadEn);
	}
}
