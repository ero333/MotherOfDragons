using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controlador : MonoBehaviour {
	public static bool[] HijosGanados;

	public static int dragoncito1 = 2;//el dragoncito nº1 que va a pelear, definido por un número del 0 al 10 
	public static int dragoncito2 = 4;

	public GameObject coinci;
	public Image dragNormal;
	public Image dragAgua;

	private int lastNumber;

	void Start(){

		HijosGanados = new bool[11];
		lastNumber = -1;

		// En vez de esto hay que cargar los dragonciotosm que ya ganaste del save
		for (int i = 0; i < 11; i++) {
			
			HijosGanados [i] = false;
		}
	}

	int GetRandom (int min, int max)
	{
		int rand = Random.Range (min, max);
		while (rand == lastNumber) { 
			rand = Random.Range (min, max);
		}
		lastNumber = rand;

		Debug.Log (lastNumber);
		return rand;
	}

	public void CambiarEscena(){

		//int perfilesIndice = Random.Range (1, 4);
		//if (perfilesIndice != perfilesIndice) {

		int perfilesIndice = GetRandom(1,5);

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
