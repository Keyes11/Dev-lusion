using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
/*
	Responsavel por salvar o jogo e enviar
	o jogador para a proxima fase
*/
namespace Devlusion{
	public class LevelController : MonoBehaviour{
        private float time = 0f;
        private bool goToNextAct;

        public int act = 1;
		public static bool canPassLevel = true;//false;
        
        void Update(){
            if (!goToNextAct)
                return;
            time += Time.deltaTime;
            PauseMenu.PauseMenuInventory.isPaused = false;

            if (time > 1f){
                GameData.levelControllerCurrentAct = act;
                GameData.saveData();

                if (act < GameData.lastAct) // se for igual então zerou o jogo
                    SceneManager.LoadScene("ACT" + act);
                else
                    SceneManager.LoadScene("Ending"); // botar creditos aqui
            }
        }

		void OnTriggerEnter2D(Collider2D other) {
            if (canPassLevel && other.CompareTag ("Player")) {
                Fade.instance.callFade(1);
                goToNextAct = true;
            }
		}
	}
}