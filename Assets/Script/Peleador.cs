using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable] 
public struct Accion {

	public string nombre;
	//public bool estatico;
	public bool objetivoEsElMismo;
	public string mensaje;
	public int argumento;
	public string animacionTrigger;

	public int costoMana;


}

public class Peleador : MonoBehaviour {

	[SerializeField]
	public Animator animator;
	public HealthBar barraSalud;
	public int vida;
	public List<Accion> Acciones;
	public string nombre;
	public int mana;
	public float cubrimiento;
	public bool aliado;
	public bool sigueVivo = true;
	public int perderturnos=0;


	void PerderTurnos(int cant) {
		perderturnos += cant; 
	}


	void Atacar(int cant)
	{
		CambiarVida (cant);
	}

	void Bloquear (int cant)
	{
		if(vida <=80){
			CambiarVida (cant);
		}
			
		animator.SetTrigger ("Bloqueo");	
	}


	void Curar (int cant){
		
		if(vida <= 80){
			vida += cant;
		}
	}

	void CambiarVida(int cant){

		//barra.AddHealth( (float) cant );
		vida += cant;

		if( cant < 0 )
			animator.SetTrigger ("Daño");	

		if (cubrimiento != 1){
		if (cant < 0){
				cant = (int)(cant / cubrimiento);
				cubrimiento = 1;
			}
		}
		mp.ActualizarInterface ();

		sigueVivo = false;
		if (vida > 0) {
			sigueVivo = true;
		} else {
			animator.SetTrigger ("Morir");
		}
	}

	void CambiarMana(int cant){

		mana += cant;
		mp.ActualizarInterface ();

	}


	void Cubrir(int cuanto){
		cubrimiento = cuanto;
		mp.ActualizarInterface ();
	}


	//Animator anim;
	//NavMeshAgent nv;
	ManagerPelea mp;

	void Awake() {

		mp = GameObject.Find ("Canvas").GetComponent<ManagerPelea> ();
	}	


	void Start () {

		//mp = ManagerPelea.singleton;
		//anim = GetComponent<Animator>();
		//nv = GetComponent<NavMeshAgent>();
		cubrimiento=1;


		//if(nombre == "Dragoncito" && Controlador.ganasteHijoNormal == false){
		//	animator.speed = 0;
		//	gameObject.SetActive (false);
		//	barraSalud.gameObject.SetActive (false);
		//}

		// Alternativa a setear el animator desde el inspector en el editor
		//animator = gameObject.GetComponent<Animator> ();
	}

	public IEnumerator EjecutarAccion(Accion accion, Transform objetivo){

		if(perderturnos>0) {
			perderturnos--;
		}
	    else {
			CambiarMana (-accion.costoMana);
			if (accion.objetivoEsElMismo) { 
				objetivo = transform;
				if(accion.nombre== "Curar"){
					animator.SetTrigger ("Curar");
				}
			
			}



			if(accion.nombre== "Golpear")
				animator.SetTrigger ("Attack");	

			objetivo.SendMessage (accion.mensaje, accion.argumento);
			print ("mensaje: "+accion.mensaje+" argumento: "+accion.argumento);
			yield return new WaitForSeconds (1/2);


			if (accion.nombre == "Especial") {
				animator.SetTrigger ("Especial");
			}
			
		}
	}
}
