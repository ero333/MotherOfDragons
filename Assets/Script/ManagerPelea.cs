using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ManagerPelea : MonoBehaviour {

	public List<Peleador> peleadores;
	public VideoPlayer ganaste;
	public VideoPlayer perdiste;
	public static ManagerPelea singleton;

	public Button prefab;
	public Text textoEstado;
	public Transform panel;

	public HealthBar barraAliado;
	public HealthBar barraEnemigo;




	/*
	public void SetHealth(int vida){
		
		GameObject Abarra = GameObject.FindGameObjectWithTag("barra");
		var AbarraRectTransform = Abarra.transform as RectTransform;
		AbarraRectTransform.sizeDelta = new Vector2 ((vida*2), 25);
	}
	*/



	void Awake (){
		if (singleton != null) {

			Destroy (gameObject);
			return;
		}
		singleton = this;

	}

	public void ActualizarInterface(){
		textoEstado.text = "";
		foreach (var peleador in peleadores)
		{
			if (peleador.sigueVivo) 
			{

				if (peleador.aliado) {
					barraAliado.SetHealth (peleador.vida);  
				} else {
					barraEnemigo.SetHealth(peleador.vida);  
				}


		


				textoEstado.text += "<color=" + (peleador.aliado ? "blue" : "red") + ">" +
				peleador.nombre + " HP: " + peleador.vida + "/100 MANA: " + peleador.mana + "/100.</color>\n";
			}

			
		}
	}

	void Start()
	{
		ActualizarInterface();
		StartCoroutine("Bucle");
		ganaste.Prepare();
		perdiste.Prepare ();
	}


	List<Button> poolBotones = new List<Button>();
	IEnumerator Bucle (){
		bool aliadosvivos = true;
		bool enemigosvivos = true;
		while (aliadosvivos || enemigosvivos) {
			aliadosvivos = false;
			enemigosvivos = false;
			foreach (var peleador in peleadores) {

				IEnumerator c = null;

				for (int i = 0; i < poolBotones.Count; i++) {
					poolBotones [i].gameObject.SetActive (false);
				}

				if (peleador.sigueVivo) 
				{
					if (peleador.aliado) 
					{

						Accion proxAccion = new Accion();
						bool sw = false;

						foreach (var accion in peleador.Acciones) 
						{	
							Button b = null;
							for (int i = 0; i < poolBotones.Count; i++) {
								if (!poolBotones [i].gameObject.activeInHierarchy) {
									b = poolBotones [i];

								}
							}

							b = Instantiate (prefab, panel);
							//b.transform.SetParent (panel);

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
										b.gameObject.SetActive (false);
									}


									///////////////////////////////

									int indice = 0;
									//objetivo.position = peleadores[indice].transform.position;

									//if (!proxAccion.objetivoEsElMismo){
											
										//if (Input.GetKeyDown(KeyCode.LeftArrow))
											//{
												
												//if (indice > 0)
												//{
												//	indice--;
												//	indice = Random.Range (1, peleadores.Count);                                        
												//}
												//objetivo.position = peleadores[indice].transform.position;


												
											//}
											//if (Input.GetKeyDown(KeyCode.RightArrow)){
												//indice++;
												//if (indice >= peleadores.Count)
												//{
												//	indice = 0;                                      
												//}
												//objetivo.position = peleadores[indice].transform.position;

										//}
									//}

									////////////////////////////////////

									c = peleador.EjecutarAccion (accion, peleadores [2].transform);
									//Random.Range (1, peleadores.Count)
								});
							}
						}

					} else {
						//Random.Range (0, peleadores.Count)
						c = peleador.EjecutarAccion (peleador.Acciones [Random.Range (0, 1)],
							peleadores [Random.Range (0, 2)].transform);
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

			if (!aliadosvivos) { 
				Debug.Log ("PERDISTE");
				perdiste.Play ();
				yield return new WaitForSeconds (5);
				SceneManager.LoadScene ("perfil1");
			} 

			if(!enemigosvivos) { 
				Debug.Log ("GANASTE");
				ganaste.Play ();
				yield return new WaitForSeconds (5);
				SceneManager.LoadScene ("perfil1");
			}
		}

	}
}