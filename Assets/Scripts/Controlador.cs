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
	public static bool[] HijosGanados = new bool[]{true, false, false, false, false, false, false, false, false, false, false, false};

	public static int CantidadDeLikes = 0;
	public static int CantidadDeCoincidencias = 0;
	public static int CantidadGanadas = 0;

	public static int CantidadDeDislikes = 0;

	public static float StartTime = 0;

	public static int CantidadDeClicksEntrar = 0;
	public static int CantidadDeClicksSalir = 0;
	public static int CantidadDeClicksSalirPelea = 0;
	public static int CantidadDeClicksSalirCita = 0;

	public static string[] NombresDragoncitos = new string[] { "", "ARENA", "TIERRA" , "ELECTRICO" , "METAL" , "AGUA" , "AIRE" , "LODO" , "LAVA" , "NORMAL" , "HIELO" , "FUEGO" };

	public static string[] NombresPerfiles = new string[] { "", "ANGEL", "ANGEL" , "MATEO" , "MATEO" ,  "DIEGO" , "DIEGO" ,  "MAXIMILIANO" , "MAXIMILIANO" ,  "ISRAEL" , "ISRAEL" ,  "" ,  "" ,  "PANCHY" , "FERNANDO" , "LEONARDO" , "ALEXIS" , "ANIBAL" , "FABIO" , "FELIPE" , "FAUSTO" , "ROBERTO" , "PABLO" , "LUCIA" };

	public static int cantidadCoincidencias = 0 ;

	public static int dragoncito1 = -1;//el dragoncito nº1 que va a pelear, definido por un número del 0 al 10 
	public static int dragoncito2 = -1;
	public GameObject botones;

	public static string escenaPrevia = "ARIEL";

	public GameObject coinci;

	public static int CantDeValMuyBuena = 0;
	public static int CantDeValBuena = 0;
	public static int CantDeValRegular = 0;
	public static int CantDeValMala = 0;
	public static int CantDeValMuyMala = 0;
	public static bool ganaste = false;


//	[SerializeField]
//	public BotonDragoncitos botonesDragoncitos;


	private int lastNumber;

	public Text guardarPartida;

	void Awake(){

		StartTime = Time.time;


		Load(Application.streamingAssetsPath + "/Save.txt");
		if(!ganaste){
			if (HijosGanados [0] && HijosGanados [1] && HijosGanados [2] && HijosGanados [3] && HijosGanados [4] && HijosGanados [5] && HijosGanados [6] && HijosGanados [7] && HijosGanados [8] && HijosGanados [9] && HijosGanados [10] && HijosGanados [11]) {
				Debug.Log ("GANATE");
				ganaste = true;
				CantidadGanadas++;
				Analytics.CustomEvent("TodosLosDragoncitos", new Dictionary<string, object>
					{
						{ "Veces", CantidadGanadas }

					});
				StartCoroutine (Ganar ());


			}
		}

			

	}
		
	void Start(){
		lastNumber = -1;

	}


	IEnumerator Ganar (){
		yield return new WaitForSeconds(2);
		SceneManager.LoadScene ("Ganaste");

	}

	public static void Load(string filename) {
		string line;
		Debug.Log ("Load");
		StreamReader r = new StreamReader (filename);
		using (r) {
			do {
				line = r.ReadLine();

				if (line != null){
					//Debug.Log(line);
					string[] lineData = line.Split(';');
					for (int j = 0; j < 11; j++) {
						Controlador.HijosGanados[j+1]= false;
						//Debug.Log((j+1) +" "+ lineData[j]);
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
		//Debug.Log (line);
		writer.WriteLine(line);
		writer.Close();
	}
		
	public static void GanarHijo(int cual) {
		Controlador.HijosGanados[cual] = true;

	

		Analytics.CustomEvent("TenerHijo", new Dictionary<string, object>
			{
				{ "Padre",  escenaPrevia },
				{ "Cual",  NombresDragoncitos[cual]}

			});
		
		Save (Application.streamingAssetsPath + "/Save.txt");
	}

	public static void PederHijo(int cual) {
		Controlador.HijosGanados[cual] = false;

	

		Analytics.CustomEvent("PerderHijo", new Dictionary<string, object>
			{
				{ "Enemigo",  escenaPrevia },
				{ "Cual",  NombresDragoncitos[cual]}

			});
		
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

		int perfilesIndice = GetRandom(1,16);
		SceneManager.LoadScene ("perfil" + perfilesIndice.ToString ());

	}

	public void CambiarAtras(string nombre){

		print("Cambiando a la escena " + nombre);
	
		// Voy a carga el video de presentacion del juego y luego a perfil1::: JUGAR
		if (nombre == "portada") { 
			StartTime = Time.time;
			CantidadDeClicksEntrar++;
			Analytics.CustomEvent ("Empezar", new Dictionary<string, object> {
				{ "vez", CantidadDeClicksEntrar },
			});
		}

		// Voy al menu principal::::: SALIR DEL TANDER
		if (nombre == "juego") { 
		CantidadDeClicksSalir++;
			Analytics.CustomEvent ("SalirTander", new Dictionary<string, object> {
				{ "vez", CantidadDeClicksSalir },
				{ "time", Time.time-StartTime },
			});
		}

		if (nombre == "perfil1") { 
			CantidadDeClicksSalirPelea++;
			Analytics.CustomEvent ("SalirPelea", new Dictionary<string, object> {
				{ "vez", CantidadDeClicksSalirPelea },
				{ "time", Time.time-StartTime },
			});
		}

		if (nombre == "perfil2") { 
			CantidadDeClicksSalirCita++;
			Analytics.CustomEvent ("SalirCita", new Dictionary<string, object> {
				{ "Quien",  NombresPerfiles[SceneManager.GetActiveScene ().buildIndex] },
				{ "vez", CantidadDeClicksSalirCita },
				{ "time", Time.time-StartTime },
			});
		}

		SceneManager.LoadScene(nombre);
	}

	IEnumerator Esperar() {

		cantidadCoincidencias++;
		Analytics.CustomEvent("Coincidencia", new Dictionary<string, object>
			{
				{ "Quien",  NombresPerfiles[SceneManager.GetActiveScene ().buildIndex] },
				{ "Cantidad", cantidadCoincidencias }

			});
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
				{ "Quien",  NombresPerfiles[SceneManager.GetActiveScene ().buildIndex] },
				{ "Cantidad", CantidadDeLikes }
			});


				


		int randomCoinci = Random.Range (0, 100);
		if (randomCoinci < 95) {

			StartCoroutine (Esperar());

		}

		else {
			
			CantidadDeDislikes++;

			Analytics.CustomEvent("DesLikear", new Dictionary<string, object>
				{
					{ "Quien",  SceneManager.GetActiveScene().name },
					{ "Cantidad", CantidadDeDislikes }
				});
			
			CambiarEscena();
		}

	}

	public void Valoracion(int v) {

		switch(v) {

			case 1:
				CantDeValMuyBuena++;
				Analytics.CustomEvent ("ValoracionMuyBuena", new Dictionary<string, object> {
					{ "Resultado", CantDeValMuyBuena }
				});
			SceneManager.LoadScene ("juego");
				break;
			
			case 2:
				CantDeValBuena++;
				Analytics.CustomEvent ("ValoracionBuena", new Dictionary<string, object> {
					{ "Resultado", CantDeValBuena }
				});
			SceneManager.LoadScene ("juego");
				break;
			
			case 3:
				CantDeValRegular++;
				Analytics.CustomEvent ("ValoracionRegular", new Dictionary<string, object> {
					{ "Resultado", CantDeValRegular }
				});
			SceneManager.LoadScene ("juego");
				break;

			case 4:
				CantDeValMala++;	
				Analytics.CustomEvent ("ValoracionMala", new Dictionary<string, object> {
					{ "Resultado", CantDeValMala }
				});
			SceneManager.LoadScene ("juego");
				break;
		case 5:
			CantDeValMuyMala++;	
			Analytics.CustomEvent ("ValoracionMuyMala", new Dictionary<string, object> {
				{ "Resultado", CantDeValMuyMala }
			});
			SceneManager.LoadScene ("juego");
			break;

			default:
				;

				break;

		}
	}

	public void Salir (){
		// SALIR DEL MENU PRINCIPAL:::::: CIERRA EL JUEGO
		print("Saliendo del juego");
		Analytics.CustomEvent ("Salir", new Dictionary<string, object> {
			{ "time", Time.time },
		});

		Application.Quit();
	}
}
