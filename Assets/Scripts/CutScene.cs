using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutScene : MonoBehaviour {
	IEnumerator Esperar() {
		yield return new WaitForSeconds(11);
		SceneManager.LoadScene ("perfil1");
	}

	// Use this for initialization

	void Start () {
		StartCoroutine (Esperar());
	}


	// Update is called once per frame
	void Update () {
		
	}
}
