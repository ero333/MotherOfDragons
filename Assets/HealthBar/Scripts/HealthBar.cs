using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	[SerializeField]
	public float health;
	[SerializeField]
	private float maxHealth;
	[SerializeField]
	private Image heatlhBarImage;

	private void Start(){

		health = maxHealth;

	}

	//[ContextMenu("Add - 5")]
	public void AddHealth( float amount )
	{

		health += amount;

		if (health > maxHealth) {
		
			health = maxHealth;
		
		} else if (health < 0) {
		
			health = 0;


		}

		UpdateHealthUI ();

	}

	public void SetHealth( float amount )
	{
		health = amount;
		if (health > maxHealth) {
			health = maxHealth;
		} else if (health < 0) {
			health = 0;
		}
		UpdateHealthUI ();
	}


	//(0, max) -> (0, 1)
	private void UpdateHealthUI(){
	
		heatlhBarImage.fillAmount = (1 / maxHealth) * health;
	
	}


}
