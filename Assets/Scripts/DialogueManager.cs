﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class DialogueManager : MonoBehaviour {

	public DialogueParser guion;

	public string dialogue, characterName;
	public int lineNum;
	int pose;

	public Text dialogueBox;
	public Text nameBox;
	public ChoiceButton choiceBox1, choiceBox2, choiceBox3;


	void UpdateUI() {


	}
	// Use this for initialization
	void Start () {

		dialogue = "";
		characterName = "";
		pose = 0;
		guion = GameObject.Find("DialogueParser").GetComponent<DialogueParser>();
		lineNum = 0;

		choiceBox1 = GameObject.Find ("Button1").GetComponent<ChoiceButton> ();
		choiceBox2 = GameObject.Find ("Button2").GetComponent<ChoiceButton> ();
		choiceBox3 = GameObject.Find ("Button3").GetComponent<ChoiceButton> ();

		choiceBox1.GetComponent<Button>().onClick.AddListener(() => Responder1());
		choiceBox2.GetComponent<Button>().onClick.AddListener(() => Responder2());
		choiceBox3.GetComponent<Button>().onClick.AddListener(() => Responder3());

		SetDialog (1); 
	}

	void Responder1()
	{
		SetDialog(choiceBox1.resultado);
		Debug.Log("Respondiste 1");
	}

	void Responder2()
	{
		SetDialog(choiceBox2.resultado);
		Debug.Log("Respondiste 2");
	}

	void Responder3()
	{
		SetDialog(choiceBox3.resultado);
		Debug.Log("Respondiste 3");
	}

	void SetDialog(int linea) {

		//if ((guion.GetResults (linea, 1)) != 1 || (guion.GetResults (linea, 2)) != 1 || (guion.GetResults (linea, 3)) != 1) {
		//	dialogueBox.text = guion.GetPregunta (linea); 

		//	choiceBox1.SetText (guion.GetOptions (linea, 1));
		//	choiceBox2.SetText (guion.GetOptions (linea, 2));
		//	choiceBox3.SetText (guion.GetOptions (linea, 3));

		//	choiceBox1.SetResult (guion.GetResults (linea, 1));
		//	choiceBox2.SetResult (guion.GetResults (linea, 2));
		//	choiceBox3.SetResult (guion.GetResults (linea, 3));

		//	pose = guion.GetPose (linea);

		//} else {
		//		print("COMBATE!");
		//		SceneManager.LoadScene ("Combate");
				
		//	} 

		switch(linea) {
			case -1:
			 
				print("COMBATE!");
				SceneManager.LoadScene ("Combate");
				break;

		case 0:
				
			print ("HUEVITO!!");

				break;

			default:
			dialogueBox.text = guion.GetPregunta (linea);
			
			choiceBox1.SetText (guion.GetOptions (linea, 1));
			choiceBox2.SetText (guion.GetOptions (linea, 2));
			choiceBox3.SetText (guion.GetOptions (linea, 3));

			choiceBox1.SetResult (guion.GetResults (linea, 1));
			choiceBox2.SetResult (guion.GetResults (linea, 2));
			choiceBox3.SetResult (guion.GetResults (linea, 3));
			break;
					
		}
	}





	void ResetImages() {
		if (characterName != "") {
			GameObject character = GameObject.Find (characterName);
			SpriteRenderer currSprite = character.GetComponent<SpriteRenderer>();
			currSprite.sprite = null;
		}
	}
	void DisplayImages() {
		if (characterName != "") {
			GameObject character = GameObject.Find(characterName);
			SpriteRenderer currSprite = character.GetComponent<SpriteRenderer>();
			currSprite.sprite = character.GetComponent<Character>().characterPoses[pose];
		}
	}

	// Update is called once per frame
	void Update () {

		UpdateUI ();
	}

}
