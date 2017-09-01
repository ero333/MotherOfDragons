using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor.SceneManagement;
using System.Linq;

public class DialogParser : MonoBehaviour {

	List<DialogueLine> lines;

	struct DialogueLine{

		public string name;
		public string content;
		public int pose;

		public DialogueLine(string n, string c, int p){

			name = n;
			content = c;
			pose = p;
		}
	}

	// Use this for initialization
	void Start () {
		lines = new List<DialogueLine> ();
		string file = "Dialogue1";
		string sceneNum = EditorSceneManager.GetActiveScene ().name;
		sceneNum = Regex.Replace (sceneNum, "[^0-9]", "");
		file += sceneNum; //file = file + scenenum("dialogue1")
		file += ".txt";

		LoadDialogue (file);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public string GetName(int lineNumber){
		if (lineNumber < lines.Count)
			return lines [lineNumber].name;

		return "";
	
	}

	public string GetContent(int lineNumber){
		if (lineNumber < lines.Count)
			return lines [lineNumber].content;

		return "";
	}

	public int GetPose(int lineNumber){
		if (lineNumber < lines.Count)
			return lines [lineNumber].pose;

		return 0;
	}

	void LoadDialogue(string filename){

		string file = "Assets/Resources/" + filename;
		string line;
		StreamReader r = new StreamReader (file);

		using(r)
		{
			do
			{
				line = r.ReadLine();
				if(line != null){
					string[] line_values = line.Split ('|');
					DialogueLine line_entry = new DialogueLine(line_values[0],line_values[1], int.Parse (line_values[2]));
					lines.Add (line_entry);
				
				}
			}
			while(line != null);
			r.Close();
		}
	}
}
