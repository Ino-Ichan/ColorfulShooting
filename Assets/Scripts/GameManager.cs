using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	private AudioSource audioSource;

	public AudioClip damageSE;
	public AudioClip gameoverSE;
	public AudioClip continueSE;
	public AudioClip bulletSE;

	public GameObject cannon;	//大砲をゲームオブジェクトとして取得
	public Canvas canvasMain;	//CanvasMain

	public Button buttonLeft;
	public Button buttonRight;

	public Button buttonRed;	//赤い弾丸を作るボタン
	public Button buttonBlue;	//青い弾丸を作るボタン
	public Button buttonGreen;	//緑の弾丸を作るボタン

	public GameObject redBulletPrefab;		//赤い弾丸プレハブ
	public GameObject blueBulletPrefab;		//青い弾丸プレハブ
	public GameObject greenBulletPrefab;	//緑の弾丸プレハブ

	private GameObject redBullet;		//赤い弾丸
	private GameObject blueBullet;		//青い弾丸
	private GameObject greenBullet;		//緑の弾丸

	public GameObject redEnemyPrefab;		//赤い敵キャラプレハブ
	public GameObject blueEnemyPrefab;		//青い敵キャラプレハブ
	public GameObject greenEnemyPrefab;		//緑の敵キャラプレハブ
	public GameObject moneyPrefab;			//コインプレハブ
	public GameObject bombPrefab;					//爆弾プレハブ

	private GameObject redEnemy;			//赤い敵キャラ
	private GameObject blueEnemy;			//青い敵キャラ
	private GameObject greenEnemy;			//緑の敵キャラ
	private GameObject money;				//コイン
	private GameObject bomb;				//爆弾

	private float timeRedEnemy;					//赤い敵キャラが出現するまでの時間を計測
	private float timeBlueEnemy;				//青い敵キャラが出現するまでの時間を計測
	private float timeGreenEnemy;				//緑の敵キャラが出現するまでの時間を計測
	private float timeMoney;		  			//コインが出現するまでの時間を計測
	private float timeBomb;						//爆弾が出現するまでの時間を計測

	private float intervalRedEnemy = 3.0f; 		//赤い敵キャラ出現間隔
	private float intervalBlueEnemy = 4.5f; 	//青い敵キャラ出現間隔
	private float intervalGreenEnemy = 2.0f; 	//緑の敵キャラ出現間隔
	private float intervalMoney;				//コインの出現間隔
	private float intervalBomb = 10.0f;					//爆弾の出現感覚

	public GameObject textScore;
	private int nowScore;

	public GameObject lifeStar1;
	public GameObject lifeStar2;
	public GameObject lifeStar3;
	private int lifeRemain = 3;
	public GameObject textGameOver;

	public enum GAME_STATE {
		PLAY,
		GAMEOVER,
	}

	public GAME_STATE gameState = GAME_STATE.PLAY;

	// Use this for initialization
	void Start () {
		timeRedEnemy = 2.0f;
		nowScore = 0;
		textScore.GetComponent<Text>().text = nowScore + " P";
		intervalMoney = Random.Range (5.0f, 10.0f);
		audioSource = this.gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameState == GAME_STATE.PLAY) {
			timeRedEnemy += Time.deltaTime;
			if (timeRedEnemy > intervalRedEnemy) {
				redEnemy = Instantiate (redEnemyPrefab) as GameObject;
				redEnemy.transform.parent = canvasMain.transform;
				redEnemy.transform.localPosition = new Vector3 (Random.Range (-300.0f, 300.0f), 500.0f, 90.0f);
				timeRedEnemy = 0;
			}

			timeBlueEnemy += Time.deltaTime;
			if (timeBlueEnemy > intervalBlueEnemy) {
				blueEnemy = Instantiate (blueEnemyPrefab) as GameObject;
				blueEnemy.transform.parent = canvasMain.transform;
				blueEnemy.transform.localPosition = new Vector3 (Random.Range (-300.0f, 300.0f), 500.0f, 90.0f);
				timeBlueEnemy = 0;
			}

			timeGreenEnemy += Time.deltaTime;
			if (timeGreenEnemy > intervalGreenEnemy) {
				greenEnemy = Instantiate (greenEnemyPrefab) as GameObject;
				greenEnemy.transform.parent = canvasMain.transform;
				greenEnemy.transform.localPosition = new Vector3 (Random.Range (-300.0f, 300.0f), 500.0f, 90.0f);
				timeGreenEnemy = 0;
			}

			timeMoney += Time.deltaTime;
			if (timeMoney> intervalMoney) {
				money = Instantiate (moneyPrefab) as GameObject;
				money.transform.parent = canvasMain.transform;
				money.transform.localPosition = new Vector3 (Random.Range (-300.0f, 300.0f), 500.0f, 90.0f);
				timeMoney = 0;
				intervalMoney = Random.Range (5.0f, 10.0f);
			}

			timeBomb += Time.deltaTime;
			if (timeBomb > intervalBomb) {
				bomb = Instantiate (bombPrefab) as GameObject;
				bomb.transform.parent = canvasMain.transform;
				bomb.transform.localPosition = new Vector3 (Random.Range (-300f, 300f), 500.0f, 90.0f);
				timeBomb = 0;
				intervalBomb = Random.Range (5.0f, 15.0f);
			}
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			MoveCannon (1);
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			MoveCannon (2);
		}
		if (Input.GetKeyDown (KeyCode.Z)) {
			ButtonRed ();
		}

		if (Input.GetKeyDown (KeyCode.X)) {
			ButtonGreen ();
		}

		if (Input.GetKeyDown (KeyCode.C)) {
			ButtonBlue ();
		}
		if (gameState == GAME_STATE.GAMEOVER) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				ButtonRetry ();
			}
		}
	}

	public void ButtonRed () {
		audioSource.PlayOneShot (bulletSE);
		redBullet = Instantiate (redBulletPrefab) as GameObject;
		redBullet.transform.parent = cannon.transform;
		redBullet.transform.localPosition = new Vector3 (0.0f, 0.0f, 0.0f);
		redBullet.transform.parent = canvasMain.transform;
	}

	public void ButtonBlue () {
		audioSource.PlayOneShot (bulletSE);
		blueBullet = Instantiate (blueBulletPrefab) as GameObject;
		blueBullet.transform.parent = cannon.transform;
		blueBullet.transform.localPosition = new Vector3 (0.0f, 0.0f, 0.0f);
		blueBullet.transform.parent = canvasMain.transform;
	}

	public void ButtonGreen () {
		audioSource.PlayOneShot (bulletSE);
		greenBullet = Instantiate (greenBulletPrefab) as GameObject;
		greenBullet.transform.parent = cannon.transform;
		greenBullet.transform.localPosition = new Vector3 (0.0f, 0.0f, 0.0f);
		greenBullet.transform.parent = canvasMain.transform;
	}

	public void ButtonRetry () {
		lifeRemain = 3;
		lifeStar1.SetActive (true);
		lifeStar2.SetActive (true);
		lifeStar3.SetActive (true);
		textGameOver.SetActive (false);
		nowScore = 0;
		textScore.GetComponent<Text>().text = nowScore + " P";

		gameState = GAME_STATE.PLAY;

	}


	public void UpdateScore (int score) {
		if (gameState == GAME_STATE.PLAY) {
			nowScore += score;
			textScore.GetComponent<Text> ().text = nowScore + " P";
		}
	}

	public void LifeDelete (int lifeRate) {
		if (gameState == GAME_STATE.PLAY) {
			if (lifeRate == 1) {//壁に当たった
				switch (lifeRemain) {
				case 3:
					lifeRemain = 2;
					lifeStar3.SetActive (false);
					audioSource.PlayOneShot (damageSE);
					break;
				case 2:
					lifeRemain = 1;
					lifeStar2.SetActive (false);
					audioSource.PlayOneShot (damageSE);
					break;
				case 1:
					lifeRemain = 0;
					lifeStar1.SetActive (false);
					ShowGameOver ();
					break;
				}
			}

			if (lifeRate == 3) {//大砲に当たった
				lifeRemain = 0;
				lifeStar3.SetActive (false);
				lifeStar2.SetActive (false);
				lifeStar1.SetActive (false);
				ShowGameOver ();
			}
		}
	}

	void ShowGameOver () {
		textGameOver.SetActive (true);
		gameState = GAME_STATE.GAMEOVER;
		Destroy (money);
		audioSource.PlayOneShot (gameoverSE);
	}

	public void MoveRightCannon () {
		MoveCannon (1);
	}

	public void MoveLeftCannon () {
		MoveCannon (2);
	}

	void MoveCannon (int key) {//keyが１なら右、左なら２
		if (key == 1) {
			if (cannon.transform.localPosition.x + 60.0f < 300.0f){
				Vector3 newCannonPos = new Vector3 (
					cannon.transform.localPosition.x + 60.0f,
					cannon.transform.localPosition.y,
					cannon.transform.localPosition.z);
				cannon.transform.localPosition = newCannonPos;
			}
		}
		if (key == 2) {
			if (cannon.transform.localPosition.x - 60.0f > -300.0f){
				Vector3 newCannonPos = new Vector3 (cannon.transform.localPosition.x - 60.0f, cannon.transform.localPosition.y, cannon.transform.localPosition.z);
				cannon.transform.localPosition = newCannonPos;
			}
		}

	}
}
