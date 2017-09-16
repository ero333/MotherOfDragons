using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChoiceButton : MonoBehaviour {

	public int resultado;
	public DialogueManager manager;

	public Text dialogueBox;
	//public GameObject choiceBox1, choiceBox2, choiceBox3;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		//choiceBox1.onClick.AddListener(() => SeClickeo1(1));
		//choiceBox2.onClick.AddListener(SeClickeo2);
		//choiceBox3.onClick.AddListener(SeClickeo3);
	}

	public void SetText(string newText) {
		this.GetComponentInChildren<Text> ().text = newText;
	}

	public void SetResult(int newResult) {
		this.resultado = newResult;
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
