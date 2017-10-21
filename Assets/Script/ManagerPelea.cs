﻿using System.Collections;
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

		marcaDragon = GameObject.Find("Marca Dragon");
		marcaDragona = GameObject.Find("Marca Dragona");

		marcaDragoncitoA1 = GameObject.Find("Marca Dragoncito 1 Aliado");
		marcaDragoncitoA2 = GameObject.Find("Marca Dragoncito 2 Aliado");

		if(Controlador.escenaPrevia == "Scene1"){
			GameObject.FindGameObjectWithTag ("israel").SetActive (false);
			GameObject.FindGameObjectWithTag ("maximiliano").SetActive (false);
			GameObject.FindGameObjectWithTag ("diego").SetActive (false);
			GameObject.FindGameObjectWithTag ("ariel").SetActive (false);
		}
		if(Controlador.escenaPrevia == "Scene2"){
			GameObject.FindGameObjectWithTag ("israel").SetActive (false);
			GameObject.FindGameObjectWithTag ("maximiliano").SetActive (false);
			GameObject.FindGameObjectWithTag ("mateo").SetActive (false);
			GameObject.FindGameObjectWithTag ("ariel").SetActive (false);
		}
		if(Controlador.escenaPrevia == "Scene3"){
			GameObject.FindGameObjectWithTag ("israel").SetActive (false);
			GameObject.FindGameObjectWithTag ("maximiliano").SetActive (false);
			GameObject.FindGameObjectWithTag ("diego").SetActive (false);
			GameObject.FindGameObjectWithTag ("mateo").SetActive (false);
		}
		if(Controlador.escenaPrevia == "Scene4"){
			GameObject.FindGameObjectWithTag ("israel").SetActive (false);
			GameObject.FindGameObjectWithTag ("ariel").SetActive (false);
			GameObject.FindGameObjectWithTag ("diego").SetActive (false);
			GameObject.FindGameObjectWithTag ("mateo").SetActive (false);
		}
		if(Controlador.escenaPrevia == "Scene3"){
			GameObject.FindGameObjectWithTag ("ariel").SetActive (false);
			GameObject.FindGameObjectWithTag ("maximiliano").SetActive (false);
			GameObject.FindGameObjectWithTag ("diego").SetActive (false);
			GameObject.FindGameObjectWithTag ("mateo").SetActive (false);
		}

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




										c = peleador.EjecutarAccion (accion, peleadores [peleadores.Count - 1].transform);




									});


								}


							}

						} else {


							indicadorTurno.SetActive(true);
							indicadorTurno.transform.position = marcaDragon.transform.position;



							c = peleador.EjecutarAccion (peleador.Acciones [Random.Range (0, peleador.Acciones.Count)],
								peleadores [Random.Range (0, (peleadores.Count - 2))].transform);	
							




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
				perdiste.Play ();
				yield return new WaitForSeconds (5);
				SceneManager.LoadScene ("perfil1");
			} 

			if (!enemigosvivos) { 
				Debug.Log ("GANASTE");
				ganaste.Play ();
				yield return new WaitForSeconds (5);
				SceneManager.LoadScene ("perfil1");
			}
		}

	}
}