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
		characterName = "Mateo";
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

	void Destroy()
	{
		choiceBox1.GetComponent<Button>().onClick.RemoveListener(() => Responder1());
		choiceBox2.GetComponent<Button>().onClick.RemoveListener(() => Responder2());
		choiceBox3.GetComponent<Button>().onClick.RemoveListener(() => Responder3());
	}


	// Update is called once per frame
	void Update () {

	}

	IEnumerator Esperar() {
		Debug.Log("HUEVITO");
		yield return new WaitForSeconds(5);
		SceneManager.LoadScene ("perfil1");
		Debug.Log("After Waiting 2 Seconds");
	}
	IEnumerator Pelear() {
		Debug.Log("PELEA");
		yield return new WaitForSeconds(2);
		SceneManager.LoadScene ("Combate1");
	}

	void Responder1()
	{
		SetDialog(choiceBox1.resultado);
		Debug.Log("Respondiste 1; Ir a linea "+choiceBox1.resultado);
	}

	void Responder2()
	{
		SetDialog(choiceBox2.resultado);
		Debug.Log("Respondiste 2; Ir a linea "+choiceBox2.resultado);
	}

	void Responder3()
	{
		SetDialog(choiceBox3.resultado);
		Debug.Log("Respondiste 3; Ir a linea "+choiceBox3.resultado);
	}

	void SetDialog(int linea) {

		Debug.Log (" set dialog en linea " + linea);
		choiceBox1.GetComponent<Button>().onClick.RemoveListener(() => Responder1());
		choiceBox2.GetComponent<Button>().onClick.RemoveListener(() => Responder2());
		choiceBox3.GetComponent<Button>().onClick.RemoveListener(() => Responder3());

		switch(linea) {
			case -1:
			 
				print("COMBATE!");
				//int sceneNum = SceneManager.GetActiveScene ().buildIndex;
				//SceneManager.LoadScene (sceneNum+1);
				StartCoroutine (Pelear());
				break;

		case 0:
					
			print ("HUEVITO!!");
			animator.SetTrigger ("GANAR");
			ganaste.enabled = true;
			if(SceneManager.GetActiveScene().name == "Scene1"){
				Controlador.HijosGanados[9] = true;
				Controlador.escenaPrevia = "Scene1";
			}
			if(SceneManager.GetActiveScene().name == "Scene2"){
				Controlador.HijosGanados[2] = true;
				Controlador.escenaPrevia = "Scene2";
			}
			if(SceneManager.GetActiveScene().name == "Scene3"){
				Controlador.HijosGanados[0] = true;
				Controlador.escenaPrevia = "Scene3";
			}
			if(SceneManager.GetActiveScene().name == "Scene4"){
				Controlador.HijosGanados[6] = true;
				Controlador.escenaPrevia = "Scene4";
			}
			if(SceneManager.GetActiveScene().name == "Scene5"){
				Controlador.HijosGanados[5] = true;
				Controlador.escenaPrevia = "Scene5";
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


			SetFace (linea);
				
				break;
					
		}
	}
		

	void SetFace(int linea) {
		GameObject character = GameObject.Find(characterName);
		SpriteRenderer currSprite = character.GetComponent<SpriteRenderer>();
		currSprite.sprite = character.GetComponent<Character>().characterPoses[guion.GetPose(linea)];
	}


}
