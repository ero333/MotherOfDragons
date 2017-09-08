using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class Controlador : MonoBehaviour {

	public void CambiarEscena(){

		int perfilesIndice = Random.Range (1, 4);

		SceneManager.LoadScene ("perfil"+ perfilesIndice.ToString());

	}

	public void CambiarAtras(string nombre){

		print("Cambiando a la escena " + nombre);
		SceneManager.LoadScene(nombre);

	}

	public void Coincidencia(string nombre){

		int randomCoinci = Random.Range (0, 100);
		if (randomCoinci < 30) {
		SceneManager.LoadScene("coinci");

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
