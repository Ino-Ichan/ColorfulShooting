using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour {
	private AudioSource audioSource;
	public AudioClip enemyBumbSE;
	public AudioClip getCoinSE;

	private Rigidbody2D rbody;

	private float enemySpeed;

	private int bulletHitPoint;

	GameObject gameManager;

	// Use this for initialization
	void Start () {
		rbody = gameObject.GetComponent<Rigidbody2D> ();
		gameManager = GameObject.Find ("GameManager");
		enemySpeed = Random.Range (1.0f, 5.0f);
		bulletHitPoint = (int)enemySpeed * 20;

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
		if (col.tag == "Bullet_Green" || col.tag == "Bullet_Blue" || col.tag == "Bullet_Red") {
			Destroy (this.gameObject);
			Destroy (col.gameObject);

			audioSource.PlayOneShot (enemyBumbSE);
		}

		if (col.tag == "Wall") {
			Destroy (this.gameObject);
		}

		if (col.tag == "Cannon") {
			Destroy (this.gameObject);
			gameManager.GetComponent<GameManager> ().UpdateScore (bulletHitPoint);
			audioSource.PlayOneShot (getCoinSE);
		}
	}
}
