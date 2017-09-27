using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ManagerPelea : MonoBehaviour {

	public List<Peleador> peleadores;

	public static ManagerPelea singleton;

	public Button prefab;
	public Text textoEstado;
	public Transform panel;

	public Image Abarra;
	public HealthBar Ebarra;
	public Image barraDeVida;

	void Start (){
		//Abarra = GetComponentInChildren<HealthBar> ();
		//GameObject  Abarra = GameObject.FindGameObjectWithTag("barra").GetComponent<RectTransform>();

		StartCoroutine ("Bucle");
	}


	public void SetHealth(int vida){
		
		GameObject Abarra = GameObject.FindGameObjectWithTag("barra");
		var AbarraRectTransform = Abarra.transform as RectTransform;
		AbarraRectTransform.sizeDelta = new Vector2 ((vida*2), 25);
	}

	void Update (){
		ActualizarInterface ();
	}

	void Awake (){
		if (singleton != null) {

			Destroy (gameObject);
			return;
		}
		singleton = this;

	}

	public void ActualizarInterface(){
		textoEstado.text = "";
		foreach (var Peleador in peleadores)
		{
			if (Peleador.sigueVivo) 
			{

				//Ebarra.SetHealth(Peleador.vida);  

				SetHealth (Peleador.vida);
				textoEstado.text += "<color=" + (Peleador.aliado ? "blue" : "red") + ">" +
				Peleador.nombre + " HP: " + Peleador.vida + "/100 MANA: " + Peleador.mana + "/100.</color>\n";
			}

			
		}
	}




	List<Button> poolBotones = new List<Button>();
	IEnumerator Bucle (){
		while (true) {
			foreach (var Peleador in peleadores)
			{

				IEnumerator c = null;

				for (int i = 0; i < poolBotones.Count; i++)
				{
					poolBotones [i].gameObject.SetActive (false);
				}

				if (Peleador.sigueVivo)
				{
					if (Peleador.aliado) 
					{
						foreach (var accion in Peleador.Acciones) 
						{	
							Button b = null;
							for (int i = 0; i < poolBotones.Count; i++) 
							{
								if (!poolBotones [i].gameObject.activeInHierarchy)
								{
									b = poolBotones [i];

								}
							}

							b = Instantiate (prefab);
							b.transform.SetParent(panel);

							b.transform.position = Vector3.zero;
							b.transform.localScale = Vector3.one;

							poolBotones.Add (b);

							b.gameObject.SetActive (true);

							b.onClick.RemoveAllListeners ();
							b.GetComponentInChildren<Text>().text = accion.nombre;
							if (Peleador.mana < accion.costoMana)
							{
								b.interactable = false;
							} 
							else
							{
								b.interactable = true;
								b.onClick.AddListener (() => {

									for (int j = 0; j < poolBotones.Count; j++)
									{
										b.gameObject.SetActive (false);
									}

									c = Peleador.EjecutarAccion(
										accion,
										peleadores[Random.Range(0, peleadores.Count)].transform);
								});
							}
						}

					} 
					else 
					{
						c = Peleador.EjecutarAccion(Peleador.Acciones[Random.Range(0, Peleador.Acciones.Count)],
							peleadores[Random.Range(0, peleadores.Count)].transform);
					}
								while (c == null)
								{
									yield return null;
								}
					yield return StartCoroutine(c);
					yield return new WaitForSeconds (1);
				}
			}
		
		}
	}
}