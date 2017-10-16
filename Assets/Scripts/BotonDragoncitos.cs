using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonDragoncitos : MonoBehaviour {

	public bool ButtonOn = false;

	public void BeenClicked(){
		ButtonOn = !ButtonOn;
		if (ButtonOn) {
			gameObject.GetComponent<Image> ().color = new Color32 (92, 223, 223, 255);
		} else {
			gameObject.GetComponent<Image> ().color = new Color32 (255,255,255,255);
		}
	}

	public void OnClick (int d){
		Seleccion (d);

	}

	void Seleccion(int D){
		if(Controlador.HijosGanados[D]){
			if(Controlador.dragoncito1 == -1){
				Controlador.dragoncito1 = D;
				Debug.Log ("d1 es " + Controlador.dragoncito1);
			}
			if (Controlador.dragoncito2 == -1) {
				Controlador.dragoncito2 = D;
				Debug.Log ("d2 es " + Controlador.dragoncito2);
			} else {
				Controlador.dragoncito2 = Controlador.dragoncito1;
				Controlador.dragoncito1 = D;
				Debug.Log ("d1 es:" + Controlador.dragoncito1 + ", y d2 es:" + Controlador.dragoncito2);
			}
		}
	}
		
}
