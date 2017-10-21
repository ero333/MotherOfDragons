using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class DialogueParser : MonoBehaviour {

	//public TextAsset dialo1;

	struct DialogueLine {

// ID Preguntas	Sprite	Opcion 1	Resultado 1	Opcion 2	Resultado 2	Opcion 3	Resultado 3
		public int id;
		public string pregunta;
		public int sprite;
		public string options1;
		public int results1;
		public string options2;
		public int results2;
		public string options3;
		public int results3;

		public DialogueLine(int NumLinea, string Content, int Pose, string Opc1, int Result1, string Opc2, int Result2, string Opc3, int Result3) {
			id = NumLinea;
			pregunta = Content;
			sprite = Pose;
			options1 = Opc1;
			results1 = Result1;
			options2 = Opc2;
			results2 = Result2;
			options3 = Opc3;
			results3 = Result3;
		}
	}
	List<DialogueLine> lines;


	// Use this for initialization
	void Start () {

		//string file = "Assets/Data/Dialogue1.tsv";
		//string file = dialo1.text;

		string file = Application.streamingAssetsPath + "/Dialogue";
		string sceneNum = SceneManager.GetActiveScene ().name;

		sceneNum = Regex.Replace (sceneNum, "[^0-9]", "");
		file += sceneNum;
		file += ".tsv";

		lines = new List<DialogueLine>();
		LoadDialogue (file);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LoadDialogue(string filename) {
		string line;
		StreamReader r = new StreamReader (filename);
		line = r.ReadLine();

		lines.Add(new DialogueLine(0,"pregunta",0,"opcion 1",0,"opcion 2",0,"opcion 3",0));
		using (r) {
			do {
				line = r.ReadLine();
				if (line != null){
					string[] lineData = line.Split('\t');
					DialogueLine lineEntry = new DialogueLine(int.Parse(lineData[0]), lineData[1], int.Parse(lineData[2]), lineData[3], int.Parse(lineData[4]), lineData[5], int.Parse(lineData[6]), lineData[7], int.Parse(lineData[8]));

					lines.Add(lineEntry);

				}
			}
			while (line != null);
			r.Close();

		
		}
	}

	public int GetLine(int lineNumber) {
		if (lineNumber < lines.Count) {
			return lines[lineNumber].id;
		}
		return 0;
	}

	public string GetName(int lineNumber) {
		
		return "Mateo";
	}

	public string GetPregunta(int lineNumber) {
		if (lineNumber < lines.Count) {
			return lines[lineNumber].pregunta;
		}
		return "";
	}

	public int GetPose(int lineNumber) {
		if (lineNumber < lines.Count) {
			return lines[lineNumber].sprite;
		}
		return 0;
	}

	public int GetResults(int lineNumber, int result) {
		if (lineNumber < lines.Count) {
			switch (result) {
			case 1:
				return lines [lineNumber].results1;
				break;

			case 2:
				return lines [lineNumber].results2;
				break;

			case 3:
				return lines [lineNumber].results3;
				break;

			default:
				return 0;
			}
		}
		return 0;
		
	}
	public string GetOptions(int lineNumber, int option) {
		if (lineNumber < lines.Count) {
			
			switch(option) {
			case 1:
				
				return lines [lineNumber].options1;
				break;

			case 2:
				return lines [lineNumber].options2;
				break;

			case 3:
				return lines [lineNumber].options3;
				break;

			default:
				return "";
			}

		}
		return "";

	}
}
