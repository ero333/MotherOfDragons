using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



[System.Serializable] 
public struct Accion {

	public string nombre;
	public bool estatico;
	public bool objetivoEsElMismo;



	public string mensaje;
	public int argumento;

	public string animacionTrigger;

	public int costoMana;


}

public class Peleador : MonoBehaviour {

	[SerializeField]
	Animator animator;

	public int vida;
	public List<Accion> Acciones;

	public string nombre;

	public int mana;
	public float cubrimiento;
	public bool aliado;
	public bool sigueVivo = true;

	void Atacar(int cant)
	{
		Debug.Log ("Peleador::Atacar(" + cant + ")");
		CambiarVida (cant);
		animator.SetTrigger ("Attack");	
	}

	void CambiarVida(int cant){

		//barra.AddHealth( (float) cant );

		vida += cant;

		if (cubrimiento != 1){
		if (cant < 0){
				cant = (int)(cant / cubrimiento);
				cubrimiento = 1;
			}
		}
		mp.ActualizarInterface ();
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

	void Start () {

		mp = ManagerPelea.singleton;
		//anim = GetComponent<Animator>();
		//nv = GetComponent<NavMeshAgent>();
		cubrimiento=1;

		// Alternativa a setear el animator desde el inspector en el editor
		//animator = gameObject.GetComponent<Animator> ();
	}

	public IEnumerator EjecutarAccion(Accion accion, Transform objetivo){

		CambiarMana (-accion.costoMana);
		if (accion.objetivoEsElMismo)
			objetivo = transform;
		if (accion.estatico) {

			//anim.SetTrigger (accion.animacionTrigger);
			objetivo.SendMessage (accion.mensaje, accion.argumento);
		} else {
			Vector3 PosInicial = transform.position;

			transform.LookAt (objetivo.transform.position);
			//nv.SetDestination (objetivo.position);
			//anim.SetFloat("Speed", 1);

			while (Vector3.Distance (transform.position, objetivo.position) > 2)
				yield return null;
			//nv.speed = 0;
			//anim.SetFloat("Speed", 0);

			yield return new WaitForSeconds (0.5f);
			//anim.SetTrigger (accion.animacionTrigger);
			yield return new WaitForSeconds (0.1f);
			objetivo.SendMessage (accion.mensaje, accion.argumento);
			yield return new WaitForSeconds (1);

			transform.LookAt (PosInicial);
			//nv.SetDestination (PosInicial);
			//nv.speed = 3.5f;
			//anim.SetFloat("Speed", 1);

			while (Vector3.Distance (transform.position, objetivo.position) > 0.1f)
				yield return null;
			//anim.SetFloat("Speed", 0);

		}
	}
}
