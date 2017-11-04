using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

public class Controlador : MonoBehaviour {
	// En vez de esto hay que cargar los dragonciotosm que ya ganaste del save
	public static bool[] HijosGanados = new bool[]{false, false, false, false, false, false, false, false, false, false, false, false};

	public static int CantidadDeLikes = 0;

	public static float StartTime = 0;

	public static int CantidadDeClicksEntrar = 0;
	public static int CantidadDeClicksSalir = 0;

	public static int dragoncito1 = -1;//el dragoncito nº1 que va a pelear, definido por un número del 0 al 10 
	public static int dragoncito2 = -1;


	public static string escenaPrevia = "Scene1";

	public GameObject coinci;
	public Image dragNormal, dragAgua, dragFuego, dragAire, dragTierra, dragArena, dragLodo, dragLava, dragMetal, dragHielo, dragElectrico;


	private int lastNumber;

	public Text guardarPartida;

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

		StartTime = Time.time;
		Load(Application.streamingAssetsPath + "/Save.txt");
	}

	void Start(){
		lastNumber = -1;


	}

	public static void Load(string filename) {
		string line;
		Debug.Log ("Load");
		StreamReader r = new StreamReader (filename);
		using (r) {
			do {
				line = r.ReadLine();

				if (line != null){
					Debug.Log(line);
					string[] lineData = line.Split(';');
					for (int j = 0; j < 11; j++) {
						Controlador.HijosGanados[j+1]= false;
						Debug.Log((j+1) +" "+ lineData[j]);
						if( lineData[j] == "True")
							Controlador.HijosGanados[j+1]= true;
						
					}

				}
			}
			while (line != null);
			r.Close();
		}
	}

	public static void Save(string filename) {
		Debug.Log ("Save");
				string line="";
		StreamWriter writer = new StreamWriter(filename);
		for (int j = 1; j < 12; j++) {
			line += HijosGanados[j] + ";"; 
		}
		Debug.Log (line);
		writer.WriteLine(line);
		writer.Close();
	}
		
	public static void GanarHijo(int cual) {
		Controlador.HijosGanados[cual] = true;
		Save (Application.streamingAssetsPath + "/Save.txt");
	}

	public static void PederHijo(int cual) {
		Controlador.HijosGanados[cual] = true;
		Save (Application.streamingAssetsPath + "/Save.txt");
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
	
		// Voy a carga el video de presentacion del juego y luego a perfil1
		if (nombre == "portada") { 
			StartTime = Time.time;
			CantidadDeClicksEntrar++;
			Analytics.CustomEvent ("Empezar", new Dictionary<string, object> {
				{ "vez", CantidadDeClicksEntrar },
			});
		}

		// Voy al menu principal
		if (nombre == "juego") { 
			CantidadDeClicksSalir++;
			Analytics.CustomEvent ("SalirTander", new Dictionary<string, object> {
				{ "vez", CantidadDeClicksSalir },
				{ "time", Time.time-StartTime },
			});
		}

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
		CantidadDeLikes++;

		Analytics.CustomEvent("Likear", new Dictionary<string, object>
			{
				{ "Quien",  SceneManager.GetActiveScene().name },
				{ "Cantidad", CantidadDeLikes }
			});
				


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
		Analytics.CustomEvent ("Salir", new Dictionary<string, object> {
			{ "time", Time.time },
		});

		Application.Quit();
	}
}
