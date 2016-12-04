using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Devlusion.PauseMenu;
/*
 	Salva e Carrega os dados do jogo

	Toda a classe que tem atributos a serem salvos
	terão um saveData() ou um loadData()
 */
namespace Devlusion{
	public class GameData : MonoBehaviour {

		private GameObject[] data;

		//LevelController
		public static int levelControllerCurrentAct;
		public static int lastAct = 1;

		// Player
		//public static Vector3 playerPosition;
		public static float   playerHP;

		// PauseMenuInventory
		public static string inventoryItemJournal;

		//PauseMenuConsoleBase
		public static string consoleBasePrompt;
		public static string consoleBaseLog;

		//Menu
		public static bool menuFullscreen;
		public static float menuResolution;

		void Awake(){
			QualitySettings.vSyncCount = 0;  // VSync must be disabled

            if (Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
                QualitySettings.SetQualityLevel(2, true);

            Application.targetFrameRate = 30;

            //Profiler.maxNumberOfSamplesPerFrame = 1024;
            DontDestroyOnLoad (transform.gameObject);
			data = GameObject.FindGameObjectsWithTag ("GAME_DATA");

			if (data.Length >= 2)
				Destroy(data[1]);
		}

		public static void saveData(){
			//LevelController
			if (levelControllerCurrentAct <= lastAct)
				PlayerPrefs.SetInt ("currentAct", levelControllerCurrentAct);
			else return; // pois o jogo estará completado
			

			//Player
			Player.instance.saveData ();
			PlayerPrefs.SetFloat ("playerHP", playerHP);
			//PlayerPrefs.SetFloat ("playerPositionX", playerPosition.x); PlayerPrefs.SetFloat ("playerPositionY", playerPosition.y); layerPrefs.SetFloat ("playerPositionZ", playerPosition.z);

			//Vocabulary
			if (Vocabulary.language)
				PlayerPrefs.SetInt ("vocabularyLanguage", 1);
			else
				PlayerPrefs.SetInt ("vocabularyLanguage", 0);

			//PauseMenuInventory
			PauseMenuInventory.instance.saveData();
			PlayerPrefs.SetString ("inventoryItemJournal", inventoryItemJournal);
			for (int i = 0; i < 10; i++)
				PlayerPrefs.SetInt ("inventoryIndex" + i, PauseMenuInventory.inventoryIndex[i]);

			// Menu
			PlayerPrefs.SetFloat ("menuResolution", menuResolution);
			if (Vocabulary.language)
				PlayerPrefs.SetInt ("menuFullscreen", 1);
			else
				PlayerPrefs.SetInt ("menuFullscreen", 0);

			PlayerPrefs.Save();
		}

		public static bool loadData(){
			// Voltando ao mapa
			if (PlayerPrefs.HasKey ("currentAct")) {
				if (levelControllerCurrentAct + 1 <= lastAct) // se for igual, ele ainda não jogou o ultimo lv
					SceneManager.LoadScene("ACT" + PlayerPrefs.GetInt ("currentAct") + 1);
				return true;
			}
			return false;
		}
	}

}