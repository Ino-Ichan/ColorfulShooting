using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class OpenManager : MonoBehaviour {

	public GameObject Description1;
	public GameObject description2;
	private int description_num = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PushDiscription () {
		if (description_num == 1) {
			Description1.SetActive (false);
			description2.SetActive (true);
			description_num = 2;
			Debug.Log ("puch");
		}
		if (description_num == 2) {
			Description1.SetActive (true);
			description2.SetActive (false);
			description_num = 1;
		}
	}

	public void PushGameScene () {
		SceneManager.LoadScene ("GameScene");
	}
}
