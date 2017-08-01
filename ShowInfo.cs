using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowInfo : MonoBehaviour {
	private Text score;
//	private GameObject winPanel;
//	private GameObject losePanel;

//	private Animator pw;
//	private Animator pl;

	private Text levelInWin;
	private Text scoreInWin;

	private Text scoreInLose;
	private Text levelInLose;

	private Slider bossHp;


	void Awake(){


		score = transform.parent.Find ("ScoreBG/score").GetComponent<Text> ();

		levelInWin = transform.parent.Find ("WinPanel/Level/level").GetComponent<Text> ();
		scoreInWin = transform.parent.Find ("WinPanel/ScoreText/score").GetComponent<Text> ();

		scoreInLose = transform.parent.Find ("LosePopup/Text/YourAmountText").GetComponent<Text> ();
		levelInLose = transform.parent.Find ("LosePopup/Text/level").GetComponent<Text> ();

        
//        winPanel = GameObject.Find("WinPanel");
//        losePanel = GameObject.Find("LosePopup");
//        pw = winPanel.GetComponent<Animator>();
//        pl = losePanel.GetComponent<Animator>();
//        winPanel.SetActive(false);
//        losePanel.SetActive(false);
        
        bossHp = transform.parent.Find("BossHp").GetComponent<Slider>();
		if (PlayerPrefs.GetInt ("Round") != 3) {
			bossHp.gameObject.SetActive (false);
		} else {
			bossHp.gameObject.SetActive (true);
			bossHp.value = 500;
		}
        
		PlayerInfo.InfoEvent += Show;
	}

	void Show(InfoType type){
		if (type == InfoType.All || type == InfoType.Hp || type == InfoType.Score || type == InfoType.DeadEn) {
            LoseHeart();
            if (PlayerPrefs.GetInt("Round") != 3)
            {
                bossHp.value = 500;

            }
            else
            {
                bossHp.value = PlayerPrefs.GetInt("bossHp");
            }
        }
	}

	void OnDestroy(){
		PlayerInfo.InfoEvent -= Show;
	}

	void LoseHeart(){
        PlayerInfo info = PlayerInfo.Instance;
        score.text = info.Score.ToString ();
		if (info != null && GameControl.Instance.EnemyList!=null) {
			if (transform.childCount != 0) {
				if (info.Hp == 80) {
					Destroy (transform.GetChild (0).gameObject);
				} else if (info.Hp == 60) {
					Destroy (transform.GetChild (0).gameObject);
				} else if (info.Hp == 40) {
					Destroy (transform.GetChild (0).gameObject);
				} else if (info.Hp == 20) {
					Destroy (transform.GetChild (0).gameObject);
				} else if (info.Hp == 0 || info.Hp < 20) {
					Destroy (transform.GetChild (0).gameObject);
                    Debug.Log("Dead");
                    Destroy(GameObject.Find("Background").gameObject);

                    SceneManager.LoadScene("Start");
                    //					ShowLose ();
                }
            } else {
                Destroy(GameObject.Find("Background").gameObject);

                SceneManager.LoadScene("Start");
                //ShowLose();
            }
        }
	}
    /*
	private void ShowLose(){
		losePanel.SetActive (true);
		scoreInLose.text = PlayerInfo.Instance.Score.ToString ();
		levelInLose.text = PlayerPrefs.GetInt ("Level").ToString ();
		pl.Play ("Open");
		StartCoroutine (HideLosePanel ());
	}
	public void win(){
		winPanel.SetActive (true);
		StartCoroutine (HideWinPanel ());
	}

	public void ShowWin(){
		winPanel.SetActive(true);
		scoreInWin.text = PlayerInfo.Instance.Score.ToString ();
		levelInWin.text = PlayerInfo.Instance.Level.ToString ();
		pw.Play("Open");
		StartCoroutine (HideWinPanel ());
	}

	IEnumerator HideWinPanel(){
		yield return new WaitForSeconds (4.0f);
		pw.Play ("Close");
		winPanel.SetActive (false);
	}

	IEnumerator HideLosePanel(){
		yield return new WaitForSeconds (4.0f);
		pl.Play ("Close");
		losePanel.SetActive (false);
		PlayerPrefs.SetInt ("Round", 0);
		Destroy (GameObject.Find ("Canvas(Clone)").gameObject);
		Destroy (GameObject.Find ("Background").gameObject);
		SceneManager.LoadScene ("Start");
	}
    */
	public void ShowBossSlider(){
        Debug.Log("showslider");
        bossHp.gameObject.SetActive(true);
	}

	public void HideBossSlider(){
        bossHp.gameObject.SetActive(true);
    }
}
