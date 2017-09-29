using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controlador : MonoBehaviour {
	public GameObject coinci;

	private int lastNumber;

	int GetRandom (int min, int max)
	{
		int rand = Random.Range (min, max);
		while (rand == lastNumber)
			rand = Random.Range (min, max);
		lastNumber = rand;
		return rand;
	}

	public void CambiarEscena(){

		//int perfilesIndice = Random.Range (1, 4);
		//if (perfilesIndice != perfilesIndice) {

		int perfilesIndice = GetRandom(1,4);
		SceneManager.LoadScene ("perfil" + perfilesIndice.ToString ());
	}

	public void CambiarAtras(string nombre){

		print("Cambiando a la escena " + nombre);
		SceneManager.LoadScene(nombre);

	}

	IEnumerator Esperar() {
		Debug.Log("Coinci!");
		coinci.GetComponent<Image> ().enabled = true;
		//gameObject.GetComponentsInChildren<Image> ().enabled = true;
		//transform.Find("COINCIDENCIA").GetComponent<Image> ().enabled = true;
		yield return new WaitForSeconds(3);
		int sceneNum = SceneManager.GetActiveScene ().buildIndex;
		SceneManager.LoadScene (sceneNum+1);
		Debug.Log("After Waiting 2 Seconds");
	}


	public void Coincidencia(string nombre){

		int randomCoinci = Random.Range (0, 100);
		if (randomCoinci < 30) {

			StartCoroutine (Esperar());

		}

		else {
			CambiarEscena();
		}

	}

	public void Salir (){
		print("Saliendo del juego");
		Application.Quit();
	}
}
