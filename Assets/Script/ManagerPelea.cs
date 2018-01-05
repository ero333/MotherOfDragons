using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.Analytics;

public class ManagerPelea : MonoBehaviour
{
	public int dragoncitoE1 = -1;
	public int dragoncitoE2 = -1;

	public List<GameObject> dragoncitos;
	public List<GameObject> dragonesForros;
	public List<Peleador> peleadores;
	public List<Peleador> dragoncitosEnemigos;

	public static string resultado = "";
	public static float StartTime = 0;

	public static int CantidadDeTurnos = 0;
	public static int CantidadDePeleas = 0;

	public VideoPlayer ganaste;
	public VideoPlayer perdiste;
	public static ManagerPelea singleton;

	public Button prefab;
	public Text textoEstado;
	public Transform panel;

	public HealthBar barraAliado;
	public HealthBar barraAliado2;
	public HealthBar barraAliado3;
	public HealthBar barraEnemigo;
	public HealthBar barraEnemigo2;
	public HealthBar barraEnemigo3;

	public int enemigoActual;

	GameObject marcaDragon;
	GameObject marcaDragona;

	GameObject marcaDragoncitoA1;
	GameObject marcaDragoncitoA2;

	GameObject marcaDragoncitoE1;
	GameObject marcaDragoncitoE2;

	const int DESFASAJE_ENEMIGOS = 11;

	const int DRAGONCITO_ARENA = 1;
	const int DRAGONCITO_TIERRA = 2;
	const int DRAGONCITO_ELECTRICO = 3;
	const int DRAGONCITO_METAL = 4;
	const int DRAGONCITO_AGUA = 5;
	const int DRAGONCITO_AIRE = 6;
	const int DRAGONCITO_LODO = 7;
	const int DRAGONCITO_LAVA = 8;
	const int DRAGONCITO_NORMAL = 9;
	const int DRAGONCITO_HIELO = 10;
	const int DRAGONCITO_FUEGO = 11;


	[SerializeField]
	GameObject indicadorTurno;



	void Awake ()
	{
		if (singleton != null) {

			Destroy (gameObject);
			return;
		}
		singleton = this;



		CantidadDePeleas++;
			Analytics.CustomEvent("PeleaEmepezar", new Dictionary<string, object>
				{
					
					{ "Dragoncito1", Controlador.dragoncito1},
					{ "Dragoncito2", Controlador.dragoncito2},
					{ "Enemigo",  Controlador.escenaPrevia },
					{ "Cantidad",  CantidadDePeleas }

				});
		

	

		
			


	}



	public void ActualizarInterface ()
	{

		textoEstado.text = "";
		foreach (var peleador in peleadores) {

			if (peleador.isActiveAndEnabled) {
				if (peleador.sigueVivo) {

					if (peleador.aliado && peleador.nombre == "Dragona") {
						barraAliado.SetHealth (peleador.vida);
					} 
					if (peleador.aliado && Controlador.dragoncito1 != -1) {
						if(peleador == peleadores[Controlador.dragoncito1]){barraAliado2.SetHealth (peleador.vida);}
					}
					if (peleador.aliado && Controlador.dragoncito2 != -1) {
						if(peleador == peleadores[Controlador.dragoncito2]){barraAliado3.SetHealth (peleador.vida);}
					}
					if (!peleador.aliado && dragoncitoE1 != -1 && peleador.nombre != "Mateo") {
						if(peleador == peleadores[dragoncitoE1]){barraEnemigo2.SetHealth (peleador.vida);}
					}
					if (!peleador.aliado && dragoncitoE2 != -1 && peleador.nombre != "Mateo") {
						if(peleador == peleadores[dragoncitoE2]){barraEnemigo3.SetHealth (peleador.vida);}
					}
					if (!peleador.aliado && peleador.nombre == "Mateo") {
						barraEnemigo.SetHealth (peleador.vida);
					}

				
			
					textoEstado.text += "<color=" + (peleador.aliado ? "blue" : "red") + ">" +
						peleador.nombre + " HP: " + peleador.vida + "/100 MANA: " + peleador.mana + "/100.</color>\n";

				}
			}
		}


		/*foreach (var dragoncitosE in dragoncitosEnemigos) {
			if (dragoncitosE.isActiveAndEnabled) {
				if (dragoncitosE.sigueVivo) {
					
					textoEstado.text += "<color=" + (dragoncitosE.aliado ? "blue" : "red") + ">" +
					dragoncitosE.nombre + " HP: " + dragoncitosE.vida + "/100 MANA: " + dragoncitosE.mana + "/100.</color>\n";
				}
			}
			
		}*/
	}

	void Start ()
	{


		if (Controlador.dragoncito1 < 0) {
			barraAliado2.gameObject.SetActive (false);
		}
		if (Controlador.dragoncito2 < 0) {
			barraAliado3.gameObject.SetActive (false);
		}

		CantidadDeTurnos = 0;

		marcaDragon = GameObject.Find ("Marca Dragon");
		marcaDragona = GameObject.Find ("Marca Dragona");

		marcaDragoncitoA1 = GameObject.Find ("Marca Dragoncito 1 Aliado");
		marcaDragoncitoA2 = GameObject.Find ("Marca Dragoncito 2 Aliado");

		marcaDragoncitoE1 = GameObject.Find ("Marca Dragoncito 1 Enemigo");
		marcaDragoncitoE2 = GameObject.Find ("Marca Dragoncito 2 Enemigo");

		dragoncitoE1 = -1;
		dragoncitoE2 = -1;

		if (Controlador.escenaPrevia == "ARIEL") {
			// Dragon Normal
			GameObject.FindGameObjectWithTag ("israel").SetActive (false);
			GameObject.FindGameObjectWithTag ("maximiliano").SetActive (false);
			GameObject.FindGameObjectWithTag ("diego").SetActive (false);
			GameObject.FindGameObjectWithTag ("ariel").SetActive (false);
			enemigoActual = 23;

			// Dragon Normal
			switch (Random.Range (0, 6)) {
			case 0: 
				// Dragoncito Agua
				dragoncitoE1 = DRAGONCITO_AGUA+DESFASAJE_ENEMIGOS;
				break;
			case 1: 
				// Dragoncito Fuego
				dragoncitoE1 = DRAGONCITO_FUEGO+DESFASAJE_ENEMIGOS;
				break;
			case 2: 
				// Dragoncito Aire
				dragoncitoE1 = DRAGONCITO_AIRE+DESFASAJE_ENEMIGOS;
				break;
			case 3: 
				// Dragoncito Tierra
				dragoncitoE1 = DRAGONCITO_TIERRA+DESFASAJE_ENEMIGOS;
				break;
			}

			if (Random.Range (0, 10) == 0) {
				if (dragoncitoE1 != -1) {
					// Segundo Dragoncito Normal
					dragoncitoE2 = DRAGONCITO_NORMAL+DESFASAJE_ENEMIGOS;
				}
			}

		}
		if (Controlador.escenaPrevia == "MATEO") {
			// Dragon Diego - Tierra
			GameObject.FindGameObjectWithTag ("israel").SetActive (false);
			GameObject.FindGameObjectWithTag ("maximiliano").SetActive (false);
			GameObject.FindGameObjectWithTag ("mateo").SetActive (false);
			GameObject.FindGameObjectWithTag ("ariel").SetActive (false);
			enemigoActual = 27;

			// Dragon TIERRA
			switch (Random.Range (0, 6)) {
			case 0: 
				// Dragoncito Metal
				dragoncitoE1 = DRAGONCITO_METAL+DESFASAJE_ENEMIGOS;
				break;
			case 1: 
				// Dragoncito Arena
				dragoncitoE1 = DRAGONCITO_ARENA+DESFASAJE_ENEMIGOS;
				break;
			case 2: 
				// Dragoncito Lodo
				dragoncitoE1 = DRAGONCITO_LODO+DESFASAJE_ENEMIGOS;
				break;
			}

			if (Random.Range (0, 10) == 0) {
				if (dragoncitoE1 != -1) {
					// Segundo Dragoncito Tierra
					dragoncitoE2 = DRAGONCITO_TIERRA+DESFASAJE_ENEMIGOS;
				}
			}
		}
		if (Controlador.escenaPrevia == "DIEGO") {
			// Dragon Ariel - Fuego
			GameObject.FindGameObjectWithTag ("israel").SetActive (false);
			GameObject.FindGameObjectWithTag ("maximiliano").SetActive (false);
			GameObject.FindGameObjectWithTag ("diego").SetActive (false);
			GameObject.FindGameObjectWithTag ("mateo").SetActive (false);
			enemigoActual = 24;
			// Dragon Fuego
			switch (Random.Range (0, 6)) {
			case 0: 
				// Dragoncito METAL
				dragoncitoE1 = DRAGONCITO_METAL+DESFASAJE_ENEMIGOS;
				break;
			case 1: 
				// Dragoncito LAVA
				dragoncitoE1 = DRAGONCITO_LAVA+DESFASAJE_ENEMIGOS;
				break;
			case 2: 
				// Dragoncito ELECTRICO
				dragoncitoE1 = DRAGONCITO_ELECTRICO+DESFASAJE_ENEMIGOS;
				break;
			}

			if (Random.Range (0, 10) == 0) {
				if (dragoncitoE1 != -1) {
					// Segundo Dragoncito Fuego
					dragoncitoE2 = DRAGONCITO_FUEGO+DESFASAJE_ENEMIGOS;
				}
			}
		}
		if (Controlador.escenaPrevia == "MAXIMILIANO") {
			GameObject.FindGameObjectWithTag ("israel").SetActive (false);
			GameObject.FindGameObjectWithTag ("ariel").SetActive (false);
			GameObject.FindGameObjectWithTag ("diego").SetActive (false);
			GameObject.FindGameObjectWithTag ("mateo").SetActive (false);
			enemigoActual = 26;
			// Dragon Maximiliano - Aire
			switch (Random.Range (0, 6)) {
			case 0: 
				// Dragoncito Hielo
				dragoncitoE1 = DRAGONCITO_HIELO+DESFASAJE_ENEMIGOS;
				break;
			case 1: 
				// Dragoncito ARENA
				dragoncitoE1 = DRAGONCITO_ARENA+DESFASAJE_ENEMIGOS;
				break;
			case 2: 
				// Dragoncito ELECTRICO
				dragoncitoE1 = DRAGONCITO_ELECTRICO+DESFASAJE_ENEMIGOS;
				break;
			}

			if (Random.Range (0, 10) == 0) {
				if (dragoncitoE1 != -1) {
					// Segundo Dragoncito Aire
					dragoncitoE2 = DRAGONCITO_AIRE+DESFASAJE_ENEMIGOS;
				}
			}

		}
		if (Controlador.escenaPrevia == "ISRAEL") {
			GameObject.FindGameObjectWithTag ("ariel").SetActive (false);
			GameObject.FindGameObjectWithTag ("maximiliano").SetActive (false);
			GameObject.FindGameObjectWithTag ("diego").SetActive (false);
			GameObject.FindGameObjectWithTag ("mateo").SetActive (false);
			enemigoActual = 25;
			// Dragon Isrrael AGUA
			switch (Random.Range (0, 6)) {
			case 0: 
				// Dragoncito LODO
				dragoncitoE1 = DRAGONCITO_LODO+DESFASAJE_ENEMIGOS;
				break;
			case 1: 
				// Dragoncito LAVA
				dragoncitoE1 = DRAGONCITO_LAVA+DESFASAJE_ENEMIGOS;
				break;
			case 2: 
				// Dragoncito HIELO
				dragoncitoE1 = DRAGONCITO_HIELO+DESFASAJE_ENEMIGOS;
				break;
			}

			if (Random.Range (0, 10) == 0) {
				if (dragoncitoE1 != -1) {
					// Segundo Dragoncito Agua
					dragoncitoE2 = DRAGONCITO_AGUA+DESFASAJE_ENEMIGOS;
				}
			}
		}

		foreach (var peleador in peleadores) {
	
			if (peleador.nombre != "Dragona" && peleador.nombre != "Mateo") {

				peleador.gameObject.SetActive (false);

			} else {
				
			}
		}

		foreach (var dra in dragoncitosEnemigos) {

			if (dra.nombre != "Dragona" && dra.nombre != "Mateo") {
				dra.gameObject.SetActive (false);

			} else {
				
			}
		}

		if (dragoncitoE1 > -1) {
			dragonesForros [dragoncitoE1-DESFASAJE_ENEMIGOS].SetActive (true);
			GameObject marca = GameObject.Find ("Marca Dragoncito 1 Enemigo");
			dragonesForros [dragoncitoE1-DESFASAJE_ENEMIGOS].transform.position = marca.transform.position;
		}
		if (dragoncitoE2 > -1) {
			dragonesForros [dragoncitoE2-DESFASAJE_ENEMIGOS].SetActive (true);
			GameObject marca = GameObject.Find ("Marca Dragoncito 2 Enemigo");
			dragonesForros [dragoncitoE2-DESFASAJE_ENEMIGOS].transform.position = marca.transform.position;
		}

		if (Controlador.dragoncito1 > -1) {
			dragoncitos [Controlador.dragoncito1].SetActive (true); 
			GameObject marca = GameObject.Find ("Marca Dragoncito 1 Aliado");
			dragoncitos [Controlador.dragoncito1].transform.position = marca.transform.position;
			barraAliado2.enabled = true;
		}
		if (Controlador.dragoncito2 > -1) {
			
			dragoncitos [Controlador.dragoncito2].SetActive (true);
			GameObject marca = GameObject.Find ("Marca Dragoncito 2 Aliado");
			dragoncitos [Controlador.dragoncito2].transform.position = marca.transform.position;
			barraAliado3.enabled = true;
		}
		print ("d1 "+dragoncitoE1);
		print ("d2 "+dragoncitoE2);

		if (dragoncitoE1 < 0) {
			barraEnemigo2.gameObject.SetActive (false);
		}
		if (dragoncitoE2 < 0) {
			barraEnemigo3.gameObject.SetActive (false);
		}
			
		ActualizarInterface ();
		StartCoroutine ("Bucle");
		ganaste.Prepare ();
		perdiste.Prepare ();
	}


	List<Button> poolBotones = new List<Button> ();

	IEnumerator Bucle ()
	{
		bool aliadosvivos = true;
		bool enemigosvivos = true;
		while (aliadosvivos || enemigosvivos) {
			aliadosvivos = false;
			enemigosvivos = false;
			foreach (var peleador in peleadores) {

				IEnumerator c = null;
				if (peleador.isActiveAndEnabled) {

					for (int i = 0; i < poolBotones.Count; i++) {
						poolBotones [i].gameObject.SetActive (false);
					}

					if (peleador.sigueVivo) {
						if (peleador.aliado) {

							indicadorTurno.SetActive (true);
							indicadorTurno.transform.position = marcaDragona.transform.position;

							Accion proxAccion = new Accion ();
							bool sw = false;




							foreach (var accion in peleador.Acciones) {	
								Button b = null;

								for (int i = 0; i < poolBotones.Count; i++) {
									
									if (!poolBotones [i].gameObject.activeInHierarchy) {
										b = poolBotones [i];

									}


								}

								CantidadDeTurnos++;

								

								b = Instantiate (prefab, panel);

								b.transform.position = Vector3.zero;
								b.transform.localScale = Vector3.one;

								poolBotones.Add (b);

								b.gameObject.SetActive (true);

								b.onClick.RemoveAllListeners ();
								b.GetComponentInChildren<Text> ().text = accion.nombre;
								if (peleador.mana < accion.costoMana) {
									b.interactable = false;

								} else {
									b.interactable = true;
									b.onClick.AddListener (() => {





										for (int j = 0; j < poolBotones.Count; j++) {
											poolBotones [j].gameObject.SetActive (false);
										}




										//if (peleadores.IndexOf(peleador).Equals(peleadores[Controlador.dragoncito1].name + " (Peleador)")) {
										if (Controlador.dragoncito1 == 9) {
											// Sterterar sopbre dragocito 1 el triangulo
											indicadorTurno.SetActive (true);
											indicadorTurno.transform.position = marcaDragoncitoA1.transform.position;
										}

										/*if (peleadores.IndexOf(peleador).Equals(peleadores[Controlador.dragoncito2].name + " (Peleador)")) {

											// Sterterar sopbre dragocito 2 el triangulo
											indicadorTurno.SetActive (true);
											indicadorTurno.transform.position = marcaDragoncitoA2.transform.position;
										}
*/

									


										if (accion.nombre == "Curar") {
											c = peleador.EjecutarAccion (accion, peleadores [ElegirAliado ()].transform);
										} else { 
											c = peleador.EjecutarAccion (accion, peleadores [ElegirEnemigo ()].transform);
										} 
										//} else {
										//	c = peleador.EjecutarAccion (accion, peleadores [2].transform);
										//}
									 

									});

								}
							}
						} else {


							indicadorTurno.SetActive (true);
							indicadorTurno.transform.position = marcaDragon.transform.position;
							yield return new WaitForSeconds (1/2);

							int acciontomada = Random.Range (0, peleador.Acciones.Count);

							if (peleador.Acciones [acciontomada].nombre == "Curar") {
								c = peleador.EjecutarAccion (peleador.Acciones [acciontomada], peleadores [ElegirEnemigo ()].transform);								
							} else {
								yield return new WaitForSeconds (1/2);
								c = peleador.EjecutarAccion (peleador.Acciones [acciontomada], peleadores [ElegirAliado ()].transform);	

							}
							//c = peleador.EjecutarAccion (peleador.Acciones [Random.Range (0, peleador.Acciones.Count)],	peleadores [Random.Range (0, (peleadores.Count - 2))].transform);	
							
							//}
							//Random.Range (0, peleadores.Count)


						}

						while (c == null) {
							yield return null;
						}


						yield return StartCoroutine (c);
						yield return new WaitForSeconds (1);
					}
				
			
					if (peleador.aliado) { 

						if (peleador.sigueVivo) {
							aliadosvivos = true;
						}

					} else { 
						if (peleador.sigueVivo) { 
							enemigosvivos = true;
						}
					}
				}
		
			}
				


			
			if (!peleadores[0].sigueVivo ) { 
				Debug.Log ("PERDISTE");
				if (Controlador.dragoncito1 > -1) {
					if (Controlador.escenaPrevia == "ARIEL") {
						Controlador.PederHijo(Controlador.dragoncito1);
					}
					if (Controlador.escenaPrevia == "MATEO") {
						Controlador.PederHijo(Controlador.dragoncito1);
					}
					if (Controlador.escenaPrevia == "DIEGO") {
						Controlador.PederHijo(Controlador.dragoncito1);
					}
					if (Controlador.escenaPrevia == "MAXIMILIANO") {
						Controlador.PederHijo(Controlador.dragoncito1);
					}
					if (Controlador.escenaPrevia == "ISRAEL") {
						Controlador.PederHijo(Controlador.dragoncito1);
					}
				}

				resultado = "Perdiste";

				int cantidDragoncitos = 0;
				int cantidDragoncitosE = 0;
				string EventDragoncito1 = "Nada";
				string EventDragoncito2 = "Nada";
				string EventDragoncitoE1 = "Nada";
				string EventDragoncitoE2 = "Nada";

				if (Controlador.dragoncito1 > 0) {
					EventDragoncito1 = Controlador.NombresDragoncitos[Controlador.dragoncito1];
					cantidDragoncitos = 1;
				}
				if (Controlador.dragoncito2 > 0){
					EventDragoncito2 = Controlador.NombresDragoncitos[Controlador.dragoncito2];
					cantidDragoncitos = 2;
				}

				if (dragoncitoE1 > 0){
					EventDragoncitoE1 = Controlador.NombresDragoncitos[dragoncitoE1-DESFASAJE_ENEMIGOS];
					cantidDragoncitosE= 1;
				}

				if (dragoncitoE2 > 0){
					EventDragoncitoE2 = Controlador.NombresDragoncitos[dragoncitoE2-DESFASAJE_ENEMIGOS];
					cantidDragoncitosE = 2;
				}

				Analytics.CustomEvent("PeleaFin", new Dictionary<string, object>
					{
						{ "Quien",  Controlador.escenaPrevia },
						{ "Resultado", resultado},
						{ "Dragoncito1", EventDragoncito1},
						{ "Dragoncito2", EventDragoncito2},
						{ "DragoncitoE1", EventDragoncitoE1},
						{ "DragoncitoE2", EventDragoncitoE2},
						{ "dragoncitosAliados", cantidDragoncitos},
						{ "dragoncitosEnemigos", cantidDragoncitosE},
						{ "Turnos", CantidadDeTurnos},
						{ "Time", Time.time-StartTime }

					});
						

				perdiste.Play ();
				yield return new WaitForSeconds (7);
				Controlador.escenaPrevia = "no";
				SceneManager.LoadScene ("perfil1");
			} 

			if (!peleadores[enemigoActual].sigueVivo) { 
				Debug.Log ("GANASTE");

				if(dragoncitoE1>-1){
					if(Controlador.escenaPrevia == "ARIEL"){
						Controlador.GanarHijo(dragoncitoE1-DESFASAJE_ENEMIGOS);
					}
					if(Controlador.escenaPrevia == "MATEO"){
						Controlador.GanarHijo(dragoncitoE1-DESFASAJE_ENEMIGOS);
					}
					if(Controlador.escenaPrevia == "DIEGO"){
						Controlador.GanarHijo(dragoncitoE1-DESFASAJE_ENEMIGOS);
					}
					if(Controlador.escenaPrevia == "MAXIMILIANO"){
						Controlador.GanarHijo(dragoncitoE1-DESFASAJE_ENEMIGOS);
					}
					if(Controlador.escenaPrevia == "ISRAEL"){
						Controlador.GanarHijo(dragoncitoE1-DESFASAJE_ENEMIGOS);
					}
				}
				resultado = "Ganaste";


				int cantidDragoncitos = 0;
				int cantidDragoncitosE = 0;
				string EventDragoncito1 = "Nada";
				string EventDragoncito2 = "Nada";
				string EventDragoncitoE1 = "Nada";
				string EventDragoncitoE2 = "Nada";

				if (Controlador.dragoncito1 > 0) {
					EventDragoncito1 = Controlador.NombresDragoncitos[Controlador.dragoncito1];
					cantidDragoncitos = 1;
				}
				if (Controlador.dragoncito2 > 0) {
					EventDragoncito2 = Controlador.NombresDragoncitos[Controlador.dragoncito2];
					cantidDragoncitos = 2;
				}

				if (dragoncitoE1 > 0) {
					EventDragoncitoE1 = Controlador.NombresDragoncitos[dragoncitoE1];
					cantidDragoncitosE = 1;
				}

				if (dragoncitoE2 > 0) {
					EventDragoncitoE2 = Controlador.NombresDragoncitos[dragoncitoE2];
					cantidDragoncitosE = 2;
				}

				Analytics.CustomEvent("PeleaFin", new Dictionary<string, object>
					{
						{ "Quien",  Controlador.escenaPrevia },
						{ "Resultado", resultado},
						{ "Dragoncito1", EventDragoncito1},
						{ "Dragoncito2", EventDragoncito2},
						{ "DragoncitoE1", EventDragoncitoE1},
						{ "DragoncitoE2", EventDragoncitoE2},
						{ "dragoncitosAliados", cantidDragoncitos},
						{ "dragoncitosEnemigos", cantidDragoncitosE},
						{ "Turnos", CantidadDeTurnos},
						{ "Time", Time.time-StartTime }

					});
				
				ganaste.Play ();
				yield return new WaitForSeconds (5);
				Controlador.escenaPrevia = "no";
				SceneManager.LoadScene ("perfil1");
			}
		}

	}
		
	int ElegirEnemigo ()
	{
		Debug.Log ("Enemigo:" + enemigoActual + " Dagoncito1: " + dragoncitoE1 + " Dagoncito2: " + dragoncitoE2);
		int cantenemigos = 1;
		if (dragoncitoE1 > -1) {
			cantenemigos++;
		}
		if (dragoncitoE2 > -1) {
			cantenemigos++;
		}

		int cual = Random.Range (0, cantenemigos);
		if (cual != 0) { 
			if (cual == 1) {
				// Es el dragoncito 1			
				if (peleadores [dragoncitoE1].sigueVivo) {
					cual = dragoncitoE1;
				} else {
					if (cantenemigos < 3)
						cual = enemigoActual;
					else {
						if (peleadores [dragoncitoE1].sigueVivo) {
							cual = dragoncitoE2;
						} else {
							cual = enemigoActual;
						}
					}
				} 
			} else {
				// Entonmces el dragoncito 2	
				if (peleadores [dragoncitoE2].sigueVivo) {
					cual = dragoncitoE2;
				} else {
					if (peleadores [dragoncitoE1].sigueVivo) {	
						cual = Random.Range (0, 1);
						if (cual == 1) {
							cual = dragoncitoE1;
						}
						if (cual == 0) {
							cual = enemigoActual;
						}
					} else {
						cual = enemigoActual;
					}
				}
			}
		}
		if (cual == 0) {
			cual = enemigoActual;
		}
		Debug.Log ("elegir enemigo " + cual);
		return cual;
	}

	int ElegirAliado ()
	{
		Debug.Log ("Aliado: 0  Dagoncito1: " + Controlador.dragoncito1 + " Dagoncito2: " + Controlador.dragoncito2);
		int cantaliados = 1;
		if (Controlador.dragoncito1 > -1) {
			cantaliados++;
		}
		if (Controlador.dragoncito2 > -1) {
			cantaliados++;
		}

		int cual = Random.Range (0, cantaliados);
		if (cual != 0) { 
			if (cual == 1) {
				// Es el dragoncito 1			
				if (peleadores [Controlador.dragoncito1].sigueVivo) {
					cual = Controlador.dragoncito1;
				} else {
					if (cantaliados < 3)
						cual = 0;
					else {
						if (peleadores [Controlador.dragoncito2].sigueVivo) {
							cual = Controlador.dragoncito2;
						} else {
							cual = 0;
						}
					}
				} 
			} else {
				// Entonmces el dragoncito 2	
				if (peleadores [Controlador.dragoncito2].sigueVivo) {
					cual = Controlador.dragoncito2;
				} else {
					if (peleadores [Controlador.dragoncito1].sigueVivo) {	
						cual = Random.Range (0, 1);
						if (cual == 1) {
							cual = Controlador.dragoncito1;
						}
					} else {
						cual = 0;
					}
				}
			}
		}

		Debug.Log ("elegir aliado " + cual);
		return cual;
	}
}