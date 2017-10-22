using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ManagerPelea : MonoBehaviour
{

	public List<GameObject> dragoncitos;
	public List<Peleador> peleadores;
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

	public int enemigoActual;

	GameObject marcaDragon;
	GameObject marcaDragona;

	GameObject marcaDragoncitoA1;
	GameObject marcaDragoncitoA2;


	[SerializeField]
	GameObject indicadorTurno;
	/*
	public void SetHealth(int vida){
		
		GameObject Abarra = GameObject.FindGameObjectWithTag("barra");
		var AbarraRectTransform = Abarra.transform as RectTransform;
		AbarraRectTransform.sizeDelta = new Vector2 ((vida*2), 25);
	}
	*/



	void Awake ()
	{
		if (singleton != null) {

			Destroy (gameObject);
			return;
		}
		singleton = this;

	}



	public void ActualizarInterface ()
	{

		textoEstado.text = "";
		foreach (var peleador in peleadores) {

			if (peleador.isActiveAndEnabled ) {
				if (peleador.sigueVivo) {

					if (peleador.aliado && peleador.nombre == "Dragona") {
						barraAliado.SetHealth (peleador.vida);
					} else {
						if (peleador.aliado && Controlador.dragoncito1 != -1) {
							barraAliado2.SetHealth (peleador.vida);
						} else {
							if (peleador.aliado && Controlador.dragoncito2 != -1) {
								barraAliado3.SetHealth (peleador.vida);
							}
							barraEnemigo.SetHealth (peleador.vida);
						}
					}

					textoEstado.text += "<color=" + (peleador.aliado ? "blue" : "red") + ">" +
					peleador.nombre + " HP: " + peleador.vida + "/100 MANA: " + peleador.mana + "/100.</color>\n";
				}

			}
		}
	}

	void Start ()
	{

		enemigoActual = 12; 
		marcaDragon = GameObject.Find("Marca Dragon");
		marcaDragona = GameObject.Find("Marca Dragona");

		marcaDragoncitoA1 = GameObject.Find("Marca Dragoncito 1 Aliado");
		marcaDragoncitoA2 = GameObject.Find("Marca Dragoncito 2 Aliado");

		if(Controlador.escenaPrevia == "Scene1" || Controlador.escenaPrevia == "Scene2" || Controlador.escenaPrevia == "Scene3" || Controlador.escenaPrevia == "Scene4" || Controlador.escenaPrevia == "Scene5"){
			GameObject.FindGameObjectWithTag ("israel").SetActive (false);
			GameObject.FindGameObjectWithTag ("maximiliano").SetActive (false);
			GameObject.FindGameObjectWithTag ("diego").SetActive (false);
			GameObject.FindGameObjectWithTag ("ariel").SetActive (false);
			enemigoActual = 12; 
		}
		/*if(Controlador.escenaPrevia == "Scene2"){
			GameObject.FindGameObjectWithTag ("israel").SetActive (false);
			GameObject.FindGameObjectWithTag ("maximiliano").SetActive (false);
			GameObject.FindGameObjectWithTag ("mateo").SetActive (false);
			GameObject.FindGameObjectWithTag ("ariel").SetActive (false);
			enemigoActual = 12; 
		}
		if(Controlador.escenaPrevia == "Scene3"){
			GameObject.FindGameObjectWithTag ("israel").SetActive (false);
			GameObject.FindGameObjectWithTag ("maximiliano").SetActive (false);
			GameObject.FindGameObjectWithTag ("diego").SetActive (false);
			GameObject.FindGameObjectWithTag ("mateo").SetActive (false);
			enemigoActual = 12; 
		}
		if(Controlador.escenaPrevia == "Scene4"){
			GameObject.FindGameObjectWithTag ("israel").SetActive (false);
			GameObject.FindGameObjectWithTag ("ariel").SetActive (false);
			GameObject.FindGameObjectWithTag ("diego").SetActive (false);
			GameObject.FindGameObjectWithTag ("mateo").SetActive (false);
			enemigoActual = 12; 
		}
		if(Controlador.escenaPrevia == "Scene3"){
			GameObject.FindGameObjectWithTag ("ariel").SetActive (false);
			GameObject.FindGameObjectWithTag ("maximiliano").SetActive (false);
			GameObject.FindGameObjectWithTag ("diego").SetActive (false);
			GameObject.FindGameObjectWithTag ("mateo").SetActive (false);
			enemigoActual = 12; 
		}*/

		foreach (var peleador in peleadores) {
	
			if (peleador.nombre != "Dragona" && peleador.nombre != "Mateo") {

				peleador.gameObject.SetActive (false);
				Debug.Log ("Desactivando: "+peleador.nombre);
			} else {
				Debug.Log ("No desactivado: "+peleador.nombre);
			}
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
				if (peleador.isActiveAndEnabled ) {
				
					Debug.Log(peleador.nombre);
					Debug.Log (peleador.enabled);


					for (int i = 0; i < poolBotones.Count; i++) {
						poolBotones [i].gameObject.SetActive (false);
					}

					if (peleador.sigueVivo) {
						if (peleador.aliado) {


							Accion proxAccion = new Accion ();
							bool sw = false;




							foreach (var accion in peleador.Acciones) {	
								Button b = null;

								for (int i = 0; i < poolBotones.Count; i++) {
									
									if (!poolBotones [i].gameObject.activeInHierarchy) {
										b = poolBotones [i];

									}


								}

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




								
										////////////////////////////////////
										//if (Controlador.ganasteHijoNormal == false) {


										if ( peleadores.IndexOf(peleador) == Controlador.dragoncito1 ) {

											// Sterterar sopbre dragocito 1 el triangulo
											indicadorTurno.SetActive (true);
											indicadorTurno.transform.position = marcaDragoncitoA1.transform.position;
										}

										if (peleadores.IndexOf(peleador) == Controlador.dragoncito2 ) {

											// Sterterar sopbre dragocito 2 el triangulo
											indicadorTurno.SetActive (true);
											indicadorTurno.transform.position = marcaDragoncitoA2.transform.position;
										}

										if (peleadores.IndexOf(peleador) == 0 ) {

											// Sterterar sopbre dragna
											indicadorTurno.SetActive (true);
									  		indicadorTurno.transform.position = marcaDragona.transform.position;

										}
									


										if ( accion.nombre =="Curar" ) {
											c = peleador.EjecutarAccion (accion, peleadores [ElegirAliado()].transform);
										}
										else { 
											c = peleador.EjecutarAccion (accion, peleadores [ElegirEnemigo()].transform);
										} 
										//} else {
										//	c = peleador.EjecutarAccion (accion, peleadores [2].transform);
										//}
									 

										});

								}
							}
						}

					 		else {


							indicadorTurno.SetActive(true);
							indicadorTurno.transform.position = marcaDragon.transform.position;


							int acciontomada = Random.Range (0, peleador.Acciones.Count);

							if (peleador.Acciones [acciontomada].nombre == "Curar") {
								c = peleador.EjecutarAccion (peleador.Acciones [acciontomada], peleadores [ElegirEnemigo()].transform);								
							} else {
								c = peleador.EjecutarAccion (peleador.Acciones [acciontomada], peleadores [ElegirAliado()].transform);	
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

			if (!aliadosvivos) { 
				Debug.Log ("PERDISTE");
				Controlador.escenaPrevia = "no";

				perdiste.Play ();
				yield return new WaitForSeconds (5);
				SceneManager.LoadScene ("perfil1");
			} 

			if (!enemigosvivos) { 
				Debug.Log ("GANASTE");
				Controlador.escenaPrevia = "no";
				ganaste.Play ();
				yield return new WaitForSeconds (5);
				SceneManager.LoadScene ("perfil1");
			}
		}

	}


	int ElegirEnemigo() {

		//return peleadores.Count - 1
			
		return enemigoActual; 
	}

	int ElegirAliado() {
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
		return cual;
	}
}