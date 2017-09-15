using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChoiceButton : MonoBehaviour {

	public string option;
	public DialogueManager manager;
	public DialogueParser guion;
	public Text dialogueBox;
	//public GameObject choiceBox1, choiceBox2, choiceBox3;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Button choiceBox1 = GameObject.Find("Button1").GetComponent<Button>();
		Button choiceBox2 = GameObject.Find("Button2").GetComponent<Button>();
		Button choiceBox3 = GameObject.Find("Button3").GetComponent<Button>();
		//choiceBox1.onClick.AddListener(() => SeClickeo1(1));
		//choiceBox2.onClick.AddListener(SeClickeo2);
		//choiceBox3.onClick.AddListener(SeClickeo3);
	}

	public void SetText(string newText) {
		this.GetComponentInChildren<Text> ().text = newText;
	}

	public void SetOption(string newOption) {
		this.option = newOption;
	}

	public void SeClickeo1(int linea) {
		Debug.Log("You have clicked BOTON 1");

		dialogueBox.GetComponentInChildren<Text> ().text = guion.GetPregunta(guion.GetResults (linea, 1));

	}
	public void SeClickeo2(int linea) {
		Debug.Log("You have clicked BOTON 2");

		dialogueBox.GetComponentInChildren<Text> ().text = guion.GetPregunta(guion.GetResults (linea, 2));

	}
	public void SeClickeo3(int linea) {
		Debug.Log("You have clicked BOTON 3");

		dialogueBox.GetComponentInChildren<Text> ().text = guion.GetPregunta(guion.GetResults (linea, 3));

	}

	//public void ParseOption(int lineaClickeada) {
		//string command = option.Split (',') [0];
		//string commandModifier = option.Split (',') [1];

		//if (command == "line") {
		//	box.lineNum = int.Parse(commandModifier);

		//} else if (command == "scene") {
		//	SceneManager.LoadScene("Scene" + commandModifier);
		//}


		//box.lineNum = guion.GetResults(lineaClickeada); 

		//box.GetComponentInChildren<Text> ().text = guion.GetResults (linea, 1);
		//choiceBox2.GetComponentInChildren<Text> ().text = guion.GetOptions (linea, 2);
		//choiceBox3.GetComponentInChildren<Text> ().text = guion.GetOptions (linea, 3);

	//}

}
