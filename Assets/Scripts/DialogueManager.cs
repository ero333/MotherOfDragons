using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DialogueManager : MonoBehaviour {

	public DialogueParser guion;
	public Animator animator;
	public string dialogue, characterName;
	public int lineNum;
	int pose;
	public Image ganaste;
	public Text dialogueBox;
	public Text nameBox;
	public ChoiceButton choiceBox1, choiceBox2, choiceBox3;


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

		//SpriteRenderer ganaste = GameObject.Find ("huevito").GetComponent<SpriteRenderer> ();
		//SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>(); 

	}

	// Update is called once per frame
	void Update () {

		ShowDialogue();
	}

	IEnumerator Esperar() {
		Debug.Log("HUEVITO");
		yield return new WaitForSeconds(5);
		SceneManager.LoadScene ("perfil1");
		Debug.Log("After Waiting 2 Seconds");
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

		//} else {
		//		print("COMBATE!");
		//		SceneManager.LoadScene ("Combate");
				
		//	} 

		switch(linea) {
			case -1:
			 
				print("COMBATE!");
				int sceneNum = SceneManager.GetActiveScene ().buildIndex;
				SceneManager.LoadScene (sceneNum+1);
				break;

		case 0:
					
			print ("HUEVITO!!");
			animator.SetTrigger ("GANAR");
			ganaste.enabled = true;
			if(SceneManager.GetActiveScene().name == "Scene1" || SceneManager.GetActiveScene().name == "Scene2"){
				Controlador.HijosGanados[0] = true;
			}

			StartCoroutine (Esperar());
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



	public void ShowDialogue() {
		//ResetImages ();
		ParseLine ();
	}



	void ParseLine() {

			pose = guion.GetPose (lineNum);
			DisplayImages();
		
	}


	//

	void ResetImages() {
			
			//GameObject character = GameObject.Find (characterName);
			//SpriteRenderer currSprite = character.GetComponent<SpriteRenderer>();
			//currSprite.sprite = null;

	}
	void DisplayImages() {
		
			//GameObject character = GameObject.Find(characterName);
			//SpriteRenderer currSprite = character.GetComponent<SpriteRenderer>();
			//currSprite.sprite = character.GetComponent<Character>().characterPoses[pose];

	}


}
