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

	IEnumerator Esperar2() {
		yield return new WaitForSeconds(0);
		SceneManager.LoadScene ("perfil1");
	}

	// Use this for initialization

	void Start () {

		Debug.Log ("esperar");
		if (Controlador.CantidadDeClicksEntrar >= 0){
				StartCoroutine (Esperar());
			}

		if (Controlador.CantidadDeClicksEntrar > 1){
			StartCoroutine (Esperar2());
		}


	}


	// Update is called once per frame
	void Update () {
		
	}
}
