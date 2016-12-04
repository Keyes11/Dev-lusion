using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Devlusion{
	public class Menu : MonoBehaviour{

		private enum state{ intro, menu, config, loading, setResolution }
		private state currentState;

		// resolution
		private float resoluc = 5;
		private bool fullscreen = true;

		// position
		public Texture title;
		private float sizeH;
		private float sizeV;
		private float posH;
		private float posV;

		// text print
		private float time	= 0;
		private int index	= 0;
		private string text = ">programming -douglas silva 'hermes passer' lacerda\n\n>level design -jorge 'hackerman' enrique\n\n>art -everton 'keyes' martins\n\n>documentation -leonardo 'john bidu' oliveira\n\n\naccessing the dev'lusion...\n\n";
		private string finalText = "";

		void Awake (){
			currentState = state.intro;
			loadData ();
		}

		void OnGUI (){
			GUI.DrawTexture (new Rect (posH, posV / 2, sizeH, sizeV), title);

			switch (currentState) {
			case state.intro:
				// Set resolution (in start and awake does work)
				if (finalText == "")
					setResolution ();
				
				if (finalText != text) {
					
					// Skip intro
					if (Input.anyKey) {
						finalText = text;
						return;
					}

					time += Time.deltaTime;

					if (time > 0.1f && index < text.Length) {											
						finalText += text [index].ToString ();							
						time = 0;											
						index++;	
					}

					GUI.color = new Color (0f, 150f, 0f, 255f);
					GUI.Label (new Rect (Screen.width / 2 - sizeH / 2, Screen.height / 3, sizeH * 2, sizeH), finalText); // creditos
					return;
				}

				currentState = state.menu;
			break;
				
			case state.loading:
				GUI.color = new Color (0f, 150f, 0f, 255f);
				if (Vocabulary.language) // pt
					GUI.Label (new Rect (posH, 5 + posV, sizeH, sizeV), "Carregando...");
				else
					GUI.Label (new Rect (posH, 5 + posV, sizeH, sizeV), "Loading...");
			break;
				
			case state.menu:
				if (GUI.Button (new Rect (posH, 5 + posV, sizeH, sizeV), Vocabulary.vocabulary [15])) { // novo jogo
					currentState = state.loading;
					newGame ();
				}

				if (GUI.Button (new Rect (posH, 5 + sizeV + posV, sizeH, sizeV), Vocabulary.vocabulary [16])) { // carregar jogo
					if (GameData.loadData ()) {
						GameData.loadData ();
						currentState = state.loading;
					}
				}

				if (GUI.Button (new Rect (posH, 5 + sizeV * 2 + posV, sizeH, sizeV), Vocabulary.vocabulary [9])) // config
					currentState = state.config;

				if (GUI.Button (new Rect (posH, 5 + sizeV * 3 + posV, sizeH, sizeV), Vocabulary.vocabulary [18]) && Application.platform != RuntimePlatform.WebGLPlayer) // sair
					Application.Quit ();	
			break;
			
			case state.config:
				if (GUI.Button (new Rect (posH, 5 + sizeV * 2 + posV, sizeH / 2, sizeV), Vocabulary.vocabulary [17])) // pt
					Vocabulary.language = true;

				if (GUI.Button (new Rect (posH + sizeH / 2, 5 + sizeV * 2 + posV, sizeH / 2, sizeV), Vocabulary.vocabulary [19])) // eng
					Vocabulary.language = false;

				if (Input.GetKey (KeyCode.Escape) || GUI.Button (new Rect (posH, 5 + sizeV * 3 + posV, sizeH, sizeV), Vocabulary.vocabulary [3]))
					returnToMainMenu ();

				if (Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
					return;

				setResolution ();

				GUI.Label (new Rect (posH, posV / 2 + sizeV, sizeH + 30, sizeV), Vocabulary.vocabulary [14]);
				resoluc = GUI.HorizontalSlider (new Rect (posH, 5 + posV, sizeH + 30, sizeV), resoluc, 0.18F, 10.0F);

				if (GUI.Button (new Rect (posH, 5 + sizeV + posV, sizeH, sizeV), Vocabulary.vocabulary [13] + fullscreen)) {
					fullscreen = !fullscreen;
					setResolution ();
				}
			break;
			}	
		}

		private void setResolution(){
            if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.LinuxPlayer || Application.platform == RuntimePlatform.OSXPlayer)
				Screen.SetResolution (Screen.resolutions [(int)resoluc].width, Screen.resolutions [(int)resoluc].height, fullscreen, 0);
		
			sizeH = Screen.width / 4;
			sizeV = Screen.height / 8;
			posH = Screen.width / 2 - sizeH / 2;
			posV = Screen.height / 2 - sizeV;
		}
			
		public void loadData (){
			if (PlayerPrefs.GetInt ("menuFullscreen") == 0)
				fullscreen = true;
			else
				fullscreen = false;
			
			if (PlayerPrefs.HasKey ("menuResolution") && PlayerPrefs.HasKey ("menuFullscreen")) {
				resoluc = PlayerPrefs.GetFloat ("menuResolution");
			
			}
		}

		public void saveData (){
			if (PlayerPrefs.HasKey ("menuResolution") && PlayerPrefs.HasKey ("menuFullscreen")) {
				GameData.menuFullscreen = fullscreen;
				GameData.menuResolution = resoluc;
			}
		}

		private void newGame (){
			PlayerPrefs.DeleteAll ();
			saveData (); // para a config ficar salva apos deletar o load game
			SceneManager.LoadSceneAsync ("ACT1", LoadSceneMode.Single);
		}

		public void callChangeLanguage (){
			Vocabulary.language = !Vocabulary.language; 							// recebe a negação dele mesmo
			Vocabulary.changeLanguage ();											// altera o idioma
		}

		private void returnToMainMenu (){
			currentState = state.menu;
			Vocabulary.changeLanguage ();
			saveData ();
		}
	}
}