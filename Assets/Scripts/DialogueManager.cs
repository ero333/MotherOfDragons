using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.Analytics;

public class DialogueManager : MonoBehaviour {

	public DialogueParser guion;
	public Animator animator;
	public string dialogue, characterName;
	public int lineNum;
	int pose;
	public Image ganaste;
	public Image preparateParaPelea;
	public Text dialogueBox;
	public Text nameBox;
	public ChoiceButton choiceBox1, choiceBox2, choiceBox3;
	public VideoPlayer romance;

	public static string resultado = "";
	public static float StartTime = 0;

	// Use this for initialization
	void Start () {
		romance.Prepare ();
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

		Analytics.CustomEvent ("DialogosEmpezar", new Dictionary<string, object> {
			{ "quien", Controlador.NombresPerfiles[SceneManager.GetActiveScene().buildIndex] }
		});


	}

	void Awake(){
		
		StartTime = Time.time;
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
		romance.Play ();
		yield return new WaitForSeconds(11);
		romance.Stop ();
		animator.SetTrigger ("GANAR");
		ganaste.enabled = true;
		yield return new WaitForSeconds(5);
		SceneManager.LoadScene ("perfil1");
	}
	IEnumerator Pelear() {
		Debug.Log("PELEA");
		preparateParaPelea.enabled = true;
		yield return new WaitForSeconds(2);
		SceneManager.LoadScene ("Combate1");
	}

	void Responder1()
	{
		SetDialog(choiceBox1.resultado);
		//Debug.Log("Respondiste 1; Ir a linea "+choiceBox1.resultado);
	}

	void Responder2()
	{
		SetDialog(choiceBox2.resultado);
		//Debug.Log("Respondiste 2; Ir a linea "+choiceBox2.resultado);
	}

	void Responder3()
	{
		SetDialog(choiceBox3.resultado);
		//Debug.Log("Respondiste 3; Ir a linea "+choiceBox3.resultado);
	}

	void SetDialog(int linea) {

		Debug.Log (" set dialog en linea " + linea);
		choiceBox1.GetComponent<Button>().onClick.RemoveListener(() => Responder1());
		choiceBox2.GetComponent<Button>().onClick.RemoveListener(() => Responder2());
		choiceBox3.GetComponent<Button>().onClick.RemoveListener(() => Responder3());

		switch(linea) {
			case -1:
				if(SceneManager.GetActiveScene().name == "Scene1"){
					Controlador.escenaPrevia = "ARIEL";
				}
				if(SceneManager.GetActiveScene().name == "Scene2"){
					Controlador.escenaPrevia = "MATEO";
				}
				if(SceneManager.GetActiveScene().name == "Scene3"){
					Controlador.escenaPrevia = "DIEGO";
				}
				if(SceneManager.GetActiveScene().name == "Scene4"){
					Controlador.escenaPrevia = "MAXIMILIANO";
				}
				if(SceneManager.GetActiveScene().name == "Scene5"){
					Controlador.escenaPrevia = "ISRAEL";
				}
				resultado = "Pelea";

			Analytics.CustomEvent ("DialogosFin", new Dictionary<string, object> {
				{ "Resultado", resultado },
				{ "Quien", SceneManager.GetActiveScene().name },
				{ "Time", Time.time-StartTime }
			});

				print("COMBATE!");
				//int sceneNum = SceneManager.GetActiveScene ().buildIndex;
				//SceneManager.LoadScene (sceneNum+1);
				StartCoroutine (Pelear());
				break;

		case 0:
			if (SceneManager.GetActiveScene ().name == "Scene1") {
				Controlador.GanarHijo (9);
				Controlador.escenaPrevia = "ARIEL";
			}
			if (SceneManager.GetActiveScene ().name == "Scene2") {
				Controlador.GanarHijo (2);
				Controlador.escenaPrevia = "MATEO";
			}
			if (SceneManager.GetActiveScene ().name == "Scene3") {
				Controlador.GanarHijo (11);
				Controlador.escenaPrevia = "DIEGO";
			}
			if (SceneManager.GetActiveScene ().name == "Scene4") {
				Controlador.GanarHijo (6);
				Controlador.escenaPrevia = "MAXIMILIANO";
			}
			if (SceneManager.GetActiveScene ().name == "Scene5") {
				Controlador.GanarHijo (5);
				Controlador.escenaPrevia = "ISRAEL";
			}

			resultado = "Ganaste";

			Analytics.CustomEvent ("DialogosFin", new Dictionary<string, object> {
				{ "Resultado", resultado },
				{ "Quien", SceneManager.GetActiveScene().name },
				{ "Time", Time.time-StartTime }
			});


			print ("HUEVITO!!");
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
