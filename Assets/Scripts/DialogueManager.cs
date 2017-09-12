using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class DialogueManager : MonoBehaviour {

	DialogueParser guion;

	public string dialogue, characterName;
	public int lineNum;
	int pose;

	public Text dialogueBox;
	public Text nameBox;
	public GameObject choiceBox1, choiceBox2, choiceBox3;


	void UpdateUI() {


	}
	// Use this for initialization
	void Start () {

		dialogue = "";
		characterName = "";
		pose = 0;
		guion = GameObject.Find("DialogueParser").GetComponent<DialogueParser>();
		lineNum = 0;

		SetDialog (1); 
	}

	void SetDialog(int linea) {

		dialogueBox.text = guion.GetPregunta(linea); 

		choiceBox1.GetComponentInChildren<Text> ().text = guion.GetOptions (linea, 1);
		choiceBox2.GetComponentInChildren<Text> ().text = guion.GetOptions (linea, 2);
		choiceBox3.GetComponentInChildren<Text> ().text = guion.GetOptions (linea, 3);

	}

	// Update is called once per frame
	void Update () {

		UpdateUI ();
	}

}
