using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemyManager : MonoBehaviour {
	private AudioSource audioSource;
	public AudioClip enemyBumbSE;

	private Rigidbody2D rbody;

	private float enemySpeed = 2.5f;

	private int bulletHitPoint = 15;

	GameObject gameManager;

	// Use this for initialization
	void Start () {
		rbody = gameObject.GetComponent<Rigidbody2D> ();
		gameManager = GameObject.Find ("GameManager");
		audioSource = gameManager.GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		if (gameManager.GetComponent<GameManager> ().gameState == GameManager.GAME_STATE.GAMEOVER) {
			Destroy (this.gameObject);
		}
	}

	void FixedUpdate () {
		rbody.velocity = new Vector2 (0.0f, -1 * enemySpeed);
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "Bullet_Blue") {
			Destroy (this.gameObject);
			Destroy (col.gameObject);
			audioSource.PlayOneShot (enemyBumbSE);

			gameManager.GetComponent<GameManager> ().UpdateScore (bulletHitPoint);
		}

		if (col.tag == "Bullet_Red" || col.tag == "Bullet_Green") {
			Destroy (col.gameObject);
		}

		if (col.tag == "Wall") {
			Destroy (this.gameObject);
		}

		if (col.tag == "PointWall") {
			gameManager.GetComponent<GameManager> ().LifeDelete (1);
		}

		if (col.tag == "Cannon") {
			gameManager.GetComponent<GameManager> ().LifeDelete (3);
		}
	}
}
