using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutScene : MonoBehaviour {

	public int tiempo = 30;
	public IEnumerator Esperar() {
		yield return new WaitForSeconds(tiempo);
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
