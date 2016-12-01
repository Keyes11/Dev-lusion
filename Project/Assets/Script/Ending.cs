using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
namespace Devlusion{
	public class Ending : MonoBehaviour {

		private float time	= 0;
		private int index	= 0;
		private string text = "";
		private string finalText = "";
		private float sizeH;
		private string partOne;

		private bool restart;
		public bool	textEnd = true;	
		
		private int spinx  = 0;
		private float spiny  = 0.05f;
		private int spinz  = 0;
		 
		public void setLogText(string txt){
			index = 0;
			text = txt;
			textEnd = false;
		}
		
		void Start (){
			sizeH = Screen.width / 4;
			if (Vocabulary.language) // pt
				partOne = "... Desativando sistema Dev'lusion...\n$Programação & Game Design:\n   DOUGLAS SILVA LACERDA\n   EVERTON BARBOSA MARTINS\n   JORGE ENRIQUE VENDRAMINI ORELLANA MUNOZ\nAssets:\n   EVERTON BARBOSA MARTINS\n   DOUGLAS SILVA LACERDA\nDocumentação:\n   LEONARDO OLIVEIRA\n   JORGE ENRIQUE VENDRAMINI ORELLANA MUNOZ\nLevel Design:\n   JORGE ENRIQUE VENDRAMINI ORELLANA MUNOZ\n   DOUGLAS SILVA LACERDA\n   LEONARDO OLIVEIRA@%";
			else
				partOne = "... Disabling Dev'lusion system ...\n$Programing & Game Design:\n   DOUGLAS SILVA LACERDA\n   EVERTON BARBOSA MARTINS\n   JORGE ENRIQUE VENDRAMINI ORELLANA MUNOZ\nAssets:\n   EVERTON BARBOSA MARTINS\n   DOUGLAS SILVA LACERDA\nDocumentation:\n   LEONARDO OLIVEIRA\n   JORGE ENRIQUE VENDRAMINI ORELLANA MUNOZ\nLevel Design:\n   JORGE ENRIQUE VENDRAMINI ORELLANA MUNOZ\n   DOUGLAS SILVA LACERDA\n   LEONARDO OLIVEIRA@%";
			
			setLogText(partOne);
		}
		
		void OnGUI (){
			
			time += Time.deltaTime;
			
			if (time > 0.1f){
				transform.Rotate(spinx,spiny,spinz);
				if (index < text.Length) {											
					if (text [index].ToString () == "$")
						finalText = "";
					else if (text [index].ToString () == "%")
						SceneManager.LoadSceneAsync ("Menu", LoadSceneMode.Single);
					else if (text [index].ToString () == "@")
						time = -10;
					else {
						finalText += text [index].ToString ();							
						time = 0;											
					}
					index++;
				}
			}
			GUI.color = new Color (0f, 0f, 0f, 255f);
			GUI.Label (new Rect (sizeH, 10, sizeH * 4, sizeH * 2), finalText); // creditos
		}
	}
}