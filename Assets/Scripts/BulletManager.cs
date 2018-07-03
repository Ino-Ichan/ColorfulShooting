using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour {

	private Rigidbody2D rbody;

	private float bulletSpeed = 8.0f;		//弾丸の速度

	// Use this for initialization
	void Start () {
		rbody = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		rbody.velocity = new Vector2 (rbody.velocity.x, bulletSpeed);
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Wall") {
			Destroy (this.gameObject);
		}
	}
}
