﻿using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controlador : MonoBehaviour {
	// En vez de esto hay que cargar los dragonciotosm que ya ganaste del save
	public static bool[] HijosGanados = new bool[]{false, false, false, false, false, false, false, false, false, false, false, false};

	public static int dragoncito1 = -1;//el dragoncito nº1 que va a pelear, definido por un número del 0 al 10 
	public static int dragoncito2 = -1;


	public static string escenaPrevia = "Scene1";

	public GameObject coinci;
	public Image dragNormal, dragAgua, dragFuego, dragAire, dragTierra, dragArena, dragLodo, dragLava, dragMetal, dragHielo, dragElectrico;

	private int lastNumber;

	void Awake(){
		if(SceneManager.GetActiveScene().name == "perfil1" || SceneManager.GetActiveScene().name == "perfil2" || SceneManager.GetActiveScene().name == "perfil3" || SceneManager.GetActiveScene().name == "perfil4" || SceneManager.GetActiveScene().name == "perfil5"){
			if(HijosGanados[9] == true){
				dragNormal.enabled = true;
			}
			if(HijosGanados[1] == true){
				dragArena.enabled = true;
			}
			if(HijosGanados[2] == true){
				dragTierra.enabled = true;
			}
			if(HijosGanados[3] == true){
				dragElectrico.enabled = true;
			}
			if(HijosGanados[4] == true){
				dragMetal.enabled = true;
			}
			if(HijosGanados[5] == true){
				dragAgua.enabled = true;
			}
			if(HijosGanados[6] == true){
				dragAire.enabled = true;
			}
			if(HijosGanados[7] == true){
				dragLodo.enabled = true;
			}
			if(HijosGanados[8] == true){
				dragLava.enabled = true;
			}
			if(HijosGanados[10] == true){
				dragHielo.enabled = true;
			}
			if(HijosGanados[11] == true){
				dragFuego.enabled = true;
			}
		}
	}

	void Start(){
		lastNumber = -1;


	}

	int GetRandom (int min, int max)
	{
		int rand = Random.Range (min, max);
		lastNumber = rand;
		while (rand == lastNumber) { 
			rand = Random.Range (min, max);
		}


		Debug.Log (lastNumber);
		return rand;
	}

	public void CambiarEscena(){

		//int perfilesIndice = Random.Range (1, 4);
		//if (perfilesIndice != perfilesIndice) {

		int perfilesIndice = GetRandom(1,6);

		SceneManager.LoadScene ("perfil" + perfilesIndice.ToString ());
	}

	public void CambiarAtras(string nombre){

		print("Cambiando a la escena " + nombre);
		SceneManager.LoadScene(nombre);

	}

	IEnumerator Esperar() {
		Debug.Log("Coinci!");
		coinci.GetComponent<Image> ().enabled = true;
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
